﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Models
{
    public class OrderItemModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public string ImageUrl { get; set; }    
    }
}
