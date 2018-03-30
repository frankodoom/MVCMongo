﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoMvc.Models.ViewModels
{
    public class RentalViewModel
    {
        public string Description { get; set; }
        public int NumberOfRooms { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
    }
}