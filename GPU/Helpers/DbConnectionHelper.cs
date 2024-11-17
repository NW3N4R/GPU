using GPU.Models;
using GPU.Services;
using Humanizer;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;
namespace GPU.Helpers
{
    public class DbConnectionHelper
    {
        private static string Connection = @"Data Source=p4165386.eero.online,1433;Initial Catalog=KTI;User ID = nwenar; Password = KnnKnn123; TrustServerCertificate = True;
            MultipleActiveResultSets = True; Pooling = True; Min Pool Size = 5; Max Pool Size = 10; Connection Lifetime = 300;";
        public static List<InfoModel> Infos { get; set; } = new List<InfoModel>(); // Define as a property
        public static SqlConnection con;
        public async Task<bool> OpenConnection()
        {
#if DEBUG
            Connection = @"Data Source=NWENAR\SQLEXPRESS;Initial Catalog=KTI;User ID=nwenar;Password =KnnKnn123;TrustServerCertificate=True;
                            MultipleActiveResultSets=True;Pooling=True;Min Pool Size=5;Max Pool Size=10;Connection Lifetime=300;";
#endif
            con = new SqlConnection(Connection);
            await con.OpenAsync();
            return con.State == System.Data.ConnectionState.Open;
        }
    }
}
