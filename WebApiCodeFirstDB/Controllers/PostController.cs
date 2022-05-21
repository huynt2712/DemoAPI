using Microsoft.AspNetCore.Mvc;
using WebApiCodeFirstDB.Data;
using WebApiCodeFirstDB.Models;
using WebApiCodeFirstDB.ViewModel;

namespace WebApiCodeFirstDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly BlogDBContext _blogDBContext;

        public PostController(BlogDBContext blogDBContext)
        {
            _blogDBContext = blogDBContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var post = new List<PostViewModel>();
            post = _blogDBContext.Posts.Select(p => new PostViewModel
            {
                Id = p.Id,
                Content = p.Content,
                CreatedDate = p.CreatedDate,
                Description = p.Description,
                Title = p.Title,
                Image = p.Image
            }).ToList();
            //using: code dispose after {}
            //connect db var context = new BlogDBContext(); connection to database
            //connection to database => slow

            return Ok(post);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var post = new PostViewModel();
            post = _blogDBContext.Posts
                .Select(p => new PostViewModel
                {
                    Id = p.Id,
                    Content = p.Content,
                    CreatedDate = p.CreatedDate,
                    Description = p.Description,
                    Title = p.Title,
                    Image = p.Image
                })
                .FirstOrDefault(c => c.Id == id);
            if (post == null)
            {
                return NotFound("The Post record couldn't be found.");
            }
            return Ok(post);
        }

        //POST:api/Post
        [HttpPost]
        public IActionResult Post([FromBody] AddPostViewModel post)
        {
            if (post == null)
            {
                return BadRequest("Post is null.");
            }
            var newPost = _blogDBContext.Posts.Add(new Post
            {
                Content = post.Content,
                Title = post.Title,
                Description = post.Description,
                PostCategoryId = post.PostCategoryId,
                Image = post.Image,
                CreatedDate = DateTime.UtcNow
            });
            _blogDBContext.SaveChanges();
            return Ok(newPost?.Entity?.Id);
        }

        ////PUT:api/Post/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdatePostViewModel updatePost)
        {
            if (updatePost == null)
            {
                return BadRequest("Post is null.");
            }


            var post = _blogDBContext.Posts.FirstOrDefault(c => c.Id == id);
            if (post == null)
            {
                return NotFound("The Post record couldn't be found.");
            }
            post.Title = updatePost.Title;
            post.Description = updatePost.Description;
            post.Content = updatePost.Content;
            post.PostCategoryId = updatePost.PostCategoryId;
            post.Image = updatePost.Image;
            post.UpdatedDate = DateTime.UtcNow;
            _blogDBContext.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var post = _blogDBContext.Posts.FirstOrDefault(c => c.Id == id);
            if (post == null)
            {
                return NotFound("The Post record couldn't be found.");
            }
            _blogDBContext.Remove(post);
            _blogDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost("generateData")]
        public IActionResult GenerateData()
        {

            for (var index = 0; index < 5000; index++)
            {
                var postCategory = new PostCategory
                {
                    Name = $"Name {index}",
                    CreateAt = DateTime.UtcNow
                };
                _blogDBContext.Categories.Add(postCategory);
            }
            _blogDBContext.SaveChanges();
            return Ok();
        }
    }
}
