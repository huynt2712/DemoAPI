using Microsoft.AspNetCore.Mvc;
using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Services.Interface;

namespace BlogApi.Controllers
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
        public IActionResult GetAll()
        {
            var postCategories = _categoryService.GetAllCategory();
            return Ok(postCategories);
        }

        //GET:api/PostCategory/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("The Post Category record couldn't be found.");
            }
            return Ok(category);
        }

        //POST:api/PostCategory
        [HttpPost]
        public IActionResult Post([FromBody] PostCategory postCategory)
        {
            if (postCategory == null)
            {
                return BadRequest("Post Category is null.");
            }
            var category = _categoryService.AddCagtegory(postCategory);
            return Ok(postCategory.Id);
        }

        //PUT:api/PostCategory/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PostCategory updateCategory)
        {
            if (updateCategory == null)
            {
                return BadRequest("Post Category is null.");
            }

            var categroyId = _categoryService.UpdateCategory(id, updateCategory);
            if (categroyId == 0)
            {
                return NotFound("The Post Category record couldn't be found.");
            }
            return Ok();
        }

        // DELETE: api/PostCategory/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categroyId = _categoryService.DeleteCategory(id);
            if (categroyId == 0)
            {
                return NotFound("The Post Category record couldn't be found.");
            }
            return Ok();
        }

        //test
    }
}
