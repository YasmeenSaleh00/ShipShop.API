using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Entities
{
    public class Category:MainEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string? NameAr {  get; set; }
        public string Description { get; set; }
        public string? DescriptionAr { get; set; }
        public string? ImagePath { get; set; }
        public List<Product> Products { get; set; }
    }
}
