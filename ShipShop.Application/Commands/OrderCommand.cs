using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Commands
{
    public class OrderCommand
    {

      
        public string CustomerName { get; set; }
      
        public string Phone { get; set; }
        public string ShippingAddress { get; set; }
        public string? Note { get; set; }
    }
}
