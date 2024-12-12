using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;
using ProductsAPI.Services;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            var res = await _categoryService.GetCategoryById(id);

            if(res.Success)
            {
                return Ok(res.Category);
            }
            else
            {
                return NotFound(res.Error);
            }
        }

        [HttpPost("{name}")]
        public async Task<ActionResult> CreateCategory(string name)
        {                       
            if(!String.IsNullOrWhiteSpace(name))
            {
                var category = new Category(name);
                var res = await _categoryService.CreateCategory(category);
                return CreatedAtRoute("CreateCategory", res);
            }
            else
            {
                return BadRequest("Failed to create category: Name cannot empty");
            }
        }

        [HttpPut("{id, updatedName}")]
        public async Task<ActionResult> UpdateCategory(int id, string updatedName)
        {
            if(!String.IsNullOrWhiteSpace(updatedName))
            {
                var updatedCategory = new Category(id, updatedName);

                var res = await _categoryService.UpdateCategory(id, updatedCategory);

                if (res.Success)
                {
                    return Ok(res.UpdatedCategory);
                }
                else
                {
                    return NotFound(res.Error);
                }
            }
            else
            {
                return BadRequest("Failed to update category: Name cannot be empty");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var res = await _categoryService.DeleteCategory(id);

            if (res.Success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(res.Error);
            }
        }
    }
}
