using KTI_DashBoard.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KTI_DashBoard.Helpers
{
    public class StaticalHelper
    {
        public static List<StaticalTableModel> _Statical = new List<StaticalTableModel>();
        public static async Task<List<StaticalTableModel>> GetStatical()
        {
            using (SqlCommand cmd = new SqlCommand("exec GetStaticalTable", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    _Statical.Clear();

                    while (await rd.ReadAsync())
                    {
                        var statical = new StaticalTableModel
                        {
                            Name = rd.GetString(0),
                            MartialStatus = rd.GetString(1),
                            Age = rd.GetInt32(2),
                            Religion = rd.GetString(3),
                            Nationality = rd.GetString(4),
                            Province = rd.GetString(5),
                            EducationType = rd.GetString(6),
                            SchoolGraduation = rd.GetString(7),
                            Department = rd.GetString(8),
                            AcceptanceType = rd.GetString(9),
                            ResidenceType = rd.GetString(10),
                            StartingYear = rd.GetString(11),
                            Stage = rd.GetInt32(12),
                        };
                        _Statical.Add(statical);
                    }
                }
            }
            return _Statical;
        }
    }
}
