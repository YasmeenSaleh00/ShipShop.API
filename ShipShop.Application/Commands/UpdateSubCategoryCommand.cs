using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Commands
{
    public class UpdateSubCategoryCommand
    {
        public string Name { get; set; }

        public string? NameAr { get; set; }
        public string Description { get; set; }
        public string? DescriptionAr { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string MainCategory { get; set; }
        public int CategoryId { get; set; }
    }
}
