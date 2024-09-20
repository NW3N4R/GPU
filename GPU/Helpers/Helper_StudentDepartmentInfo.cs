using GPU.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;

namespace GPU.Helpers
{
    public class Helper_StudentDepartmentInfo
    {
        public static ObservableCollection<StudentDepartmentInfo> _department = new ObservableCollection<StudentDepartmentInfo>();

        public static async Task GetDepartments(string queary)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = queary;
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    _department.Clear();
                    while (await rd.ReadAsync())
                    {
                        if (cmd.CommandText != "select * from ar_StudentDepartmentInfo")
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
                        else
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
                            _department.Add(model);
                        }
                    }

                }
            }
        }
    }
}
