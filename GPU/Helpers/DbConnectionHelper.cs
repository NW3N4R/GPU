using System.Data.SqlClient;

namespace GPU.Helpers
{
    public class DbConnectionHelper
    {
        public static SqlConnection con = new SqlConnection("Data Source=p4165386.eero.online,1433;Initial Catalog=KTI;User ID=nwenar;Password =KnnKnn123;TrustServerCertificate=True;MultipleActiveResultSets=True");

        public static async Task<bool> OpenConnection()
        {
            await con.OpenAsync();
            return con.State == System.Data.ConnectionState.Open;
        }
        public static async void CloseConnection()
        {

            await con.CloseAsync();
        }

        public static async Task LoadAll(string ar)
        {
            await Task.WhenAll(
               Helper_PersonalStudent.GetStudents($"select * from {ar}PersonalStudent"),
               Helper_StudentParentInfo.GetParent($"select * from {ar}StudentParentInfo"),
               Helper_StudentContactInfo.GetContacts($"select * from {ar}StudentContactInfo"),
               Helper_Student12Grade.GetGrades($"select * from {ar}Student12Grade"),
               Helper_StudentSupport.GetSupports($"select * from {ar}studentsupport"),
               Helper_StudentDepartmentInfo.GetDepartments($"select * from {ar}StudentDepartmentInfo"),
               Helper_Invoice.GetInvoices($"select * from {ar}InvoiceInfo")
               );
        }
    }
}
