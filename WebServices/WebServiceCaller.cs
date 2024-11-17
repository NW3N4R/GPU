using System.Diagnostics;
using System.Net.Http;
using Microsoft.SqlServer.Server;
namespace WebServices
{
    public class WebServiceCaller
    {
        [SqlProcedure]
        public static void CallWebServices(string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                var responsoe = client.GetAsync(uri).Result;
                if (!responsoe.IsSuccessStatusCode)
                {
                    SqlContext.Pipe.Send("Service Response Was Error");
                }
            }
        }
    }
}
