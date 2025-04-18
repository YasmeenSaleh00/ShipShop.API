using Microsoft.AspNetCore.Http;
using ShipShop.Application.Commands;
using ShipShop.Application.Models;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ShipShop.Application.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IHttpContextAccessor accessor;
        public CartService(ICartRepository cartRepository, IHttpContextAccessor accessor)
        {
            _cartRepository = cartRepository;
            this.accessor = accessor;
        }


        public async Task<CartModel> GetCartById(int cartId)
        {
            var cart = await _cartRepository.GetCartById(cartId);

            if (cart == null)
                return null;

            var cartItems = await _cartRepository.GetCartItemById(cartId);

            var model = new CartModel
            {
                Id = cart.Id,
                Items = cartItems.Select(item => new CartItemModel
                {
                    ProductId= item.ProductId,
                    ProductName = item.Product.Name,
                    ImageUrl = $"https://localhost:7057/Images/{item.Product.ImageUrl}",

                    Price = item.Product.Price,
                    Quantity = item.Quantity
                }).ToList()
            };

            return model;

        }
        public async Task<CartModel> GetCartByCustomerId(int customerId)
        {
            var cart=await _cartRepository.GetCartByCustomerAsync(customerId);
            if (cart == null)
                return null;


            var cartItems = await _cartRepository.GetCartItemById(cart.Id);

            var model = new CartModel
            {
                Id = cart.Id,
                Items = cartItems.Select(item => new CartItemModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    ImageUrl = $"https://localhost:7057/Images/{item.Product.ImageUrl}",

                    Price = item.Product.Price,
                    Quantity = item.Quantity
                }).ToList()
            };

            return model;
        }

        
        public async Task RemoveProductFromCartAsync(int cartId, int productId)
        {
            await _cartRepository.RemoveFromCartAsync(cartId, productId);
        }
        public async Task ClearCartAsync(int cartId)
        {
            await _cartRepository.ClearCartAsync(cartId);
        }


        public async Task AddProductToCartAsync1(int customerId, int productId, int quantity)
        {
            var cart = await _cartRepository.GetCartByCustomerAsync(customerId);

            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = customerId,
                    CreatedOn = DateTime.UtcNow,
                    StatusCartId=9
                };

                await _cartRepository.Add(cart); 
            }

            var existingItem = await _cartRepository.IsProductInCartAsync(productId, cart.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                await _cartRepository.UpdateCartItem(existingItem);
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity
                };

                await _cartRepository.AddingProductToCart(newItem);
            }
        }



    }
}