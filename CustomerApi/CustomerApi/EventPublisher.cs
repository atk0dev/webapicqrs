using System.Text;
using CustomerApi.Events;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Constants = CustomerApi.Events.Constants;

namespace CustomerApi
{
    public class EventPublisher
    {
        private readonly ConnectionFactory connectionFactory;

        public EventPublisher(IHostingEnvironment env)
        {
            connectionFactory = new ConnectionFactory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

            builder.Build().GetSection("amqp").Bind(connectionFactory);
        }

        public void PublishEvent<T>(T theEvent) where T : IEvent
        {
            using (IConnection conn = connectionFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    var queue = theEvent is CustomerCreatedEvent ?
                        Constants.QUEUE_CUSTOMER_CREATED : theEvent is CustomerUpdatedEvent ?
                            Constants.QUEUE_CUSTOMER_UPDATED : Constants.QUEUE_CUSTOMER_DELETED;

                    channel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(theEvent));

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queue,
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    }
}
