using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Entities
{
    public class LookupItem : MainEntity
    {
        public string Value { get; set; }
        public int LookupTypeId { get; set; }
        public LookupType LookupType { get; set; }

     
        public List<Product> Products { get; set; }

 
        public List<User> Users { get; set; }

    
        public List<Order> Orders { get; set; }
        public List<Cart> Carts { get; set; }       
    }
}
