using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Entities
{
    public class Customer:User
    {
        

        [ForeignKey("LookupItem")]
        public int CustomerStatusId { get; set; }
        public LookupItem LookupItem { get; set; }
        public List<Cart> Carts { get; set; }

        public List<Order> Orders { get; set; }
        public List<WishList> WishList { get; set; }    
    }
}
