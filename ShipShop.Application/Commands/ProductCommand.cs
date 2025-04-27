using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Commands
{
    public class ProductCommand
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
     
        public string Description { get; set; }
        public string DescriptionAr { get; set; }
        public float Price { get; set; }
        public float TaxPercentage { get; set; }
     
        public int  Quantity { get; set; }
        public int SubCategoryId { get; set; }
        public string ImageUrl { get; set; }
        public int BrandId { get; set; }
    
    }
}
