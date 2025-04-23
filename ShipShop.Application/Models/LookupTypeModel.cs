using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Models
{
    public class LookupTypeModel
    {
        public int Id { get; set; }     
        public string TypeName { get; set; }
        public string Value { get; set; }  
        public int LookupTypeId { get; set; } 
    }

}
