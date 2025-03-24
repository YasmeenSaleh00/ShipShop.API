using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
  
        public string Email { get; set; }
        public string CustomerStatus { get; set; }
        public string RoleName { get; set; }
  
        public string CreateOn { get; set; }
        public string UpdateOn { get; set; }
    }
}
