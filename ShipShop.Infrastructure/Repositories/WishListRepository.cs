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
    public class WishListRepository : IWishlistRepository
    {
        private readonly ShipShopDbContext _context;

        public WishListRepository(ShipShopDbContext context)
        {
            _context = context;
        }

        public async Task AddToWishlist(WishList list)
        {
          _context.Add(list);
            await _context.SaveChangesAsync();
        }

        public async Task<WishList> GetWishList(int id)
        {
           var wishlist = await _context.WishLists.Include(x=>x.Product).FirstOrDefaultAsync(x=>x.Id == id);
            return wishlist;
        }

        public async Task<List<Product>> GetWishlistItems(int customerId)
        {
            var products = await _context.WishLists
                             .Where(w => w.CustomerId == customerId)
                             .Select(w => w.Product) 
                             .ToListAsync();
            return products;
        }

        public async Task RemoveFromWishlist(int wishlistId, int productId)
        {
            var item = await _context.WishLists
                                    .FirstOrDefaultAsync(w => w.Id == wishlistId && w.ProductId == productId);

            _context.WishLists.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
