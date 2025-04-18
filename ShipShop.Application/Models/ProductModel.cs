using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public string DescriptionAr { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float TaxPercentage { get; set; }
        public string ProductStatus { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public bool IsActive { get; set; }

    }
}
