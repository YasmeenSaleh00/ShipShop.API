﻿using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Interfaces
{
    public interface IOrderRepository:IRepository<Order>
    {
    
        Task<List<Order>> GetAllOrdersByCustomerId(int customerId);
        Task<List<Order>> SortByDeliveryDate(string sortDirection);

    }
}
