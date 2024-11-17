using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using Microsoft.SqlServer.Server;
using System.Diagnostics;
namespace GPU.Services
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
                    Debug.WriteLine("Service Response Was Error");
                }
            }
        }
    }
}
