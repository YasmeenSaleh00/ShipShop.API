﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Commands
{
    public class WishListCommand
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
    }
}
