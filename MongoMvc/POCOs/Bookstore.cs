using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoMvc.POCOs
{
    public class BookStore
    {
        public ObjectId Id { get; set; }
        public string BookTitle { get; set; }
        public string Auther { get; set; }
        public string Category { get; set; }
        public string ISBN { get; set; }
    }
}