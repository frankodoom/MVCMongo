using MongoDB.Driver;
using MongoMvc.POCOs;
using MongoMvc.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoMvc.Context
{
    public class MongoContext: IDisposable
    {
        public IMongoDatabase Database;

        public MongoContext()
        {
            
            var client = new MongoClient(Settings.Default.MongoConn);         
            Database = client.GetDatabase(Settings.Default.Database);
      
        }

        public IMongoCollection<Rental> Rentals => Database.GetCollection<Rental>("rentals");

        public void Dispose()
        {
           
        }
    }
}