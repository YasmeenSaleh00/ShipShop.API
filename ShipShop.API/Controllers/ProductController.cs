using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Commands;
using ShipShop.Application.Queries;
using ShipShop.Application.Services;
using ShipShop.Infrastructure.Repositories;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
          var products =  await _productService.GetAll();
            if(products == null || products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {

            var product = await _productService.GetById(id);
            if(product == null)
            {
                return BadRequest();
            }
            return Ok(product); 
        }
        [HttpGet]
        [Route("GetByFilters")]

        public async Task<IActionResult> GetProductsByFilters([FromQuery] ProductQuery query)
        {
            var products = await _productService.GetProductsByFilters(query);
            if(products == null || products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet]
        [Route("SortByName")]

        public async Task<IActionResult> SortByName(string sortDirection)
        {
            var products = await _productService.SortByName( sortDirection);
            if (products == null || products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet]
        [Route("SortByPrice")]
        public async Task<IActionResult> SortByPrice(string sortDirection)
        {
            var product = await _productService.SortByPrice(sortDirection);
            return Ok(product);

        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }
            await _productService.AddProduct(command);
            return Ok("Product successfully added");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command , int id)
        {
            if (command == null || id == 0 || id <0)
            {
                return BadRequest();
            }
            await _productService.UpdateProduct(command, id);
            return Ok();
        }
      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.Delete(id);
            return Ok();
        }
    }
}
