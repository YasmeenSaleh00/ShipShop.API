using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Entities
{
    public class Order : MainEntity
    {
        public string CustomerName { get; set; }
        public string Phone { get; set; }

        [ForeignKey("LookupItem")]
        public int OrderStatusId { get; set; }
        public LookupItem LookupItem { get; set; }

        public string ShippingAddress { get; set; }
        public DateOnly DeliveryDate { get; set; }

        public string? Note { get; set; }
        public float TotalPrice { get; set; }
        public string Feedback { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }  
        public List<OrderItem> Items { get; set; }
    }
}
