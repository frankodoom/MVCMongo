using MongoDB.Bson;
using MongoMvc.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MongoMvc.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            //Testing server run
            //var _context = new MongoContext();
            //var buildinfoCommand = new BsonDocument("buildinfo", 1);
            //var buildInfo = await _context.Database.RunCommandAsync<BsonDocument>(buildinfoCommand);
            //return Content(buildInfo.ToJson(), "application/json");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}