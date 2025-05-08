using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Models
{
    internal class MostOrderedProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public float Price { get; set; }
        public string Description { get; set; }
        public int TotalOrderedQuantity { get; set; }
    }
}
