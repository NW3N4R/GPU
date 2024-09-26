using KTI_DashBoard.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace KTI_DashBoard.Helpers
{
    public class Helper_StudentDepartmentInfo
    {
        public static List<StudentDepartmentInfo> _department = new List<StudentDepartmentInfo>();
        public static List<StudentDepartmentInfo> ar_department = new List<StudentDepartmentInfo>();

        public static async Task GetDepartments()
        {
            using (SqlCommand cmd = new SqlCommand("select * from StudentDepartmentInfo", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    _department.Clear();
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
                            Department = rd.GetString(6),
                            startinYear = rd.GetString(7),
                            Stage = rd.GetInt32(8),
                            ResidenceType = rd.GetString(9)

                        };
                        _department.Add(model);

                    }
                }
            }
        }

        public static async Task ar_GetDepartments()
        {
            using (SqlCommand cmd = new SqlCommand("select * from ar_StudentDepartmentInfo", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    ar_department.Clear();
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
                            Department = rd.GetString(6),
                            Graduate = rd.GetString(7),
                            startinYear = rd.GetString(8),
                            Stage = rd.GetInt32(9),
                            ResidenceType = rd.GetString(10)

                        };
                        ar_department.Add(model);
                    }
                }
            }
        }
    }
}


