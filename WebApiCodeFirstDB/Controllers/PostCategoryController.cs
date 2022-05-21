using Microsoft.AspNetCore.Mvc;
using WebApiCodeFirstDB.Data;
using WebApiCodeFirstDB.Models;

namespace WebApiCodeFirstDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCategoryController : ControllerBase
    {
        private readonly BlogDBContext _blogDBContext;

        public PostCategoryController(BlogDBContext blogDBContext)
        {
            _blogDBContext = blogDBContext;
        }

        //GET:api/PostCategory
        [HttpGet]
        public IActionResult GetAll()
        {
            var postCategories = new List<PostCategory>();
            postCategories = _blogDBContext.Categories.ToList();
            return Ok(postCategories);
        }

        //GET:api/PostCategory/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = new PostCategory();
            category = _blogDBContext.Categories.FirstOrDefault(c => c.Id == id);
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

            _blogDBContext.Categories.Add(postCategory);
            _blogDBContext.SaveChanges();
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

            var category = _blogDBContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound("The Post Category record couldn't be found.");
            }
            category.Name = updateCategory.Name;
            category.Slug = updateCategory.Slug;
            category.UpdateAt = DateTime.UtcNow;
            _blogDBContext.SaveChanges();
            return Ok();
        }

        // DELETE: api/PostCategory/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _blogDBContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound("The Post Category record couldn't be found.");
            }
            _blogDBContext.Remove(category);
            _blogDBContext.SaveChanges();
            return Ok();
        }
    }
}
