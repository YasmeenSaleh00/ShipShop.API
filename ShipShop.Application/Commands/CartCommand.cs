﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Commands
{
    public class CartCommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int customerId { get; set; }

    }
}
