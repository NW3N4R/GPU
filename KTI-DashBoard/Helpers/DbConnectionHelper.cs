using Microsoft.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace KTI_DashBoard.Helpers
{
    public class DbConnectionHelper
    {
        private static string strConnection = "Data Source=p4165386.eero.online,1433;Initial Catalog=KTI;User ID=nwenar;Password =KnnKnn123;TrustServerCertificate=True;MultipleActiveResultSets=True";
        public static SqlConnection con;
        public static async Task<bool> OpenConnection()
        {
//#if DEBUG
//            strConnection = "Data Source=NWENAR\\SQLEXPRESS;Initial Catalog=KTI;User ID=nwenar;Password =KnnKnn123;TrustServerCertificate=True;MultipleActiveResultSets=True";
//#endif
            con = new SqlConnection(strConnection);
            await con.OpenAsync();
            return con.State == System.Data.ConnectionState.Open;
        }
        public static async void CloseConnection()
        {

            await con.CloseAsync();
        }

        public static async Task LoadAll()
        {

            await Task.WhenAll(
               Helper_PersonalStudent.GetStudents(),
               Helper_PersonalStudent.ar_GetStudents(),
               Helper_StudentParentInfo.GetParent(),
               Helper_StudentParentInfo.ar_GetParent(),
               Helper_StudentContactInfo.GetContacts(),
               Helper_StudentContactInfo.ar_GetContacts(),
               Helper_Student12Grade.GetGrades(),
               Helper_Student12Grade.ar_GetGrades(),
               Helper_StudentSupport.GetSupports(),
               Helper_StudentSupport.ar_GetSupports(),
               Helper_StudentDepartmentInfo.GetDepartments(),
               Helper_StudentDepartmentInfo.ar_GetDepartments(),
               Helper_Invoice.GetInvoices(),
               Helper_Invoice.ar_GetInvoices()
               );
        }

        public static async Task LoadStudent()
        {
            await Task.WhenAll(
            Helper_PersonalStudent.GetStudents(),
            Helper_StudentParentInfo.GetParent(),
            Helper_StudentContactInfo.GetContacts(),
            Helper_Student12Grade.GetGrades(),
            Helper_StudentSupport.GetSupports(),
            Helper_StudentDepartmentInfo.GetDepartments(),
            Helper_Invoice.GetInvoices()
            );
        }

        public static async Task LoadArchive()
        {
            await Task.WhenAll(
               Helper_PersonalStudent.ar_GetStudents(),
               Helper_StudentParentInfo.ar_GetParent(),
               Helper_StudentContactInfo.ar_GetContacts(),
               Helper_Student12Grade.ar_GetGrades(),
               Helper_StudentSupport.ar_GetSupports(),
               Helper_StudentDepartmentInfo.ar_GetDepartments(),
               Helper_Invoice.ar_GetInvoices()
               );
        }
    }
}
