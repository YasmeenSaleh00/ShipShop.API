using Microsoft.EntityFrameworkCore;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using ShipShop.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShipShopDbContext _context;

        public OrderRepository(ShipShopDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Order input)
        {
            _context.Add(input);
            await _context.SaveChangesAsync();
            return input.Id;
       
        }

      

        public async Task Delete(int id)
        {
            var order = await GetById(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAll()
        {

            var orders = await _context.Orders .Include(x => x.Customer)
                                               .Include(x =>x.Items)
                                               .ThenInclude(x=>x.Product)
                                               .Include(x=>x.LookupItem)
                                               .ToListAsync();  
            return orders;
        }

        public async Task<List<Order>> GetAllOrdersByCustomerId(int customerId)
        {
            var orders = await _context.Orders.Include(o => o.Customer)
                                           .Include(o => o.Items)
                                           .ThenInclude(od => od.Product)
                                           .Include(od => od.LookupItem)
                                           .Where(o => o.CustomerId == customerId).ToListAsync();

            return orders;
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _context.Orders.Include(o => o.Customer)
                                           .Include(o => o.Items)
                                           .ThenInclude(od => od.Product)
                                           .Include(od => od.LookupItem)
                                           .FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public async Task<List<Order>> SortByCreationDate(string sortDirection)
        {
            IQueryable<Order> orders = _context.Orders.Include(o => o.Customer)
                                           .Include(o => o.Items)
                                           .ThenInclude(od => od.Product)
                                           .Include(od => od.LookupItem);
           if (sortDirection == "desc")
            {
               orders= orders.OrderByDescending(x => x.CreatedOn);
            }
            if (sortDirection == "asc")
            {
                orders = orders.OrderBy(x => x.CreatedOn);
            }
            return await orders.ToListAsync();
        }

        public async Task<List<Order>> SortByDeliveryDate(string sortDirection)
        {
            IQueryable<Order> orders = _context.Orders.Include(o => o.Customer)
                                            .Include(o => o.Items)
                                            .ThenInclude(od => od.Product)
                                            .Include(od => od.LookupItem);
            if (sortDirection == "desc")
            {
                orders = orders.OrderByDescending(x => x.DeliveryDate);
            }
            if (sortDirection == "asc")
            {
                orders = orders.OrderBy(x => x.DeliveryDate);
            }
            return await orders.ToListAsync();
        }

        public async Task<List<Order>> SortById(string sortDirection)
        {
            IQueryable<Order> orders =  _context.Orders.Include(o => o.Customer)
                                           .Include(o => o.Items)
                                           .ThenInclude(od => od.Product)
                                           .Include(od => od.LookupItem);
            if (sortDirection == "desc")
            {
                orders = orders.OrderByDescending(x => x.Id);
            }
            if (sortDirection == "asc")
            {
                orders = orders.OrderBy(x => x.Id);
            }
            return await orders.ToListAsync();
        }

        public async Task<List<Order>> SortByName(string sortDirection)
        {
            IQueryable<Order> orders = _context.Orders.Include(o => o.Customer)
                               .Include(o => o.Items)
                               .ThenInclude(od => od.Product)
                               .Include(od => od.LookupItem);
            if (sortDirection == "desc")
            {
                orders = orders.OrderByDescending(x => x.CustomerName);
            }
            if (sortDirection == "asc")
            {
                orders = orders.OrderBy(x => x.CustomerName);
            }
            return await orders.ToListAsync();
        }

        public async Task Update(Order input)
        {
          _context.Orders.Update(input);    
          await  _context.SaveChangesAsync();

        }
       
    }
}
