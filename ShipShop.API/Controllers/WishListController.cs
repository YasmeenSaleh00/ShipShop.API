using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        [Route("[action]/{customerId}")]
        public async Task<IActionResult> GetWishlistItems(int customerId)
        {
            var items = await _wishListService.GetWishlistItems(customerId);
            if (items == null || items.Count == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWishList(int id)
        {
            var wishlist = await _wishListService.GetWishList(id);
            if (wishlist == null)
            {
                return NotFound();
            }
            return Ok(wishlist);
        }
        [HttpDelete("{wishlistId}/{productId}")]
        public async Task<IActionResult> RemoveFromWishlist(int wishlistId, int productId)
        {
            await _wishListService.RemoveFromWishlist(wishlistId, productId);
            return Ok("Deleted Successfully");
        }
    }
}

