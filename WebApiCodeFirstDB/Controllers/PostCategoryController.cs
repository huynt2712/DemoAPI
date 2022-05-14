using Microsoft.AspNetCore.Mvc;
using WebApiCodeFirstDB.Data;
using WebApiCodeFirstDB.Models;

namespace WebApiCodeFirstDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCategoryController : ControllerBase
    {
        //GET:api/PostCategory
        [HttpGet]
        public IActionResult GetAll()
        {
            var postCategories = new List<PostCategory>();
            using (var context = new BlogDBContext())
            {
                postCategories = context.Categories.ToList();
            }
            return Ok(postCategories);
        }

        //GET:api/PostCategory/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = new PostCategory();
            using (var context = new BlogDBContext())
            {
                category = context.Categories.FirstOrDefault(c => c.Id == id);
            }
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
            using (var context = new BlogDBContext())
            {
                context.Categories.Add(postCategory);
                context.SaveChanges();
            }
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

            using (var context = new BlogDBContext())
            {
                var category = context.Categories.FirstOrDefault(c => c.Id == id);
                if (category == null)
                {
                    return NotFound("The Post Category record couldn't be found.");
                }
                category.Name = updateCategory.Name;
                category.Slug = updateCategory.Slug;
                category.UpdateAt = DateTime.UtcNow;
                context.SaveChanges();
            }
            return Ok();
        }

        // DELETE: api/PostCategory/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //test
            using (var context = new BlogDBContext())
            {
                var category = context.Categories
                    .FirstOrDefault(c => c.Id == id);
                //test

                if (category == null)
                {
                    //test
                    return NotFound("The Post Category record couldn't be found.");
                }
                context.Remove(category);
                //test
                context.SaveChanges();
            }
            return Ok();
            //test
        }

        //test
    }
}
