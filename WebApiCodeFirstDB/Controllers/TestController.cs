using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using BlogWebApi.Configuration;
using BlogWebApi.Data;
using BlogWebApi.Entites;
using BlogWebApi.Services;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Course _course;
        public TestController(IConfiguration configuration, IOptions<Course> course)
        {
            _configuration = configuration;
            _course = course.Value;
        }    

        [HttpGet]
        public IActionResult Test()
        {
            var defaultStudent = _configuration.GetSection("DefaultStudent")?.Value;
            //var courseName = _configuration["Course:Name"];
            //var courseDescription = _configuration["Course:Description"];
            var defaultLogLevel = _configuration.GetSection("Logging:LogLevel:Default")?.Value;
            return new JsonResult($"defaultStudent: {defaultStudent}," +
                $"courseName: {_course.Name}," + 
                $"courseDescription: {_course.Description}," +
                $"defaultLogLevel: {defaultLogLevel}");
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

        [HttpGet("TestIEnumerableVsIQueryAble")]
        public IActionResult TestIEnumerableVsIQueryAble()
        {
            //Sự khác nhau giữa IEnumerable và IQueryable?
            var categoryList = new List<PostCategory>();
            using (var context = new BlogDBContext())
            {
                //postCategories = context.Categories.ToList();

                //IEnumerable namespace System.Collections
                //List, array, arrayList, Hash... IEnumerable
                //List<string> test = new List<string>();

                //IQueryable namespace System.Linq

                //Cách 1
                //Generate query in sql server
                var categoryQueryable = context.Categories.Where(c => c.Id > 50).Take(10);

                //Execute sql query => data in memory (RAM)
                categoryList = categoryQueryable.ToList();

                //Linq: generate query in sql server //Iqueryable
                //Execute sql query => data in memory (RAM) //IEnumerable

                //IQueryable
                //Top 10 + cagegory id > 50
                //select top 10 * from category where id > 50
                //Cách 2
                var categoryEnumerable = context.Categories.ToList();
                //select  * from category 
                //data in memory (RAM)
                categoryList = categoryEnumerable.Where(c => c.Id > 50).Take(10).ToList(); //linq query data in memory

                List<string> testList = new List<string>(); //memory (RAM)
                var xx = testList.Where(x => x == "").FirstOrDefault();
                //testList linq

                //Iqueryable: truy vấn query dữ liệu trên sql server và trả dữ liệu cho client 

                //IEnumerable: truy vấn dữ liệu trên memory (RAM)

                //IEqueryable to Ienumerable ToList() ToArray ToDictionary...

                //Iqueryable performance tốt hơn IEnumerable 


            }

            //using: context {} context dispose (giải phóng)

            return Ok(categoryList);
        }
    }
}
