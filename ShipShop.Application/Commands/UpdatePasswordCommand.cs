﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Commands
{
    public class UpdatePasswordCommand
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword {  get; set; }
    }
}
