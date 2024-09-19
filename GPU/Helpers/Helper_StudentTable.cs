using GPU.Models;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;

namespace GPU.Helpers
{
    public class Helper_StudentTable
    {
        public static ObservableCollection<StudentTableModel> _stu = new ObservableCollection<StudentTableModel>();
        public static ObservableCollection<SearchModel>_search = new ObservableCollection<SearchModel>();

        public static async Task<ObservableCollection<StudentTableModel>> GetStudent(string cmdtxt)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = cmdtxt;
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
            _stu.First().isFirst = true;
            _stu.First().isLast = true;
            return _stu;
        }
    }
}
