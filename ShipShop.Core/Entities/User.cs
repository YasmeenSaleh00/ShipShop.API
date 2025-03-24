
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Entities
{
    public class User:MainEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string? ConfirmPassword { get; set; }

        [ForeignKey("LookupItem")]
        public int CustomerStatusId { get; set; }
        public LookupItem LookupItem { get; set; }
        public List<Cart> Carts { get; set; }
    
        public List<Order> Orders { get; set; }
        [NotMapped]
        public string FullName { get => FirstName + " "+ LastName; }
    }
}
