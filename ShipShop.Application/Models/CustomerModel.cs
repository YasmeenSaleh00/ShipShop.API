using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string CustomerStatus { get; set; }
        public string Role { get; set; }
        public string CreateOn { get; set; }
        public string UpdateOn { get; set; }


    }
}
