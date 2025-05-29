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
    public class ProductRepository : IProductRepository
    {
        private readonly ShipShopDbContext _context;

        public ProductRepository(ShipShopDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Product input)
        {
            _context.Products.Add(input);
            await _context.SaveChangesAsync();
            return input.Id;

        }

        public async Task Delete(int id)
        {
            var product = await GetById(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _context.Products
                   .Include(x => x.LookupItem)
                   .Include(x => x.SubCategory)
                   .Include(x => x.Brand)
             
                   .ToListAsync(); return products;
        }

        public async Task<Product> GetById(int id)
        {
            var entity = await _context
                     .Products.Include(x => x.SubCategory)
                     .Include(x=>x.Brand)
                     .Include(x=>x.LookupItem)
                     .FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task<List<Product>> GetProductsByBrand(string brandName)
        {
            var data = await _context.Products.Include(x => x.SubCategory).Include(x => x.Brand).Include(x => x.LookupItem)
                .Where(x =>

                (string.IsNullOrEmpty(brandName) || x.Brand.Name.Contains(brandName)))
                .ToListAsync();
            return data;
        }

        public async Task<List<Product>> GetProductsByFilters(string categoryName)
        {
            var data = await _context.Products.Include(x => x.SubCategory).Include(x=>x.Brand).Include(x=>x.LookupItem)
                .Where(x =>
               
                (string.IsNullOrEmpty(categoryName) || x.SubCategory.Name.Contains(categoryName)))
                .ToListAsync();
            return data;

        }

        public async Task<List<Product>> Search(string name)
        {
            name = name.Trim().ToLower(); 
            return await _context.Products
                .Include(x => x.SubCategory)
                .Include(x => x.Brand)
                .Include(x => x.LookupItem)
                .Where(p =>
                    p.Name.ToLower().Contains(name) ||
                    p.NameAr.ToLower().Contains(name)||
                    p.SubCategory.Name.ToLower().Contains(name)||
                    p.Brand.Name.ToLower().Contains(name))
                    
                .ToListAsync();
        }

        public async Task<List<Product>> SortByCreationDate(string sortDirection)
        {
            IQueryable<Product> query = _context.Products.Include(x=>x.Brand).Include(x=>x.LookupItem).Include(x=>x.SubCategory);
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

        public async Task<List<Product>> SortById(string sortDirection)
        {
            IQueryable<Product> query = _context.Products.Include(x => x.Brand).Include(x => x.LookupItem).Include(x => x.SubCategory);
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

        public async Task<List<Product>> SortByName(string sortDirection)
        {
            IQueryable<Product> query = _context.Products.Include(x => x.Brand).Include(x => x.LookupItem).Include(x => x.SubCategory);
            if (sortDirection == "desc")
            {
                query = query.OrderByDescending(x => x.Name);
            }
            if(sortDirection =="asc")
            {
                query=query.OrderBy(x => x.Name);
            }
            
            return await query.ToListAsync();
        }

        public async Task<List<Product>> SortByPrice(string sortDirection)
        {
            IQueryable<Product> query = _context.Products.Include(x => x.Brand).Include(x => x.LookupItem).Include(x => x.SubCategory);

            if (sortDirection.ToLower() == "desc")
            {
                query = query.OrderByDescending(x => x.Price);
            }
            if(sortDirection.ToLower() =="asc" )
            {
                query = query.OrderBy(x => x.Price);
            }

            return await query.ToListAsync();
        }
 

        public async Task Update(Product input)
        {
            _context.Update(input);
            await _context.SaveChangesAsync();

        }

        
    }
}
