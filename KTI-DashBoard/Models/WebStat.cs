using KTI_DashBoard.Helpers;
using System;
using Microsoft.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace KTI_DashBoard.Models
{
    class WebStat
    {
        public static async Task<bool> GetStatAsync()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 webStat FROM stat", DbConnectionHelper.con))
            {
                var result = await cmd.ExecuteScalarAsync();

                if (result != null && bool.TryParse(result.ToString(), out bool status))
                {
                    return status;
                }

                return false;
            }
        }
        private static readonly HttpClient client = new HttpClient();
        public static async Task<bool> IsWebRespondingAsync(string url)
        {
            try
            {
                // Send an asynchronous GET request to the URL
                HttpResponseMessage response = await client.GetAsync(url);

                // Check if the status code indicates success (200-299)
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException e)
            {
                // Handle any request errors (e.g., server not found, etc.)
                Console.WriteLine($"Request error: {e.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Catch all other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> UpdateWebStat()
        {
            using (SqlCommand cmd = new SqlCommand("update stat set webStat =@webStat", DbConnectionHelper.con))
            {

                cmd.Parameters.AddWithValue("@webStat", await GetStatAsync() == true ? false : true);
                int rf = await cmd.ExecuteNonQueryAsync();
                return rf > 0;
            }
        }
    }
}
