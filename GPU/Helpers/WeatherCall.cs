
using System.Net.Http;
using System.Threading.Tasks;
using GPU.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace GPU.Helpers
{
    public class WeatherCall
    {
        public static List<WeatherModel.WeatherData> _weather = new List<WeatherModel.WeatherData>();
        private static readonly HttpClient client = new HttpClient();
        public static async Task<string> getWeather()
        {
            string apiKey = "d7d48dccf8284c36b35152906240610"; // Replace with your API key
            string city = "Kalar,Iraq";
            string url = $"http://api.weatherapi.com/v1/current.json?key={apiKey}&q={city}&lang=ku";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                WeatherModel.WeatherData weatherData = JsonConvert.DeserializeObject<WeatherModel.WeatherData>(responseBody);
                _weather.Clear();
                _weather.Add(weatherData);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Request error: " + e.Message);
            }
            return city;
        }
    }
}
