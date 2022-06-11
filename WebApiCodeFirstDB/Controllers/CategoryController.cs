using Microsoft.AspNetCore.Mvc;
using BlogWebApi.Data;
using BlogWebApi.Entites;
using BlogWebApi.Services.Interface;
using BlogWebApi.ViewModel;
using BlogWebApi.ViewModel.Category;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        private BlogDBContext _blogDBContext;

        public CategoryController(ICategoryService categoryService, BlogDBContext blogDBContext)
        {
            _categoryService = categoryService;
            _blogDBContext = blogDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetsAsync([FromQuery]CategoryRequestModel requestModel)
        {
            var postCategories = await _categoryService.GetsAsync(requestModel);
            return Ok(postCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("The Post Category record couldn't be found.");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddCategoryViewModel postCategory)
        {
            if (postCategory == null)
                return BadRequest("Category can not null");

            if(string.IsNullOrWhiteSpace(postCategory.Name))
                return BadRequest("Name can not empty");

            if (string.IsNullOrWhiteSpace(postCategory.Slug))
                return BadRequest("Slug can not empty");


            var categoryId = await _categoryService.AddCagtegoryAsync(postCategory);
            return Ok(categoryId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateCategoryViewModel updateCategory)
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
