using Microsoft.AspNetCore.Http;
using ShipShop.Application.Models;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Services
{
    public class WishListService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IHttpContextAccessor accessor;

        public WishListService(IWishlistRepository wishlistRepository, IHttpContextAccessor accessor)
        {
            _wishlistRepository = wishlistRepository;
            this.accessor = accessor;
        }

        public async Task<WishListModel> GetWishList(int id)
        {
            var wishList = await _wishlistRepository.GetWishList(id);
            if(wishList == null)
            {
                return null;
            }
            WishListModel wishListModel = new WishListModel();
            wishListModel.Id = id;
            wishListModel.PrpductName = wishList.Product.Name;
            wishListModel.UnitPrice=wishList.Product.Price;
            wishListModel.ProductStatus=wishList.Product.LookupItem.Value;  
            wishListModel.CreatedOn=wishList.CreatedOn.ToShortDateString();

            return wishListModel;

        }
        public async Task<List<WishListModel>> GetWishlistItems(int customerId)
        {
            //var customerId =int.Parse( accessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);

         
            var products = await _wishlistRepository.GetWishlistItems(customerId);
            List < WishListModel> wishListModel = new List<WishListModel> ();
            wishListModel = products.Select(x => new WishListModel
            {
                Id=x.Id,
                PrpductName=x.Name,
                UnitPrice=x.Price,
                ProductStatus=x.LookupItem.Value,   
                CreatedOn=x.CreatedOn.ToShortDateString(),  

            }).ToList();  
            return wishListModel;   
          
        }
        public async Task RemoveFromWishlist(int wishlistId, int productId)
        {
            _wishlistRepository.RemoveFromWishlist(wishlistId, productId);  
        }


    }
}
