using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public bool IsActive { get; set; } 
       
    }
}
