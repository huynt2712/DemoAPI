using Microsoft.AspNetCore.Mvc;
using WebApiCodeFirstDB.Data;
using WebApiCodeFirstDB.Models;

namespace WebApiCodeFirstDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCategoryController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var post = new List<Post>();
            using (var context = new PostContext())
            {
                post = context.Posts.ToList();
            }
            return Ok(post);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = new PostCategory();
            using (var context = new PostContext())
            {
                category = context.Categories.FirstOrDefault(c => c.Id == id);
            }
            if (category == null)
            {
                return NotFound("The Post Category record couldn't be found.");
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult Post([FromBody] PostCategory postCategory)
        {
            if (postCategory == null)
            {
                return BadRequest("Post Category is null.");
            }
            using (var context = new PostContext())
            {
                context.Categories.Add(postCategory);
                context.SaveChanges();
            }
            return Ok(postCategory.Id);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PostCategory updateCategory)
        {
            if (updateCategory == null)
            {
                return BadRequest("Post Category is null.");
            }

            using (var context = new PostContext())
            {
                var category = context.Categories.FirstOrDefault(c => c.Id == id);
                if (category == null)
                {
                    return NotFound("The Post Category record couldn't be found.");
                }
                category.Name = updateCategory.Name;
                category.UpdateAt = DateTime.UtcNow;
                context.SaveChanges();
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var context = new PostContext())
            {
                var category = context.Categories.FirstOrDefault(c => c.Id == id);
                if (category == null)
                {
                    return NotFound("The Post Category record couldn't be found.");
                }
                context.Remove(category);
                context.SaveChanges();
            }
            return Ok();
        }
    }
}
