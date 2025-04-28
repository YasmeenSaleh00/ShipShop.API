﻿using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Commands
{
    public class UpdateCategoryCommand
    {
        public string Name { get; set; }

        public string? NameAr { get; set; }
        public string Description { get; set; }
        public string? DescriptionAr { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
