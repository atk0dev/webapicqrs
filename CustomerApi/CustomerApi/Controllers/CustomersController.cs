using CustomerApi.CommandHandlers;
using CustomerApi.Commands.Customer;
using CustomerApi.Dto;

namespace CustomerApi.Controllers
{
    using System.Collections.Generic;
    using CustomerApi.Commands;
    using CustomerApi.Data;
    using CustomerApi.Data.Models.Mongo;
    using CustomerApi.Data.Models.Sql;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerSqlRepository sqlRepository;
        private readonly CustomerMongoRepository mongoRepository;
        private readonly ICommandHandler<Command> commandHandler;

        public CustomersController(CustomerSqlRepository sqlRepository, 
            ICommandHandler<Command> commandHandler, 
            CustomerMongoRepository mongoRepository)
        {
            this.sqlRepository = sqlRepository;
            this.commandHandler = commandHandler;
            this.mongoRepository = mongoRepository;
        }

        [HttpGet]
        public List<CustomerEntity> Get()
        {
            return this.mongoRepository.GetCustomers();
        }

        [HttpGet("{id:int}", Name = "GetCustomer")]
        public IActionResult GetById(long id)
        {
            var customer = this.mongoRepository.GetCustomer(id);
            if (customer == null)
            {
                return this.NotFound();
            }

            return new ObjectResult(customer);
        }

        [HttpGet("{email}", Name = "GetCustomerByEmail")]
        public IActionResult GetByEmail(string email)
        {
            var customer = this.mongoRepository.GetCustomerByEmail(email);
            if (customer == null)
            {
                return this.NotFound();
            }

            return new ObjectResult(customer);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateCustomerCommand customer)
        {
            this.commandHandler.Execute(customer);
            var response = new CustomerCreatedResponse
            {
                Email = customer.Email,
                Age = customer.Age,
                Name = customer.Name
            };

            return this.CreatedAtRoute("GetCustomerByEmail", new { email = customer.Email }, response);
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] UpdateCustomerCommand customer)
        {
            var record = this.sqlRepository.GetById(id);
            if (record == null)
            {
                return this.NotFound();
            }

            customer.Id = id;
            this.commandHandler.Execute(customer);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var record = this.sqlRepository.GetById(id);
            if (record == null)
            {
                return this.NotFound();
            }

            this.commandHandler.Execute(new DeleteCustomerCommand()
            {
                Id = id
            });

            return this.NoContent();
        }

        ////[HttpGet]
        ////public IActionResult GetAll()
        ////{
        ////    var customers = sqlRepository.GetAll();
        ////    if (customers == null)
        ////    {
        ////        return NotFound();
        ////    }
        ////    return new ObjectResult(customers);
        ////}

        ////[HttpGet("{id}", Name = "GetCustomer")]
        ////public IActionResult GetById(long id)
        ////{
        ////    var customer = sqlRepository.GetById(id);
        ////    if (customer == null)
        ////    {
        ////        return NotFound();
        ////    }
        ////    return new ObjectResult(customer);
        ////}
        ////[HttpPost]
        ////public IActionResult Post([FromBody] CustomerRecord customer)
        ////{
        ////    CustomerRecord created = sqlRepository.Create(customer);
        ////    return CreatedAtRoute("GetCustomer", new { id = created.Id }, created);
        ////}
        ////[HttpPut("{id}")]
        ////public IActionResult Put(long id, [FromBody] CustomerRecord customer)
        ////{
        ////    var record = sqlRepository.GetById(id);
        ////    if (record == null)
        ////    {
        ////        return NotFound();
        ////    }
        ////    customer.Id = id;
        ////    sqlRepository.Update(customer);
        ////    return NoContent();
        ////}
        ////[HttpDelete("{id}")]
        ////public IActionResult Delete(long id)
        ////{
        ////    var record = sqlRepository.GetById(id);
        ////    if (record == null)
        ////    {
        ////        return NotFound();
        ////    }
        ////    sqlRepository.Remove(id);
        ////    return NoContent();
        ////}
    }
}