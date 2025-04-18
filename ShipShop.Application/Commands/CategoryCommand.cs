using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Commands
{
    public class CategoryCommand
    {
  
        public string Name { get; set; }
      
        public string? NameAr { get; set; }
        public string Description { get; set; }
        public string? DescriptionAr { get; set; }
        public string ImageUrl { get; set; }

    }
}
