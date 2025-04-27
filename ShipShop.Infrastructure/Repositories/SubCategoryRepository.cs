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
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly ShipShopDbContext _context;

        public SubCategoryRepository(ShipShopDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(SubCategory input)
        {
          await  _context.SubCategories.AddAsync(input);
            await _context.SaveChangesAsync();
            return input.Id;
        }

        public async Task Delete(int id)
        {
            var category = await GetById(id);
            _context.Remove(category);
            await _context.SaveChangesAsync();

        }

        public async Task<List<SubCategory>> GetAll()
        {
            var categories = await _context.SubCategories.Include(x=>x.Category).AsNoTracking().ToListAsync();
            return categories;
        }

        public async Task<SubCategory> GetById(int id)
        {
            var category = await _context.SubCategories.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
            return category;
        }

        public async Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            var category = await _context.SubCategories.Include(x => x.Category).AsNoTracking().Where(x => x.CategoryId == categoryId).ToListAsync();
            return category;

        }

        public async Task<List<SubCategory>> SortByCreationDate(string sortDirection)
        {
            IQueryable<SubCategory> query = _context.SubCategories.Include(x => x.Category);
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

        public async Task<List<SubCategory>> SortById(string sortDirection)
        {
            IQueryable<SubCategory> query = _context.SubCategories.Include(x => x.Category);
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

        public async Task<List<SubCategory>> SortByName(string sortDirection)
        {
            IQueryable<SubCategory> query = _context.SubCategories.Include(x => x.Category);

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

        public async Task Update(SubCategory input)
        {

            _context.SubCategories.Update(input);
            await _context.SaveChangesAsync();
        }
    }
}
