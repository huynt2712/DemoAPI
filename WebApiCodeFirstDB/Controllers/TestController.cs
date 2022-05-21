using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogApi.Models;
using BlogApi.Services;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            //return Content("This is content string....");
            var value = new PostCategory
            {
                Name = "test json"
            };
            return new JsonResult(value);
        }

        [HttpGet("TestSendMail")]
        public IActionResult TestSendMail()
        {
            var emailService = new EmailService();
            var userService = new UserService("Tom", "passs", "1/1/2004", emailService);

            //TestController depence UserService
            var mail = userService.SendMail();
            return Ok(mail);
        }    
    }
}
