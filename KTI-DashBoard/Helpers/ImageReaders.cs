using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KTI_DashBoard.Helpers
{
    public class ImageReaders
    {
        public static ObservableCollection<ImageAddress> _imageCollection = new ObservableCollection<ImageAddress>();

        private static readonly HttpClient _httpClient = new HttpClient();
        public static async Task<BitmapImage> LoadImageFromUrlAsync(string imageUrl, string imgName)
        {
            try
            {
                // Get the image bytes from the URL
                byte[] imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);

                // Create a BitmapImage and load the image from the byte array
                var bitmapImage = new BitmapImage();
                using (var stream = new MemoryStream(imageBytes))
                {
                    await bitmapImage.SetSourceAsync(stream.AsRandomAccessStream());
                }
                var model = new ImageAddress
                {
                    Image = bitmapImage,
                    ImageName = imgName + ".jpg",
                    CountDown = _imageCollection.Count + 1
                };
                _imageCollection.Add(model);
                return bitmapImage;
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., URL not reachable, invalid image format)
                Console.WriteLine($"Error loading image: {ex.Message}");
                return null;
            }
        }


    }
    public class ImageAddress
    {
        public BitmapImage Image { get; set; }
        public string ImageName { get; set; }
        public int CountDown { get; set; }
    }
}
