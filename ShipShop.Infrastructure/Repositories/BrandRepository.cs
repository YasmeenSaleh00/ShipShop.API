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

        public async Task<List<Brand>> SortByCreationDate(string sortDirection)
        {
            IQueryable<Brand> query = _context.Brands;
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

        public async Task<List<Brand>> SortById(string sortDirection)
        {
            IQueryable<Brand> query = _context.Brands;
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

        public async Task<List<Brand>> SortByName(string sortDirection)
        {
            IQueryable<Brand> query = _context.Brands;
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

        public async Task Update(Brand input)
        {
            _context.Update(input);
            await _context.SaveChangesAsync();
           
        }
    }
}
