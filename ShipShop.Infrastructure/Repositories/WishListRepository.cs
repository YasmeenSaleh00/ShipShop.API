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

        public async Task Add(WishList wishList)
        {
            _context.Add(wishList);
            await _context.SaveChangesAsync();
        }

        public async Task AddingProductToWishList(WishListItem item)
        {
            await _context.WishListItems.AddAsync(item);
            await _context.SaveChangesAsync();
           
        }

    

        public async Task<List<WishListItem>> GetWishItemById(int wishlistId)
        {
            var wish = await _context.WishListItems
                .Include(x=>x.Product).ThenInclude(x => x.LookupItem).Where(x=>x.WishListId == wishlistId ).ToListAsync();    
            return wish;
        }

        public async Task<WishList> GetWishList(int id)
        {
            var wish = await _context.WishLists
                   .Include(x => x.WishListItems).ThenInclude(x => x.Product).ThenInclude(x=>x.LookupItem).FirstOrDefaultAsync(x => x.Id == id);
            return wish;
        }

        public async Task<WishList> GetWishtByCustomerAsync(int customerId)
        {
            var wish = await _context.WishLists
          .Include(c => c.WishListItems)
              .ThenInclude(ci => ci.Product)
          .Include(c => c.Customer)
          .FirstOrDefaultAsync(c => c.CustomerId == customerId);
            return wish;
        }

        public async Task<WishListItem> IsProductInWishAsync(int productId, int wishId)
        {

            var existing = await _context.WishListItems
                 .FirstOrDefaultAsync(x => x.ProductId == productId && x.WishListId == wishId);
            return existing;
        }

        public async Task RemoveFromWishlist(int wishlistId, int productId)
        {
            var wishlist= await _context.WishListItems.FirstOrDefaultAsync(x=>x.WishListId==wishlistId && x.ProductId == productId);
            _context.WishListItems.Remove(wishlist);
            await _context.SaveChangesAsync();  
        
        }
    }
}
