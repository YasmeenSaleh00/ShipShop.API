using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Commands
{
    public class RemoveFromCartCommand
    {
        public int CartId { get; set; } 
        public int ProductId { get; set; }
    }
}
