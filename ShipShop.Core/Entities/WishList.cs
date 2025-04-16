using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Entities
{
    public class WishList:MainEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<WishListItem> WishListItems { get; set; }
                   

    }
}
