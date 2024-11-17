using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using Microsoft.SqlServer.Server;

public class WebServiceCaller
{
    [SqlProcedure]
    public static void CallWebService(string uri)
    {
        using (HttpClient client = new HttpClient())
        {
            var response = client.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode)
            {
            }
        }
    }
}
