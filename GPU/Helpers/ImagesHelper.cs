namespace GPU.Helpers
{
    public class ImagesHelper
    {
        static string? directoryPath;
        public static async Task<IFormFile> GetProfileImageAsFormFile(string fileNameWithoutExtension, int ind)
        {
            try
            {
                if (ind == 0)
                {

                    directoryPath = "C:\\Users\\Aurora\\Desktop\\Student List Images\\ProfilePic";

#if !DEBUG
directoryPath = @"C:\Users\nwenar\Desktop\Student List Images\ProfilePic";
#endif
                }
                else
                {

                    directoryPath = @"C:\Users\Aurora\Desktop\Archive List Images\ProfilePic";

#if !DEBUG
directoryPath = @"C:\Users\nwenar\Desktop\Archive List Images\ProfilePic";
#endif
                }
                // Define the directory path where the images are located

                // Get all files in the directory
                var files = Directory.GetFiles(directoryPath);

                // Look for a file that matches the provided name (case-insensitive) regardless of extension
                var filePath = files.FirstOrDefault(file =>
                    Path.GetFileNameWithoutExtension(file).Equals(fileNameWithoutExtension, StringComparison.OrdinalIgnoreCase) &&
                    !Path.GetExtension(file).Equals(".gif", StringComparison.OrdinalIgnoreCase)); // Exclude .gif files

                if (filePath == null)
                {
                    return null; // No file found with the specified name
                }

                // Convert the found file to IFormFile
                using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var fileName = Path.GetFileName(filePath);
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var formFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = GetContentType(fileName)
                };

                return formFile;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public static async Task<IFormFile> GetotherPics(string fileNameWithoutExtension, int ind, string folder)
        {
            try
            {
                if (ind == 0)
                {

                    directoryPath = @$"C:\Users\Aurora\Desktop\Student List Images\{folder}";

#if !DEBUG
directoryPath = @"C:\Users\nwenar\Desktop\Student List Images\ProfilePic";
#endif
                }
                else
                {

                    directoryPath = @$"C:\Users\Aurora\Desktop\Archive List Images\{folder}";

#if !DEBUG
directoryPath = @"C:\Users\nwenar\Desktop\Archive List Images\ProfilePic";
#endif
                }
                // Define the directory path where the images are located

                // Get all files in the directory
                var files = Directory.GetFiles(directoryPath);

                // Look for a file that matches the provided name (case-insensitive) regardless of extension
                var filePath = files.FirstOrDefault(file =>
                    Path.GetFileNameWithoutExtension(file).Equals(fileNameWithoutExtension, StringComparison.OrdinalIgnoreCase) &&
                    !Path.GetExtension(file).Equals(".gif", StringComparison.OrdinalIgnoreCase)); // Exclude .gif files

                if (filePath == null)
                {
                    return null; // No file found with the specified name
                }

                // Convert the found file to IFormFile
                using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var fileName = Path.GetFileName(filePath);
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var formFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = GetContentType(fileName)
                };

                return formFile;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".bmp" => "image/bmp",
                ".tiff" or ".tif" => "image/tiff",
                ".webp" => "image/webp",
                _ => "application/octet-stream", // Default if file type is unknown
            };
        }
    }
}
