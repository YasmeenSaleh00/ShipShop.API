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
    public class AuthRepository : IAuthRepository
    {
        private readonly ShipShopDbContext _context;

        public AuthRepository(ShipShopDbContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string username, string password)
        {

            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == username && x.Password == password);
            return user;


        }

    }
}
