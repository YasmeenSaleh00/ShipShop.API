﻿using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Interfaces
{
    public interface ILookupRepository
    {
    
        Task<List<LookupItem>> GetLookupItemValueByType(int LookupTypeId);
        Task<LookupItem> GetLookupItemById(int id);
    }
}
