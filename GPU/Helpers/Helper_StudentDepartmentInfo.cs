using GPU.Models;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;

namespace GPU.Helpers
{
    public class Helper_StudentDepartmentInfo
    {
        public static ObservableCollection<StudentDepartmentInfo> _departmen = new ObservableCollection<StudentDepartmentInfo>();

        public static async Task GetDepartments()
        {
            using (SqlCommand cmd = new SqlCommand("select * from StudentDepartmentInfo", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    _departmen.Clear();
                    while (await rd.ReadAsync())
                    {
                        var model = new StudentDepartmentInfo
                        {
                            Id = rd.GetInt32(0),
                            AcceptanceType = rd.GetString(1),
                            UniversityCommandNo = rd.GetString(2),
                            AdministratorCommandNo = rd.GetString(3),
                            Seq = rd.GetInt32(4),
                            SID = rd.GetInt32(5),

                        };
                        _departmen.Add(model);
                    }

                }
            }
        }
    }
}
