using KTI_DashBoard.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KTI_DashBoard.Helpers
{
    public class Helper_StudentTable
    {
        public static List<StudentTableModel> _stu = new List<StudentTableModel>();
        public static List<StudentTableModel> ar_stu = new List<StudentTableModel>();

        public static async Task<List<StudentTableModel>> GetStudent()
        {
            using (SqlCommand cmd = new SqlCommand("getstudents", DbConnectionHelper.con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    _stu.Clear();
                    while (await rd.ReadAsync())
                    {
                        var model = new StudentTableModel
                        {
                            id = rd.GetInt32(0),
                            Name = rd.GetString(1),
                            Acceptance = rd.GetString(2),
                            department = rd.GetString(3),
                            StartingYear = rd.GetString(4),
                            Stage = rd.GetInt32(5),

                        };
                        _stu.Add(model);
                    }
                }
            }
        
            return _stu;
        }
        public static async Task<List<StudentTableModel>> ar_GetStudent()
        {
            using (SqlCommand cmd = new SqlCommand("ar_getstudents", DbConnectionHelper.con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    ar_stu.Clear();
                    while (await rd.ReadAsync())
                    {
                        var model = new StudentTableModel
                        {
                            id = rd.GetInt32(0),
                            Name = rd.GetString(1),
                            Acceptance = rd.GetString(2),
                            department = rd.GetString(3),
                            StartingYear = rd.GetString(4),
                            Stage = rd.GetInt32(5),

                        };
                        ar_stu.Add(model);
                    }
                }
            }
         
            return ar_stu;
        }

        public static List<StudentTableModel> CombineCollections()
        {
            List<StudentTableModel> combinedList = new List<StudentTableModel>(_stu);
            combinedList.AddRange(ar_stu);
            return new List<StudentTableModel>(combinedList);
        }
    }
}
