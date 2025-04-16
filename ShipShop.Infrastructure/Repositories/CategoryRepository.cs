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

        public async Task<List<Category>> SortByCreationDate(string sortDirection)
        {
            IQueryable<Category> query = _context.Categories;
            if (sortDirection == "desc")
            {
                query = query.OrderByDescending(x => x.CreatedOn);
            }
            if (sortDirection == "asc")
            {
                query = query.OrderBy(x => x.CreatedOn);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Category>> SortById(string sortDirection)
        {
            IQueryable<Category> query = _context.Categories;
            if (sortDirection == "desc")
            {
                query = query.OrderByDescending(x => x.Id);
            }
            if (sortDirection == "asc")
            {
                query = query.OrderBy(x => x.Id);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Category>> SortByName(string sortDirection)
        {
            IQueryable<Category> query = _context.Categories;

            if (sortDirection == "desc")
            {
                query = query.OrderByDescending(x => x.Name);
            }
            if (sortDirection == "asc")
            {
                query = query.OrderBy(x => x.Name);
            }

            return await query.ToListAsync();
        }

        public async Task Update(Category input)
        {

            _context.Categories.Update(input);
            await _context.SaveChangesAsync();
        }
    }
}
