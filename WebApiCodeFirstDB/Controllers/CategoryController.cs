using Microsoft.AspNetCore.Mvc;
using BlogWebApi.Data;
using BlogWebApi.Entites;
using BlogWebApi.Services.Interface;
using BlogWebApi.ViewModel;
using BlogWebApi.ViewModel.Category;
using Microsoft.EntityFrameworkCore;
using BlogWebApi.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetsAsync([FromQuery]CategoryRequestModel requestModel)
        {
            var postCategories = await _categoryService.GetsAsync(requestModel);
            return Ok(postCategories);
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddCategoryViewModel postCategory)
        {
            if (postCategory == null)
                return BadRequest(new ErrorReturnModel 
                {
                    ErrorCode = "1001",//const
                    ErrorMessage = "Category can not be null"
                }); 

            if (string.IsNullOrWhiteSpace(postCategory.Name))
                return BadRequest(new ErrorReturnModel
                {
                    ErrorCode = "1002",
                    ErrorMessage = "Name can not be empty"
                });

            if (string.IsNullOrWhiteSpace(postCategory.Slug))
                return BadRequest(new ErrorReturnModel
                {
                    ErrorCode = "1003",
                    ErrorMessage = "Slug can not be empty"
                });


            var categoryId = await _categoryService.AddCagtegoryAsync(postCategory);

            if (categoryId == -1)
                return BadRequest(new ErrorReturnModel
                {
                    ErrorCode = "1004",
                    ErrorMessage = "Cagegory is existing"
                });

            return Ok(categoryId);
        }

        [Authorize]
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

        [Authorize]
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
