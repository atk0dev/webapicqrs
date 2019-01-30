namespace CustomerApi.CommandHandlers
{
    using System;
    using CustomerApi.Commands;
    using CustomerApi.Commands.Customer;
    using CustomerApi.Data;
    using CustomerApi.Data.Models.Sql;

    public class CustomerCommandHandler : ICommandHandler<Command>
    {
        private CustomerSqlRepository _repository;
        private EventPublisher _eventPublisher;

        public CustomerCommandHandler(EventPublisher eventPublisher, CustomerSqlRepository repository)
        {
            _eventPublisher = eventPublisher;
            _repository = repository;
        }

        public void Execute(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("Command is null");
            }

            if (command is CreateCustomerCommand createCommand)
            {
                CustomerRecord created = _repository.Create(createCommand.ToCustomerRecord());
                _eventPublisher.PublishEvent(createCommand.ToCustomerEvent(created.Id));
            }
            else if (command is UpdateCustomerCommand updateCommand)
            {
                CustomerRecord record = _repository.GetById(updateCommand.Id);
                _repository.Update(updateCommand.ToCustomerRecord(record));
                _eventPublisher.PublishEvent(updateCommand.ToCustomerEvent());
            }
            else if (command is DeleteCustomerCommand deleteCommand)
            {
                _repository.Remove(deleteCommand.Id);
                _eventPublisher.PublishEvent(deleteCommand.ToCustomerEvent());
            }
            else
            {
                throw new ArgumentNullException("Unknown Command");
            }
        }
    }
}
