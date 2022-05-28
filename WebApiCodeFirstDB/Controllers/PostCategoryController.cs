using Microsoft.AspNetCore.Mvc;
using BlogWebApi.Data;
using BlogWebApi.Models;
using BlogWebApi.Services.Interface;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCategoryController : ControllerBase
    {
        private ICategoryService _categoryService;

        public PostCategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //GET:api/PostCategory
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var postCategories = await _categoryService.GetAllCategoryAsync();
            return Ok(postCategories);
        }

        //GET:api/PostCategory/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("The Post Category record couldn't be found.");
            }
            return Ok(category);
        }

        //POST:api/PostCategory
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PostCategory postCategory)
        {
            if (postCategory == null)
            {
                return BadRequest("Post Category is null.");
            }
            var category = await _categoryService.AddCagtegoryAsync(postCategory);
            return Ok(postCategory.Id);
        }

        //PUT:api/PostCategory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] PostCategory updateCategory)
        {
            if (updateCategory == null)
            {
                return BadRequest("Post Category is null.");
            }

            var categroyId = await _categoryService.UpdateCategoryAsync(id, updateCategory);
            if (categroyId == 0)
            {
                return NotFound("The Post Category record couldn't be found.");
            }
            return Ok();
        }

        // DELETE: api/PostCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var categroyId = await _categoryService.DeleteCategoryAsync(id);
            if (categroyId == 0)
            {
                return NotFound("The Post Category record couldn't be found.");
            }
            return Ok();
        }
    }
}
