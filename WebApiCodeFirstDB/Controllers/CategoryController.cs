﻿using Microsoft.AspNetCore.Mvc;
using BlogWebApi.Data;
using BlogWebApi.Models;
using BlogWebApi.Services.Interface;
using BlogWebApi.ViewModel;
using BlogWebApi.ViewModel.Category;

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
            {
                return BadRequest("Post Category is null.");
            }
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