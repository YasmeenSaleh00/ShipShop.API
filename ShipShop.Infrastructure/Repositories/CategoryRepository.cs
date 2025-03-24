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
    public class CategoryRepository : IRepository<Category>
    {
        private readonly ShipShopDbContext _context;

        public CategoryRepository(ShipShopDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Category input)
        {
          await  _context.Categories.AddAsync(input);
            await _context.SaveChangesAsync();
            return input.Id;
        }

        public async Task Delete(int id)
        {
            var category = await GetById(id);
            _context.Remove(category);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Category>> GetAll()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            return categories;
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
            return category;
        }

        public async Task Update(Category input)
        {

            _context.Categories.Update(input);
            await _context.SaveChangesAsync();
        }
    }
}
