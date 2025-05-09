using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Entities
{
    public class Messages:MainEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone {  get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
      
    }
}
