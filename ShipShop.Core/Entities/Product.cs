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
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
       [ForeignKey("LookupItem")]
        public int ProductStatusId { get; set; }
        public LookupItem LookupItem { get; set; }
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<OrderItem> Items { get; set; }
       public List<CartItem> CartItems { get; set; }
        public List<WishListItem> WishListItems { get; set; }

    }
}
