using BlogWebApi.ViewModel.Common;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApi.Controllers
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

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddFileAsync([FromForm]FileRequestModel requestModel)
        {
            if (requestModel.File == null)
                return BadRequest();

            if(requestModel.File.Length <= 0)
                return BadRequest();

            //Upload file
            var folderName = Path.Combine("Resources", "Images"); //Resources/Images
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            //C:\Users\long.nguyendh.STS\Desktop\DemoAPI\WebApiCodeFirstDB\ + Resources\Images

            var fileName = requestModel.File.FileName.Trim();
            var fullPath = Path.Combine(pathToSave, fileName);
            // C:\Users\long.nguyendh.STS\Desktop\DemoAPI\WebApiCodeFirstDB\ +Resources\Images + test1Upload.png
            //localhost:port + Resources\Images + test1Upload.png

            var dbPath = Path.Combine(folderName, fileName); //Resources\Images + test1Upload.png

            using (var stream = new FileStream(fullPath, FileMode.Create)) //create new file
            {
                await requestModel.File.CopyToAsync(stream); //copy file from requestModel to new file in local
            }

            return Ok(new { FileName = fileName, Path = dbPath });
        }
    }
}
