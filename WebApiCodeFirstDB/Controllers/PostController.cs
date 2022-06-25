using Microsoft.AspNetCore.Mvc;
using BlogWebApi.Data;
using BlogWebApi.Entites;
using BlogWebApi.Services.Interface;
using BlogWebApi.ViewModel;
using BlogWebApi.ViewModel.Post;
using BlogWebApi.ViewModel.Common;

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
            var post = await _postService.GetAllPostAsync(postRequestModel);
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
                return BadRequest(new ErrorReturnModel
                {
                    ErrorCode = "1001",
                    ErrorMessage = "Post can not be null"
                });

            if (string.IsNullOrWhiteSpace(post.Title))
                return BadRequest(new ErrorReturnModel
                {
                    ErrorCode = "1002",
                    ErrorMessage = "Title can not be empty"
                });

            if (string.IsNullOrWhiteSpace(post.Description))
                return BadRequest(new ErrorReturnModel
                {
                    ErrorCode = "1003",
                    ErrorMessage = "Description can not be empty"
                });

            if (string.IsNullOrWhiteSpace(post.Content))
                return BadRequest(new ErrorReturnModel
                {
                    ErrorCode = "1004",
                    ErrorMessage = "Content can not be empty"
                });

            if (string.IsNullOrWhiteSpace(post.Slug))
                return BadRequest(new ErrorReturnModel
                {
                    ErrorCode = "1006",
                    ErrorMessage = "Slug can not be empty"
                });

            var newPost = await _postService.AddPostAsync(post);

            if (newPost == -2)
                return BadRequest(new ErrorReturnModel
                {
                    ErrorCode = "1005",
                    ErrorMessage = "Category is not exist"
                });
            if (newPost == -1)
            {
                return BadRequest(new ErrorReturnModel
                {
                    ErrorCode = "1007",
                    ErrorMessage = "Post is exist"
                });
            }

            if (post == null)
            {
                return BadRequest("Post is null.");
            }
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
