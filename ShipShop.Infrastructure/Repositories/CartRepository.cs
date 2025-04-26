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
            var cart = await _context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefaultAsync(c => c.Id == cartId);
            return cart;
        }



        public async Task<CartItem> IsProductInCartAsync(int productId, int cartId)
        {
            var sh = await _context.CartItems
                 .FirstOrDefaultAsync(x => x.ProductId == productId && x.CartId == cartId);
            return sh;
        }
        public async Task UpdateCartItemQuantityAsync(int customerId, int productId, int quantity)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.ProductId == productId);
            _context.Update(cartItem);
            await _context.SaveChangesAsync();
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

        public async Task RemoveFromCartAsync(int cartId, int productId)
        {
            var cartItem = await _context.CartItems
        .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

        }

        public async Task<Cart> GetCartByCustomerAsync(int customerId)
        {
            var cart = await _context.Carts
        .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
        .Include(c => c.Customer)
        .FirstOrDefaultAsync(c => c.CustomerId == customerId);
            return cart;
        }

        public async Task Add(Cart cart)
        {
            _context.Add(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetCartItemById(int cartId)
        {
            var cartItems = await _context.CartItems
        .Include(x => x.Product)
        .Where(x => x.CartId == cartId)
        .ToListAsync();
            return cartItems;
        }



        public async Task ClearCartAsync(int cartId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);  

                _context.Carts.Remove(cart);  

                await _context.SaveChangesAsync(); 
            }
        }


    }
}