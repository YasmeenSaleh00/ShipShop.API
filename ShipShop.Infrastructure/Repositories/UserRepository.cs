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
    public class UserRepository : IUserRepository
    {
        private readonly ShipShopDbContext _context;

        public UserRepository(ShipShopDbContext context)
        {
            _context = context;
        }

        public async Task CreateNewUser(User user)
        {
            _context.Users.Add(user);   
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetById(id);
         _context.Remove(user);
            await _context.SaveChangesAsync();  
     
        }


        public async Task<List<User>> GetAll()
        {

            var users = await _context.Users
                                      .Where(x => !(x is Customer))
                                      .Include(x => x.Role)
                                      .ToListAsync();

            return users;

        }
    

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<List<User>> SortUserByCreateOn(string sortDirection)
        {
            IQueryable<User> query = _context.Users
                                      .Where(x => !(x is Customer))
                                      .Include(x => x.Role);


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

        public async Task<List<User>> SortUserByEmail(string sortDirection)
        {

            IQueryable<User> query = _context.Users
                                      .Where(x => !(x is Customer))
                                      .Include(x => x.Role);


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

        public async Task<List<User>> SortUserById(string sortDirection)
        {


            IQueryable<User> query = _context.Users
                                      .Where(x => !(x is Customer))
                                      .Include(x => x.Role);


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

        public async Task<List<User>> SortUserByName(string sortDirection)
        {

            IQueryable<User> query = _context.Users
                                      .Where(x => !(x is Customer))
                                      .Include(x => x.Role);


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

        public async Task Update(User customer)
        {
            _context.Users.Update(customer);
            await _context.SaveChangesAsync();
        }

      
    }
}
