using CustomerApi.Commands.Phone;
using CustomerApi.Events;

namespace CustomerApi.Commands.Customer
{
    using System.Collections.Generic;
    using System.Linq;
    using CustomerApi.Data.Models.Sql;

    public class CreateCustomerCommand : Command
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public List<CreatePhoneCommand> Phones { get; set; }

        public CustomerCreatedEvent ToCustomerEvent(long id)
        {
            return new CustomerCreatedEvent
            {
                Id = id,
                Name = this.Name,
                Email = this.Email,
                Age = this.Age,
                Phones = this.Phones.Select(phone => new PhoneCreatedEvent { AreaCode = phone.AreaCode, Number = phone.Number }).ToList()
            };
        }

        public CustomerRecord ToCustomerRecord()
        {
            return new CustomerRecord
            {
                Name = this.Name,
                Email = this.Email,
                Age = this.Age,
                Phones = this.Phones.Select(phone => new PhoneRecord { AreaCode = phone.AreaCode, Number = phone.Number }).ToList()
            };
        }
    }
}
