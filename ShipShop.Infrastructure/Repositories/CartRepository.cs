using Microsoft.EntityFrameworkCore;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using ShipShop.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ShipShopDbContext _context;

        public CartRepository(ShipShopDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartById(int cartId)
        {
          var cart= await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);
            return cart;
        }

        public async Task<Product> GetProductById(int productId)
        {

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return product;
        }

        public async Task<CartItem> IsProductInCartAsync(int productId, int cartId)
        {
            var sh = await _context.CartItems
                 .FirstOrDefaultAsync(x => x.ProductId == productId && x.CartId == cartId);
            return sh;
        }

        public async Task UpdateCartItem(CartItem item)
        {
            _context.CartItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task AddingProductToCart(CartItem item)
        {
            await _context.CartItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public Task RemoveFromCartAsync(int cartId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetCartByCustomerAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public async Task Add(Cart cart)
        {
           _context.Add(cart);
            await _context.SaveChangesAsync();
        }
    }
}