using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoMvc.POCOs
{
    public class Rental
    {
        //[BsonId]
        //public ObjectId _id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string Description { get; set; }
        public int NumberOfRooms { get; set; }
        public List<string> Address = new List<string>();

        [BsonRepresentation(BsonType.Double)]
        public decimal Price { get; set; }
    }
}