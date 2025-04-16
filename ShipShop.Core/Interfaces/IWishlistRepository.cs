using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Interfaces
{
    public interface IWishlistRepository
    {

        Task<WishList> GetWishtByCustomerAsync(int customerId);
        Task RemoveFromWishlist(int wishlistId, int productId);
        Task<WishList> GetWishList(int id);
        Task AddingProductToWishList(WishListItem item);
        Task Add(WishList wishList);
        Task<WishListItem> IsProductInWishAsync(int productId, int wishId);
        Task<List<WishListItem>> GetWishItemById(int wishlistId);

    }
}
