using MongoDB.Bson;
using MongoDB.Driver;
using MongoMvc.Context;
using MongoMvc.Models.ViewModels;
using MongoMvc.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MongoMvc.Controllers
{
    public class RentalController : Controller
    {
        // GET: Rental
        public async Task<ActionResult> Index(decimal ? search)
        {
            //Filter By Price
            if (search > 0)
            {
                using (var _context = new MongoContext())
                {
                    //Greater or Equal TO
                    //var rentals = await _context.Rentals.Find(Builders<Rental>.Filter.Gte(r => r.Price, search)).ToListAsync();

                    //Less Than Or Equal To
                    //var rentals = await _context.Rentals.Find(Builders<Rental>.Filter.Lte(r => r.Price, search)).ToListAsync();

                    var rentals = await _context.Rentals.Find(Builders<Rental>.Filter.Where(r => r.Price ==search)).ToListAsync();

                    return View(rentals);
                }

            }
            using (var _context = new MongoContext())
            {
                var rentals = await _context.Rentals.Find(new BsonDocument()).Project<Rental>(Builders<Rental>.Projection.Exclude(r => r.id))
                    .SortBy(r=>r.Price) //defult Ascending
                    .ThenByDescending(s=>s.NumberOfRooms) //perform another sort on another field
                    .ToListAsync();

                return View(rentals);
            }
        }


        //Create A Rental View Form
        public ActionResult Create()
        {

            return View();
                 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RentalViewModel model)
        {
            using(var _context = new MongoContext())
            {
                var rental = new Rental()
                {
                    Address = (model.Address ?? string.Empty).Split('\n').ToList(),
                    Description = model.Description,
                    NumberOfRooms = model.NumberOfRooms,
                    Price = model.Price
                };
                await _context.Rentals.InsertOneAsync(rental);
              
            }
                  
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Update(string id,  RentalViewModel model)
        //{
        //    using (var _context = new MongoContext())
        //    {
        //        var rental = new Rental()
        //        {
        //            Address = (model.Address ?? string.Empty).Split('\n').ToList(),
        //            Description = model.Description,
        //            NumberOfRooms = model.NumberOfRooms,
        //            Price = model.Price
        //        };
        //        await _context.Rentals.UpdateOne(r=>r.id == id, rental);
        //    }


        //    return View("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
          using (var context = new MongoContext())
           {
            await context.Rentals.DeleteOneAsync(r => r.id == id);
                return RedirectToAction("Index");
           }
        }
    }
}