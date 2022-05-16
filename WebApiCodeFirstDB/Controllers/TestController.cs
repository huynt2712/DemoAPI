using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCodeFirstDB.Configuration;
using WebApiCodeFirstDB.Models;
using WebApiCodeFirstDB.Services;

namespace WebApiCodeFirstDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public TestController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public IActionResult Test()
        {
            //return Content("This is content string....");
            //var value = new PostCategory
            //{
            //    Name = "test json"
            //};
            //return new JsonResult(value);

            //var myKeyValue = Configuration["MyKey"];
            //var title = Configuration["Position:Title"];


            //return Content($"MyKey value: {myKeyValue} \n" +
            //               $"Title: {title} \n");

            var positionOptions = new PositionOptions();
            Configuration.GetSection(PositionOptions.Position).Bind(positionOptions);

            return Content($"Title: {positionOptions.Title} \n" +
                           $"Name: {positionOptions.Name}");

            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0
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
