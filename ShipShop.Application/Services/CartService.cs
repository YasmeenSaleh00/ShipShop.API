using Microsoft.AspNetCore.Http;
using ShipShop.Application.Commands;
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
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IHttpContextAccessor accessor;
        public CartService(ICartRepository cartRepository, IHttpContextAccessor accessor)
        {
            _cartRepository = cartRepository;
            this.accessor = accessor;
        }
        public async Task CreateCart(CartCommand cart)
        {
            var userId = accessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

            // تحقق إذا كانت السلة موجودة بالفعل
            var existingCart = await _cartRepository.GetCartById(cart.CartId);
            if (existingCart == null)
            {
                // إذا السلة غير موجودة، أنشئ واحدة جديدة
                Cart cart1 = new Cart()
                {
                    StatusCartId = 9, // تأكد من أن هذا هو الوضع الصحيح
                  UserId=int.Parse(userId),
                    CartItems = new List<CartItem>(), // تأكد من تهيئة CartItems كمصفوفة فارغة
                };

                // إذا كان لديك منتج لاضافته، قم بإضافته إلى CartItems
                if (cart.ProductId > 0)
                {
                    var product = await _cartRepository.GetProductById(cart.ProductId);
                    if (product != null && product.ProductStatusId == 1) // تحقق من حالة المنتج
                    {
                        var newItem = new CartItem()
                        {
                            ProductId = cart.ProductId,
                            Quantity = cart.Quantity
                        };
                        cart1.CartItems.Add(newItem); // أضف العنصر الجديد إلى CartItems
                    }
                }

                // إضافة السلة إلى قاعدة البيانات
                await _cartRepository.Add(cart1);
            }
        }

        public async Task AddingProductToCart(CartCommand input)
        {
            // تحقق من صحة البيانات
            if (input.ProductId <= 0 || input.Quantity <= 0)
                throw new Exception("ProductId and Quantity must be greater than zero.");

            // تحقق من السلة الموجودة
            var cart = await _cartRepository.GetCartById(input.CartId);
            if (cart == null)
            {
             
                await _cartRepository.Add(cart);
            }

            // تحقق من وجود المنتج
            var product = await _cartRepository.GetProductById(input.ProductId);
            if (product == null || product.ProductStatusId != 1)
                throw new Exception("Product not found or out of stock.");

            // تحقق من وجود المنتج في السلة
            var existingItem = await _cartRepository.IsProductInCartAsync(input.ProductId, cart.Id);

            if (existingItem != null)
            {
                // إذا كان المنتج موجودًا، قم بتحديث الكمية
                existingItem.Quantity += input.Quantity;
                await _cartRepository.UpdateCartItem(existingItem);
            }
            else
            {
                // إضافة منتج جديد للسلة
                var newItem = new CartItem
                {
                    ProductId = input.ProductId,
                    Quantity = input.Quantity
                };

                // إضافة المنتج للسلة
                await _cartRepository.AddingProductToCart(newItem);
            }
        }

    }
}