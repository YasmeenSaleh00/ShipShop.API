﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Entities
{
    public class Role:MainEntity
    {
        public string Name { get; set; }

        public List<User> Users { get; set; }
    


    }
}
