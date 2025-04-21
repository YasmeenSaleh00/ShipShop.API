using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipShop.Application.Commands;
using ShipShop.Application.Services;
using ShipShop.Core.Entities;
using System.Security.Claims;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }
   
        [HttpGet("{cartId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCartById(int cartId)
        {
            var cart = await _cartService.GetCartById(cartId);
            if (cart == null)
            {
                
                    return NotFound();
              
           
            }
            return Ok(cart);
        }

        [HttpGet]
        [Route("get-by-customer/{customerId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCartByCustomerId(int customerId)
        {
            var cart = await _cartService.GetCartByCustomerId(customerId);
            if (cart == null)
            {

                return NotFound();


            }
            return Ok(cart);
        }

     

        [HttpDelete("{cartId}/{productId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult>  RemoveProductFromCartAsync(int cartId, int productId)
        {
            try
            {
                await _cartService.RemoveProductFromCartAsync(cartId, productId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("clear/{cartId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ClearCart(int cartId)
        {
            try
            {
                await _cartService.ClearCartAsync(cartId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddToCart([FromBody] CartCommand dto)
        {
            try
            {
                 

            var cart=    await _cartService.AddProductToCartAsync(dto.customerId, dto.ProductId, dto.Quantity);
                return Ok(cart);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Database error: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("UpdateQuantity")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateCartItemCommand dto)
        {
            try
            {
                await _cartService.UpdateCartItemQuantityAsync(dto.CustomerId,dto.ProductId, dto.Quantity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


    }
}
