using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Models
{
    public class WishListItemModel
    {
        public int productId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public float Price { get; set; }
        public string ProductStatus { get; set; }
    }
}
