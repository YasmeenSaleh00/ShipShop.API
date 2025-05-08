using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Models
{
    public class TestimonialModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int CustomerId { get; set; }
        public string CreatedOn { get; set; }
    }
}
