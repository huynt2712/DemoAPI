using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadFileApi.Models;

namespace UploadFileApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddFileAsync([FromForm] FileRequestModel requestModel)
        {
            if (requestModel.File == null)
                return BadRequest();

            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(),folderName);
            if(requestModel.File.Length > 0)
            {
                var fileName = requestModel.File.FileName.Trim();
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await requestModel.File.CopyToAsync(stream);
                }
                return Ok(new { Path = dbPath, FileName = fileName });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
