using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoMvc.POCOs
{
    public class Rental
    {
        public string _id { get; set; }
        public string Description { get; set; }
        public int NumberOfRooms { get; set; }
        public List<string> Address = new List<string>();

        [BsonRepresentation(MongoDB.Bson.BsonType.Double)]
        public decimal Price { get; set; }
    }
}