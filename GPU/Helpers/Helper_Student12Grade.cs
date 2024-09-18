
using GPU.Models;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
namespace GPU.Helpers
{
    public class Helper_Student12Grade
    {
        public static ObservableCollection<Student12Grade> _Grade = new ObservableCollection<Student12Grade>();

        public static async Task GetGrades()
        {
            using (SqlCommand cmd = new SqlCommand("select * from Student12Grade", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    _Grade.Clear();
                    while (await rd.ReadAsync())
                    {
                        var grade = new Student12Grade
                        {
                            Id = rd.GetInt32(0),
                            ExamNo = rd.GetString(1),
                            SchoolName = rd.GetString(2),
                            EducationAdministrator = rd.GetString(3),
                            EducationType = rd.GetString(4),
                            Graduation = rd.GetString(5),
                            TotalMark = rd.GetString(6),
                            SID = rd.GetInt32(7),
                        };
                        _Grade.Add(grade);
                    }
                }
            }
        }
    }
}
