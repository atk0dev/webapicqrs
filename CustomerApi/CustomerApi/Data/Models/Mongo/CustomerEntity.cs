using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomerApi.Data.Models.Mongo
{
    public class CustomerEntity
    {
        [BsonElement("Id")]
        public long Id { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Age")]
        public int Age { get; set; }
        [BsonElement("Phones")]
        public List<PhoneEntity> Phones;
    }
}
