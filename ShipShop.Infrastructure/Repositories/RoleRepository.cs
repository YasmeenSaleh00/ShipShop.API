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
    public class RoleRepository : IRoleRepository
    {
        private readonly ShipShopDbContext _context;

        public RoleRepository(ShipShopDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Role input)
        {
           _context.Roles.Add(input);
            await _context.SaveChangesAsync();
            return input.Id;
        }

     

    

        public async Task Delete(int id)
        {
            var role = await GetById(id);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            
        }

        public async Task<List<Role>> GetAll()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;   
 
        }

        public async Task<Role> GetById(int id)
        {
            var role = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
            return role;
        }

   

        public async Task Update(Role input)
        {
            _context.Roles.Update(input);
            await _context.SaveChangesAsync();
            
        }
       

    }
}
