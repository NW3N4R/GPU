using System.Data.SqlClient;

namespace GPU.Helpers
{
    public class DbConnectionHelper
    {
        public static SqlConnection con = new SqlConnection("Data Source=p4165386.eero.online,1433;Initial Catalog=KTI;User ID=nwenar;Password =KnnKnn123;TrustServerCertificate=True");

        public static async Task<bool> OpenConnection()
        {
            await con.OpenAsync();
            return con.State == System.Data.ConnectionState.Open;
        }
        public static async void CloseConnection()
        {

            await con.CloseAsync();

        }
    }
}
