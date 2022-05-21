using Microsoft.AspNetCore.Mvc;
using BlogWebApi.Data;
using BlogWebApi.Models;
using BlogWebApi.Services.Interface;
using BlogWebApi.ViewModel;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private IPostService _postService;
        public PostController (IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            //Logic code
            var post = _postService.GetAllPost();
            //using: code dispose after {}
            //connect db var context = new BlogDBContext(); connection to database
            //connection to database => slow

            //Controller
            return Ok(post);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var post = _postService.GetPostById(id);
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
            var newPost = _postService.AddPost(post);
            return Ok();
        }

        ////PUT:api/Post/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdatePostViewModel updatePost)
        {
            if (updatePost == null)
            {
                return BadRequest("Post is null.");
            }

            var post = _postService.UpdatePost(id,updatePost);
           if (post == 0)
            {
                return NotFound("The Post record couldn't be found.");
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var post = _postService.DeletePost(id);
            if (post == 0)
            {
                return NotFound("The Post Category record couldn't be found.");
            }
            return Ok();
        }
    }
}
