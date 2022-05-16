using Microsoft.AspNetCore.Mvc;

namespace WebApiCodeFirstDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet]
        public IActionResult TestFile()
        {
            return new PhysicalFileResult(@"C:\Users\long.nguyendh.STS\Desktop\DemoAPI\WebApiCodeFirstDB\Files\TextFile.txt", "text/*");
        }
    }
}
