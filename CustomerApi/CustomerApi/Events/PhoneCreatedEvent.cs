using CustomerApi.Data.Models.Sql;

namespace CustomerApi.Events
{
    public class PhoneCreatedEvent : IEvent
    {
        public PhoneType Type { get; set; }

        public int AreaCode { get; set; }

        public int Number { get; set; }
    }
}
