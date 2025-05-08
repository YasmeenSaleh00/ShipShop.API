using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Commands;
using ShipShop.Application.Services;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class SubCategoryController : ControllerBase
    {
        private readonly SubCategoryService _categoryService;

        public SubCategoryController(SubCategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        /// <summary>
        /// This EndPoint To Show All Category
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategory();
            if(categories == null || categories.Count == 0)
            {
                return NoContent();
            } 
            return Ok(categories);
        }
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetAllCategoriesCount()
        {
            var categories = await _categoryService.GetAllCategory();
            if (categories == null || categories.Count == 0)
            {
                return NoContent();
            }
            return Ok(categories.Count);
        }
        [HttpGet]
        [Route("sort-by-name/{sortDirection}")]

        public async Task<IActionResult> SortByName(string sortDirection)
        {
            var subCategories = await _categoryService.SortByName(sortDirection);
            if (subCategories == null || subCategories.Count == 0)
            {
                return NotFound();
            }
            return Ok(subCategories);
        }
        [HttpGet]
        [Route("sort-by-id/{sortDirection}")]
        public async Task<IActionResult> SortById(string sortDirection)
        {
            var subCategories = await _categoryService.SortById(sortDirection);
            return Ok(subCategories);

        }
        [HttpGet]
        [Route("sort-by-creation-date/{sortDirection}")]
        public async Task<IActionResult> SortByCreationDate(string sortDirection)
        {
            var subCategories = await _categoryService.SortByCreationDate(sortDirection);
            return Ok(subCategories);

        }
        /// <summary>
        /// This EndPoint To get Category info
        /// </summary>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            if(id == 0 || id < 0)
            {
                return BadRequest();
            }
            var category = await _categoryService.GetById(id);
            if(category == null)
            {
                return NotFound();
            }
            return Ok(category);

        }
        [HttpGet]
        [Route("get-by-filter/{categoryId}")]
        public async Task<IActionResult> GetSubCategoriesByCategoryId(int categoryId)
        {
            if (categoryId == 0 || categoryId < 0)
            {
                return BadRequest();
            }
            var category = await _categoryService.GetSubCategoriesByCategoryId(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);

        }
        ///<summary>
        ///This EndPoint to add new Category
        ///</summary>
        [HttpPost]
        [Authorize(Roles = "Add")]
        public async Task<IActionResult> AddCategory([FromBody] SubCategoryCommand command)
        {
            var id = await _categoryService.AddCategory(command);
            return Ok();
        }
        /// <summary>
        /// This EndPoint To update Category info
        /// </summary>

        [HttpPut("{id}")]
        [Authorize(Roles = "Edit")]
        public async Task<IActionResult> UpdateCategory([FromRoute]int id, UpdateSubCategoryCommand command)
        {
            if(id == 0 || id < 0)
            {
                return BadRequest();
            }
            var data = await _categoryService.GetById(id);
            if (data == null)
                return NotFound();
            await _categoryService.UpdateCategory(id, command);

            return Ok();
        }
        /// <summary>
        /// This EndPoint To delete Category 
        /// </summary>

        [HttpDelete("{id}")]
        [Authorize(Roles = "Delete")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if(id == 0 || id < 0)
            {
                return BadRequest();
            }
            await _categoryService.DeleteCategory(id);
            return Ok();
        }

    }
}
