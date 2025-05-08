using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Entities
{
    public class Testimonial:MainEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }  
    }
}
