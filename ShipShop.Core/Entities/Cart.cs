using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Entities
{
    public class Cart:MainEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CustomerId { get; set; }
       public Customer Customer { get; set; }
        [ForeignKey("LookupItem")]
        public int StatusCartId { get; set; }
        public LookupItem LookupItem { get; set; }
        //public int Quantity { get; set; }

        public Order Order { get; set; }
       public List<CartItem> CartItems { get; set; }
    }
}
