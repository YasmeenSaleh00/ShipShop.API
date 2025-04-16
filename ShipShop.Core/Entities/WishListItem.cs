using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Entities
{
    public class WishListItem:MainEntity
    {
        public int WishListId { get; set; }
        public WishList WishList { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
