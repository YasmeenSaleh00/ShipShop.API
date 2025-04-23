using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Commands
{
    public class UpdateOrderStatusCommand
    {
        public int OrderId { get; set; }
        public int OrderStatusId { get; set; }
    }
}
