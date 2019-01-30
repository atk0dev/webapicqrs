using CustomerApi.Events;

namespace CustomerApi.Commands.Customer
{
    public class DeleteCustomerCommand : Command
    {
        internal CustomerDeletedEvent ToCustomerEvent()
        {
            return new CustomerDeletedEvent
            {
                Id = this.Id
            };
        }
    }
}
