using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Entities
{
    public class Product:MainEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Description { get; set; }
        public string DescriptionAr { get; set; }
        public float Price { get; set; }
        public float TaxPercentage { get; set; }
        public string ImageUrl { get; set; }
       [ForeignKey("LookupItem")]
        public int ProductStatusId { get; set; }
        public LookupItem LookupItem { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<OrderItem> Items { get; set; }
       public List<CartItem> CartItems { get; set; }
        public List<WishListItem> WishListItems { get; set; }

    }
}
