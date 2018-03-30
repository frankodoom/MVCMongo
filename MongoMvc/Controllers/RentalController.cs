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
        public async System.Threading.Tasks.Task<ActionResult> IndexAsync()
        {
            using (var _context = new MongoContext())
            {
                // var data = await _context.Rentals.Find(new BsonDocument()).ToListAsync();
                var data = await _context.Rentals.Find(new BsonDocument()).ToListAsync();
                return View();
            }
        }


        public async Task<ActionResult> Create()
        {
            using(var _context = new MongoContext())
            {
                //var rentals = _context.Rentals.Find(new BsonDocument()).Project<Rental>(Builders<Rental>.Projection.Exclude(r => r.Description));

                var rentals = await _context.Rentals.Find(new BsonDocument()).Project<Rental>(Builders<Rental>.Projection.Exclude(r => r._id)).ToListAsync();


                return View(rentals);
            }
           
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
               await  _context.Rentals.InsertOneAsync(rental);
            }
           
            
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(RentalViewModel model)
        {
            using (var _context = new MongoContext())
            {
                var rental = new Rental()
                {
                    Address = (model.Address ?? string.Empty).Split('\n').ToList(),
                    Description = model.Description,
                    NumberOfRooms = model.NumberOfRooms,
                    Price = model.Price
                };
                await _context.Rentals.UpdateOneAsync(new BsonDocument(),rental, null);
            }


            return View("Index");
        }
    }
}