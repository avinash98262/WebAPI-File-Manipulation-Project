using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Models;
using Repository;

namespace FileManipulation_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;

         public FileController(IFileRepository fileRepository)
         {
             _fileRepository = fileRepository;
         }
        

        [HttpGet("")]
        public string Get() 
        {
            return "Welcome to the File Manipulation Projects";
        }


        [HttpGet("all")]
        public IActionResult GetAllFiles()
        {
            var files = _fileRepository.GetAllFiles();
           
            return Ok(files);
        }


        [HttpPost("")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var fileName = Path.GetFileName(file.FileName);
            var filePath = _fileRepository.GetUniqueFilePath(fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(fileName);
        }


        [HttpGet("{filename}")]
        public async Task<IActionResult> DownloadFile([FromRoute] string filename)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound($"File '{filename}' not found.");
                }

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(bytes, contentType, filename);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while downloading the file.");
            }
        }

        [HttpDelete("{fileName}")]
        public  IActionResult DeleteFile([FromRoute]string fileName)
        {
            var check =  _fileRepository.DeleteFile(fileName);
            

            if (check == true)
            {
                
                return Ok($"File '{fileName}' deleted successfully.");
            }

            return NotFound($"File '{fileName}' not found.");
        }



    }
}
