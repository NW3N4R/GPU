using System.Collections.ObjectModel;
using System.Data.SqlClient;
using GPU.Models;
namespace GPU.Helpers
{
    public class Helper_PersonalStudent
    {
        public static ObservableCollection<PersonalStudent> _Student = new ObservableCollection<PersonalStudent>();

        public static async Task<ObservableCollection<PersonalStudent>> GetStudents()
        {
            using (SqlCommand cmd = new SqlCommand("select * from PersonalStudent", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    _Student.Clear();
                    while (await rd.ReadAsync())
                    {
                        var model = new PersonalStudent
                        {
                            Id = rd.GetInt32(0),
                            Name = rd.GetString(1),
                            Age = rd.GetInt32(2),
                            Sex = rd.GetString(3),
                            MartialStatus = rd.GetString(4),
                            BloodGroup = rd.GetString(5),
                            Religion = rd.GetString(6),
                            IdentityNo = rd.GetString(7),
                            Nationality = rd.GetString(8),
                            RationingFormNo = rd.GetString(9),

                        };

                        _Student.Add(model);
                    }
                }
            }
            return _Student;
        }

        public static async Task Create(PersonalStudent student)
        {
            using (SqlCommand cmd = new SqlCommand("insert into PersonalStudent " +
                "(name,Age,sex,MartialStatus,BloodGroup,Religion,IdentityNo,Nationality,RationingFormNo)values" +
                "(@StudentName,@Age,@Sex,@MartialStatus,@BloodGroup,@Religion,@identityNo,@Nationality,@RationingFormNo)", DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@StudentName", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                cmd.Parameters.AddWithValue("@Sex", student.Sex);
                cmd.Parameters.AddWithValue("@MartialStatus", student.MartialStatus);
                cmd.Parameters.AddWithValue("@BloodGroup", student.BloodGroup);
                cmd.Parameters.AddWithValue("@Religion", student.Religion);
                cmd.Parameters.AddWithValue("@IdentityNo", student.IdentityNo);
                cmd.Parameters.AddWithValue("@Nationality", student.Nationality);
                cmd.Parameters.AddWithValue("@RationingFormNo", student.RationingFormNo);

                // Execute the command
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
