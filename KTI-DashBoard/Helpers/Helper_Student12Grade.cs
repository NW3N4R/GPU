
using KTI_DashBoard.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
namespace KTI_DashBoard.Helpers
{
    public class Helper_Student12Grade
    {
        public static List<Student12Grade> _Grade = new List<Student12Grade>();
        public static List<Student12Grade> ar_Grade = new List<Student12Grade>();

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
                        Debug.WriteLine($"the data {grade.SchoolName} was read");
                        _Grade.Add(grade);
                    }
                }
            }
        }

        public static async Task ar_GetGrades()
        {
            using (SqlCommand cmd = new SqlCommand("select * from ar_Student12Grade", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    ar_Grade.Clear();
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
                        ar_Grade.Add(grade);
                    }
                }
            }
        }
    }
}
