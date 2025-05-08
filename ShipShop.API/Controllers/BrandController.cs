using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetAllBrandCount()
        {
            var brands = await _brandService.GetAllBrand();
            if (brands == null || brands.Count == 0)
            {
                return NotFound();
            }
            return Ok(brands.Count);
        }
        [HttpGet]
        [Route("sort-by-id/{sortDirection}")]
        public async Task<IActionResult> SortById(string sortDirection)
        {
            var brand = await _brandService.SortById(sortDirection);
            return Ok(brand);

        }
        [HttpGet]
        [Route("sort-by-creation-date/{sortDirection}")]
        public async Task<IActionResult> SortByCreationDate(string sortDirection)
        {
            var brand = await _brandService.SortByCreationDate(sortDirection);
            return Ok(brand);

        }
        [HttpGet]
        [Route("sort-by-name/{sortDirection}")]

        public async Task<IActionResult> SortByName(string sortDirection)
        {
            var brand = await _brandService.SortByName(sortDirection);
            if (brand == null || brand.Count == 0)
            {
                return NotFound();
            }
            return Ok(brand);
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
        [Authorize(Roles = "Add")]
        public async Task<IActionResult> AddBrand(BrandCommand command)
        {
            if(command == null)
            {
                return BadRequest();
            }
         var id=   await _brandService.AddNewBrand(command);
            return Ok();
        }
        /// <summary>
        /// This EndPoint To update Brand info
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Edit")]
        public async Task<IActionResult> UpdateBrand(int id ,BrandCommand command)
        {
            var brand = await _brandService.GetById(id);
            if(brand == null)
            {
                return NotFound();
            }
            await _brandService.UpdateBrand(id,command);
            return Ok();

        }
        /// <summary>
        /// This EndPoint To delete  Brand
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Delete")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _brandService.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }
            await _brandService.DeleteBrand(id);
            return Ok();
        }
    }
}
