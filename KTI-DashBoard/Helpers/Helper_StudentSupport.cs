using KTI_DashBoard.Models;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace KTI_DashBoard.Helpers
{
    public class Helper_StudentSupport
    {
        public static List<StudentSupport> _Supports = new List<StudentSupport>();
        public static List<StudentSupport> ar_Supports = new List<StudentSupport>();
        public static async Task GetSupports()
        {
            using (SqlCommand cmd = new SqlCommand("select * from studentsupport", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    _Supports.Clear();
                    while (await rd.ReadAsync())
                    {
                        var model = new StudentSupport
                        {
                            id = rd.GetInt32(0),
                            office = rd.GetString(1),
                            WrittenNo = rd.GetString(2),
                            WrittenDate = rd.GetString(3),
                            Amount = rd.GetString(4),
                            sid = rd.GetInt32(5)
                        };
                        _Supports.Add(model);
                    }

                }
            }
        }
        public static async Task ar_GetSupports()
        {
            using (SqlCommand cmd = new SqlCommand("select * from ar_studentsupport", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    ar_Supports.Clear();
                    while (await rd.ReadAsync())
                    {
                        var model = new StudentSupport
                        {
                            id = rd.GetInt32(0),
                            office = rd.GetString(1),
                            WrittenNo = rd.GetString(2),
                            WrittenDate = rd.GetString(3),
                            Amount = rd.GetString(4),
                            sid = rd.GetInt32(5)
                        };
                        ar_Supports.Add(model);
                    }

                }
            }
        }
    }
}
