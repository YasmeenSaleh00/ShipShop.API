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
        Task AddToWishlist(WishList list);
        Task<List<Product>> GetWishlistItems(int customerId);
        Task RemoveFromWishlist(int customerId, int productId);
        Task<WishList> GetWishList(int id);     

    }
}
