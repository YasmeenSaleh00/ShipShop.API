﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Commands
{
    public class ChangePasswordCommand
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
