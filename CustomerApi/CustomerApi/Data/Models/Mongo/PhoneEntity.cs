using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApi.Data.Models.Sql;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomerApi.Data.Models.Mongo
{
    public class PhoneEntity
    {
        [BsonElement("Type")]
        public PhoneType Type { get; set; }
        [BsonElement("AreaCode")]
        public int AreaCode { get; set; }
        [BsonElement("Number")]
        public int Number { get; set; }
    }
}
