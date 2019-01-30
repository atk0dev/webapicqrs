namespace CustomerApi.Data.Models.Sql
{
    using System.Collections.Generic;

    public class CustomerRecord
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public List<PhoneRecord> Phones { get; set; }
    }
}
