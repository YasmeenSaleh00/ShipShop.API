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
            // نبحث عن المستخدم ونشمل كل الـ navigation properties الممكنة حسب نوعه
            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == username && x.Password == password);

            if (user == null)
                return null;

            if (user is Customer customer)
            {
                // التحقق من أن الحساب مفعّل
                if (!customer.IsActive)
                    throw new Exception("Your account has been banned");

                // تحميل تفاصيل إضافية
                await _context.Entry(customer).Collection(c => c.Carts).LoadAsync();
                await _context.Entry(customer).Collection(c => c.WishList).LoadAsync();
                return customer;
            }

            return user;
        }

    }

}

