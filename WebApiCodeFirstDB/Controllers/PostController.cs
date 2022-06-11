using Microsoft.AspNetCore.Mvc;
using BlogWebApi.Data;
using BlogWebApi.Entites;
using BlogWebApi.Services.Interface;
using BlogWebApi.ViewModel;
using BlogWebApi.ViewModel.Post;

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
        public async Task <IActionResult> GetAllAsync([FromQuery]PostRequestModel postRequestModel)
        {
            //Logic code
            var post = await _postService.GetAllPostAsync(postRequestModel);
            //using: code dispose after {}
            //connect db var context = new BlogDBContext(); connection to database
            //connection to database => slow

            //Controller
            return Ok(post);
        }
        [HttpGet("{id}")]
        public async Task <IActionResult> GetAsync(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound("The Post record couldn't be found.");
            }
            return Ok(post);
        }

        //POST:api/Post
        [HttpPost]
        public async Task <IActionResult> PostAsync([FromBody] AddPostViewModel post)
        {
            if (post == null)
            {
                return BadRequest("Post is null.");
            }
            var newPost = await _postService.AddPostAsync(post);
            return Ok(newPost);
        }

        ////PUT:api/Post/5
        [HttpPut("{id}")]
        public async Task <IActionResult> PutAsync(int id, [FromBody] UpdatePostViewModel updatePost)
        {
            if (updatePost == null)
            {
                return BadRequest("Post is null.");
            }
            var postId = await _postService.UpdatePostAsync(id,updatePost);
            return Ok(postId);
        }
        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteAsync(int id)
        {
            var post = await _postService.DeletePostAsync(id);
            if (post == 0)
            {
                return NotFound("The Post record couldn't be found.");
            }
            return Ok();
        }
    }
}
