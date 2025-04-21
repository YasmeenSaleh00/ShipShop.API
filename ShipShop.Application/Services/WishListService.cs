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
            var wish = await _wishlistRepository.GetWishList(id);

            if (wish == null)
                return null;

            var wishitem = await _wishlistRepository.GetWishItemById(id);

            var model = new WishListModel
            {
                Id = wish.Id,
                WishListItems = wishitem.Select(item => new WishListItemModel
                {
                    productId=item.ProductId,   
                    ProductName = item.Product.Name,
                    ImageUrl = $"https://localhost:7057/Images/{item.Product.ImageUrl}",

                    Price = item.Product.Price,
                   ProductStatus=item.Product.LookupItem.Value

                }).ToList()
            };

            return model;
        }

        public async Task RemoveProductFromWishlistAsync(int WishId, int productId)
        {
            await _wishlistRepository.RemoveFromWishlist(WishId, productId);
        }
        public async Task<int> AddProductToWishAsync1(int customerId, int productId)
        {
            var wish = await _wishlistRepository.GetWishtByCustomerAsync(customerId);

            if (wish == null)
            {
                wish = new WishList
                {
                    CustomerId = customerId,
                    CreatedOn = DateTime.UtcNow,

                };

                await _wishlistRepository.Add(wish);
            }
            var existingItem = await _wishlistRepository.IsProductInWishAsync(productId, wish.Id);
            if (existingItem != null)
            {
                throw new Exception("product already exist");
            }
            else
            {
                var newItem = new WishListItem
                {
                    WishListId = wish.Id,
                    ProductId = productId,


                };
                await _wishlistRepository.AddingProductToWishList(newItem); 
            }
            return wish.Id; 

        }
        public async Task<WishListModel> GetWishtByCustomerId(int customerId)
        {
            var wishList = await _wishlistRepository.GetWishtByCustomerAsync(customerId);
            if (wishList == null)
                return null;


            var wishItems = await _wishlistRepository.GetWishItemById(wishList.Id);

            var model = new WishListModel
            {
                Id = wishList.Id,
                WishListItems = wishItems.Select(item => new WishListItemModel
                {
                    productId = item.ProductId,
                    ProductName = item.Product.Name,
                    ImageUrl = $"https://localhost:7057/Images/{item.Product.ImageUrl}",

                    Price = item.Product.Price,
                    ProductStatus = item.Product.LookupItem.Value

                }).ToList()
            };

            return model;
        }
    }
}
