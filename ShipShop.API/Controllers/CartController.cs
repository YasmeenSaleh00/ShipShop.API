using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipShop.Application.Commands;
using ShipShop.Application.Services;
using ShipShop.Core.Entities;

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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartById(int id)
        {
            var cart = await _cartService.GetCartById(id);
            if (cart == null)
            {
                
                    return NotFound();
              
           
            }
            return Ok(cart);
        }

        [HttpGet]
        [Route("get-by-customer/{customerId}")]
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
        public async Task<IActionResult> AddToCart([FromBody] CartCommand dto)
        {
            try
            {
                await _cartService.AddProductToCartAsync1(dto.CustomerId, dto.ProductId, dto.Quantity);
                return Ok();
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

    }
}
