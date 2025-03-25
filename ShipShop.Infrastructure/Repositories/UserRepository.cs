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

       

        public async Task DeleteAsync(int id)
        {
            var user = await GetById(id);
         _context.Remove(user);
            await _context.SaveChangesAsync();  
     
        }


        public async Task<List<User>> GetAll()
        {
            var users = await _context.Users
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
            var user = await _context.Users.Include(x => x.Role).AsNoTracking().ToListAsync();
            if (sortDirection == "asc")
            {
                user = user.OrderBy(x => x.CreatedOn).ToList();
            }
            if(sortDirection == "desc")
            {
                user = user.OrderByDescending(x => x.CreatedOn).ToList();
            }
            return user;
        }

  
        public async Task Update(User customer)
        {
            _context.Users.Update(customer);
            await _context.SaveChangesAsync();
        }

      
    }
}
