using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApiCodeFirstDB.Configuration;
using WebApiCodeFirstDB.Models;
using WebApiCodeFirstDB.Services;

namespace WebApiCodeFirstDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //private readonly IConfiguration Configuration;

        //public TestController(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        private readonly PositionOptions _options;

        public TestController(IOptions<PositionOptions> options)
        {
            _options = options.Value;
        }


        [HttpGet]
        public IActionResult Test()
        {
            return Content($"Title: {_options.Title} \n" +
                      $"Name: {_options.Name}");
            //var myKeyValue = Configuration["MyKey"];
            //var title = Configuration["Position:Title"];
            //var name = Configuration["Position:Name"];
            //var defaultLogLevel = Configuration["Logging:LogLevel:Default"];


            //return Content($"MyKey value: {myKeyValue} \n" +
            //               $"Title: {title} \n" +
            //               $"Name: {name} \n" +
            //               $"Default Log Level: {defaultLogLevel}");



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
