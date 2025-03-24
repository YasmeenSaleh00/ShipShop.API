using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Commands;
using ShipShop.Application.Services;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandService _brandService;

        public BrandController(BrandService brandService)
        {
            _brandService = brandService;
        }
        /// <summary>
        /// This EndPoint To Show All Brand
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllBrand()
        {
            var brands = await _brandService.GetAllBrand();
            if (brands == null || brands.Count==0)
            {
                return NotFound();
            }
            return Ok(brands);
        }
        /// <summary>
        /// This EndPoint To Show  Brand depend on Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            var brand = await _brandService.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }
        /// <summary>
        /// This EndPoint To Add new  Brand  
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandCommand command)
        {
            if(command == null)
            {
                return BadRequest();
            }
         var id=   await _brandService.AddNewBrand(command);
            return Ok($"Brand with Id {id} was added successfully");
        }
        /// <summary>
        /// This EndPoint To update Brand info
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id ,BrandCommand command)
        {
            var brand = await _brandService.GetById(id);
            if(brand == null)
            {
                return NotFound();
            }
            await _brandService.UpdateBrand(id,command);
            return Ok($"Brand with Id {id} was updated successfully");

        }
        /// <summary>
        /// This EndPoint To delete  Brand
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _brandService.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }
            await _brandService.DeleteBrand(id);
            return Ok($"Brand with Id {id} was deleted successfully");
        }
    }
}
