using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartById(int cartId);
        Task<Product> GetProductById(int productId);
        Task<CartItem> IsProductInCartAsync(int productId, int cartId);
        Task UpdateCartItem(CartItem item);
        Task Add(Cart cart);
        Task RemoveFromCartAsync(int cartId,int productId);
        Task<Cart> GetCartByCustomerAsync(int customerId);
        Task AddingProductToCart(CartItem item);
      
    }
}
