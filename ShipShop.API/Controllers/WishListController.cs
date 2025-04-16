using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipShop.Application.Commands;
using ShipShop.Application.Services;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly WishListService _wishListService;

        public WishListController(WishListService wishListService)
        {
            _wishListService = wishListService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWishById(int id)
        {
            var wishList = await _wishListService.GetWishList(id);
            if (wishList == null)
            {

                return NotFound();


            }
            return Ok(wishList);
        }
        [HttpGet]
        [Route("get-by-customer/{customerId}")]
        public async Task<IActionResult> GetWishByCustomerId(int customerId)
        {
            var wishList = await _wishListService.GetWishtByCustomerId(customerId);
            if (wishList == null)
            {

                return NotFound();


            }
            return Ok(wishList);
        }
        [HttpPost]
        public async Task<IActionResult> AddToWishList([FromBody] WishListCommand dto)
        {
            try
            {
                await _wishListService.AddProductToWishAsync1(dto.CustomerId, dto.ProductId);
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

        [HttpDelete("{wishId}/{productId}")]
        public async Task<IActionResult> RemoveProductFromWish(int wishId, int productId)
        {
            try
            {
                await _wishListService.RemoveProductFromWishlistAsync(wishId, productId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}

