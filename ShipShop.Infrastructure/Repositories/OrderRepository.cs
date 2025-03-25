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

        public Task<float> CalculateTotalAmount(int id)
        {
            throw new NotImplementedException();
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

        public async Task<Order> GetById(int id)
        {
            var order = await _context.Orders.Include(o => o.Customer)
                                           .Include(o => o.Items)
                                           .ThenInclude(od => od.Product)
                                           .Include(od => od.LookupItem)
                                           .FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public async Task Update(Order input)
        {
          _context.Orders.Update(input);    
          await  _context.SaveChangesAsync();

        }
    }
}
