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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShipShopDbContext _context;

        public CustomerRepository(ShipShopDbContext context)
        {
            _context = context;
        }


        public async Task<List<Customer>> GetAll()
        {
            var users = await _context.Customers
               .Include(x => x.Role)
                .Include(x => x.LookupItem)

               .ToListAsync();
            return users;
        }

        public async Task<Customer> GetById(int id)
        {
            var user = await _context.Customers
               .Include(x => x.Role)
               .Include(x => x.LookupItem)

               .FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<Customer> GetUserByEmail(string email)
        {
            var user = await _context.Customers.Include(X => X.Role).Include(x=>x.LookupItem).FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task Singup(Customer user)
        {
            _context.Customers.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Customer customer)
        {

            _context.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePassword(Customer user)
        {
            _context.Customers.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var user = await GetById(id);
            _context.Remove(user);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Customer>> SortCustomerByCreateOn(string sortDirection)
        {
       

            IQueryable<Customer> query = _context.Customers.Include(x=>x.LookupItem);

         
             if (sortDirection.ToLower() == "desc")
            {
                query = query.OrderByDescending(x => x.CreatedOn);
            }
            if (sortDirection.ToLower() == "asc")
            {
                query = query.OrderBy(x => x.CreatedOn);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Customer>> SortCustomerByName(string sortDirection)
        {
            IQueryable<Customer> query = _context.Customers.Include(x => x.LookupItem);


            if (sortDirection.ToLower() == "desc")
            {
                query = query.OrderByDescending(x => x.FirstName);
            }
            if (sortDirection.ToLower() == "asc")
            {
                query = query.OrderBy(x => x.FirstName);
            }

            return await query.ToListAsync();

        }

        public async Task<List<Customer>> SortCustomerByEmail(string sortDirection)
        {
            IQueryable<Customer> query = _context.Customers.Include(x => x.LookupItem);


            if (sortDirection.ToLower() == "desc")
            {
                query = query.OrderByDescending(x => x.Email);
            }
            if (sortDirection.ToLower() == "asc")
            {
                query = query.OrderBy(x => x.Email);
            }

            return await query.ToListAsync();

        }

        public async Task<List<Customer>> SortCustomerById(string sortDirection)
        {
            IQueryable<Customer> query = _context.Customers.Include(x => x.LookupItem);


            if (sortDirection.ToLower() == "desc")
            {
                query = query.OrderByDescending(x => x.Id);
            }
            if (sortDirection.ToLower() == "asc")
            {
                query = query.OrderBy(x => x.Id);
            }

            return await query.ToListAsync();
        }
    }
}
