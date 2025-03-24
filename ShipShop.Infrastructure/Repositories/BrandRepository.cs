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
    public class BrandRepository : IRepository<Brand>
    {
        private readonly ShipShopDbContext _context;

        public BrandRepository(ShipShopDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Brand input)
        {
            await _context.AddAsync(input);
            await _context.SaveChangesAsync();
            return input.Id;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _context.Brands.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<List<Brand>> GetAll()
        {
            var brands = await _context.Brands.AsNoTracking().ToListAsync();
            return brands;
        }

        public async Task<Brand> GetById(int id)
        {
            var brand = await _context.Brands.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
            return brand;
        }

        public async Task Update(Brand input)
        {
            _context.Update(input);
            await _context.SaveChangesAsync();
           
        }
    }
}
