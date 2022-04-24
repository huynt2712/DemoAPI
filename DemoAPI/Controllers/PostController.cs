using DemoAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    public class PostController : Controller
    {
        
        [HttpGet]
        [Route("/posts")]
        public IActionResult Index()
        {
            return Ok(PostList.GetListPost(string.Empty));
        }
        [HttpGet]
        [Route("/posts/filter/{searchField}")]
        public IActionResult Index(string searchField)
        {
            return Ok(PostList.GetListPost(searchField));
        }
    }
}
