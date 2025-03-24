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

        public async Task Singup(User user)
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

        public async Task<List<User>> FilterUserByRole(string roleName)
        {
            if (roleName == null) throw new ArgumentNullException(nameof(roleName));

            

            var users = await _context.Users
                .Include(x => x.Role)
                .Where(x => x.Role.Name.ToLower().Contains(roleName.ToLower())) 
                .AsNoTracking()
                .ToListAsync();
       

            return users;
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _context.Users
               .Include(x => x.Role)
                .Include(x => x.LookupItem)
            
               .ToListAsync();
            return users;

        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .Include(x=>x.LookupItem)
          
                .FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<User> Login(string username, string password)
        {
       
                var user = await _context.Users.Include(x=>x.Role).FirstOrDefaultAsync(x => x.Email == username && x.Password == password);
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

        public async Task UpdatePassword(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();  

        }
        public async Task Update(User customer)
        {
            _context.Users.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.Include(X=>X.Role).FirstOrDefaultAsync(x=>x.Email == email);
            return user;
        }
    }
}
