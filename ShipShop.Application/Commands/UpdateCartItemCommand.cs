using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Commands
{
    public class UpdateCartItemCommand
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}
