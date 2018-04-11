using Microsoft.SqlServer.Server;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoMvc.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MongoMvc.Controllers
{
    public class DynamicFormController : Controller
    {
        // GET: DynamicForm
        public ActionResult Index()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostDynamic(FormCollection data)
        {
            using (var context = new MongoContext())
            {
                var json = FormSerializer(data);
                var collection = context.Database.GetCollection<BsonDocument>("questionnaire");
                var document = BsonSerializer.Deserialize<BsonDocument>(json);
                collection.InsertOne(document);
            }
            
             return View("index");
        }

        public string FormSerializer(FormCollection collection)
        {
            var list = new Dictionary<string, string>();
            foreach (string key in collection.Keys)
            {
             
                list.Add(key, collection[key]);
            }
            list.Remove("__RequestVerificationToken");
            return new JavaScriptSerializer().Serialize(list);
        }

    }
}