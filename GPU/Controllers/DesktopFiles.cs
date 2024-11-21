using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
namespace GPU.Controllers
{
    [ApiController]
    public class DesktopFiles : Controller
    {
        string _FolderPath = @"C:\Users\nwenar\Desktop\Student List Images";
        string[] supportedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", };

        public DesktopFiles()
        {
#if DEBUG
            _FolderPath = @"C:\Users\Aurora\Desktop\Student List Images";
#endif
        }
        [HttpGet("PersonalFiles/{fileName}")]
        public IActionResult PersonalFiles(string fileName)
        {

            string folderPath = Path.Combine(_FolderPath, "Personal Pictures");

            var matchingFile = supportedExtensions
                .Select(ext => Path.Combine(folderPath, fileName + ext))
                .FirstOrDefault(System.IO.File.Exists);

            // If the file is found
            if (!string.IsNullOrEmpty(matchingFile))
            {
                // Set the default content type
                var contentType = "application/octet-stream";

                // Get the file extension
                var extension = Path.GetExtension(matchingFile).ToLowerInvariant();

                // Determine the correct content type based on the file extension
                switch (extension)
                {
                    case ".jpg":
                    case ".jpeg":
                        contentType = "image/jpeg";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    case ".gif":
                        contentType = "image/gif";
                        break;
                    case ".bmp":
                        contentType = "image/bmp";
                        break;
                    case ".tiff":
                        contentType = "image/tiff";
                        break;
                    case ".pdf":
                        contentType = "application/pdf";
                        break;
                }

                // Set the content type in the response
                Response.ContentType = contentType;

                // Return the file to the client
                return PhysicalFile(matchingFile, contentType);
            }

            return NotFound();
        }

        [HttpGet("OtherFiles/{fileName}")]
        public async Task<IActionResult> OtherFiles(string fileName)
        {
            string folderPath = Path.Combine(_FolderPath, "Pdf Files");
            var matchingFile = supportedExtensions
                .Select(ext => Path.Combine(folderPath, fileName))
                .FirstOrDefault(System.IO.File.Exists);

            if (!string.IsNullOrEmpty(matchingFile))
            {
                // Set the default content type
                var contentType = "application/octet-stream";

                // Get the file extension
                var extension = Path.GetExtension(matchingFile).ToLowerInvariant();

                // Determine the correct content type based on the file extension
                switch (extension)
                {
                    case ".jpg":
                    case ".jpeg":
                        contentType = "image/jpeg";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    case ".gif":
                        contentType = "image/gif";
                        break;
                    case ".bmp":
                        contentType = "image/bmp";
                        break;
                    case ".tiff":
                        contentType = "image/tiff";
                        break;
                    case ".pdf":
                        contentType = "application/pdf";
                        break;
                }

                // Set the content type in the response
                Response.ContentType = contentType;

                // Return the file to the client
                return PhysicalFile(matchingFile, contentType);
            }

            return NotFound("No Files");
        }

        [HttpGet("HowManyFiles/{fileName}")]
        public IActionResult GetAllFiles(string fileName)
        {
            string folderPath = Path.Combine(_FolderPath, "Pdf Files");
            string formatName = "_" + fileName + ".";

            var matchingFiles = Directory.GetFiles(folderPath)
                .Where(filePath => Path.GetFileName(filePath).Contains(formatName))  // Check if the file name contains the pattern
                .ToList();

            if (matchingFiles.Any())
            {
                var fileNames = matchingFiles.Select(file => Path.GetFileName(file)).ToList();
                return Ok(fileNames);
            }


            return NotFound("No File");
        }
      
    }
}
