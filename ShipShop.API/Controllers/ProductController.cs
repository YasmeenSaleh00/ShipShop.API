using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            var products = await _productService.GetAll();
            if (products == null || products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);

        }
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetAllProductCount()
        {
            var products = await _productService.GetAll();
            if (products == null || products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products.Count);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {

            var product = await _productService.GetById(id);
            if (product == null)
            {
                return BadRequest();
            }
            return Ok(product);
        }
        [HttpGet]
        [Route("GetByFilters/{categoryName}")]

        public async Task<IActionResult> GetProductsByFilters(string categoryName)
        {
            var products = await _productService.GetProductsByFilters(categoryName);
            if(products == null || products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet]
        [Route("GetByBrand/{brandName}")]

        public async Task<IActionResult> GetProductsByBrand(string brandName)
        {
            var products = await _productService.GetProductsBrand(brandName);
            if (products == null || products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet]
        [Route("sort-by-name/{sortDirection}")]

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
        [Route("sort-by-price/{sortDirection}")]
        public async Task<IActionResult> SortByPrice(string sortDirection)
        {
            var product = await _productService.SortByPrice(sortDirection);
            return Ok(product);

        }
        [HttpGet]
        [Route("sort-by-id/{sortDirection}")]
        public async Task<IActionResult> SortById(string sortDirection)
        {
            var product = await _productService.SortById(sortDirection);
            return Ok(product);

        }
        [HttpGet]
        [Route("sort-by-creation-date/{sortDirection}")]
        public async Task<IActionResult> SortByCreationDate(string sortDirection)
        {
            var product = await _productService.SortByCreationDate(sortDirection);
            return Ok(product);

        }
        [HttpGet]
        [Route("search/{name}")]
        public async Task<IActionResult> SearchProducts(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("The name is required");

            var products = await _productService.Search(name);
            return Ok(products);
        }

        [HttpPost]
        [Authorize(Roles = "Add,Admin")]
        public async Task<IActionResult> CreateProduct(ProductCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }
            await _productService.AddProduct(command);
            return Ok();
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Edit,Admin")]
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
        [Authorize(Roles = "Delete,Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.Delete(id);
            return Ok();
        }
    }
}







          