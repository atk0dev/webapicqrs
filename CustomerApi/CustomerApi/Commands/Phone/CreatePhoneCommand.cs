namespace CustomerApi.Commands.Phone
{
    using CustomerApi.Data.Models.Sql;

    public class CreatePhoneCommand : Command
    {
        public PhoneType Type { get; set; }

        public int AreaCode { get; set; }

        public int Number { get; set; }
    }
}
