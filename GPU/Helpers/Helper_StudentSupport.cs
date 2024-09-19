using GPU.Models;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace GPU.Helpers
{
    public class Helper_StudentSupport
    {
        public static ObservableCollection<StudentSupport> _Supports = new ObservableCollection<StudentSupport>();
        public static async Task GetSupports(string queary)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = queary;
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
    }
}
