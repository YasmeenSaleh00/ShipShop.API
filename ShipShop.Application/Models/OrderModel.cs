using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Models
{
    public class OrderModel
    {
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string OrderStatus { get; set; }

        public int OrderStatusId { get; set; }
        public string OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public string DeliveryDate { get; set; }
        public string Notes { get; set; }
        public float TotalPrice { get; set; }
   
        public List<OrderItemModel> Items { get; set; }
    }
}
