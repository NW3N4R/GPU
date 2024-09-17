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

        public static async Task Create(PersonalStudent student, StudentContactInfo contact,
                                        StudentParentInfo parent, Student12Grade school, StudentDepartmentInfo department,InvoiceInfo Invoice )
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                if (department.Graduate == "0")
                {
                    cmd.CommandText = "InsertStudent";

                }
                else
                {
                    cmd.CommandText = "Ar_InsertStudent";
                    cmd.Parameters.AddWithValue("@Graduate", department.Graduate);
                }
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentName", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                cmd.Parameters.AddWithValue("@Sex", student.Sex);
                cmd.Parameters.AddWithValue("@MartialStatus", student.MartialStatus);
                cmd.Parameters.AddWithValue("@BloodGroup", student.BloodGroup);
                cmd.Parameters.AddWithValue("@Religion", student.Religion);
                cmd.Parameters.AddWithValue("@identityNo", student.IdentityNo);
                cmd.Parameters.AddWithValue("@Nationality", student.Nationality);
                cmd.Parameters.AddWithValue("@RationingFormNo", student.RationingFormNo);
                cmd.Parameters.AddWithValue("@StudentPhone", contact.Phone);
                cmd.Parameters.AddWithValue("@StudentEmail", contact.StudentEmail);
                cmd.Parameters.AddWithValue("@Province", contact.Province);
                cmd.Parameters.AddWithValue("@City", contact.City);
                cmd.Parameters.AddWithValue("@District", contact.District);
                cmd.Parameters.AddWithValue("@Village", contact.Village);
                cmd.Parameters.AddWithValue("@ParentName", parent.Name);
                cmd.Parameters.AddWithValue("@Profession", parent.Profession);
                cmd.Parameters.AddWithValue("@ParentPhone", parent.Phone);
                cmd.Parameters.AddWithValue("@ParentEmail", parent.Email);
                cmd.Parameters.AddWithValue("@ExamNo", school.ExamNo);
                cmd.Parameters.AddWithValue("@SchoolName", school.SchoolName);
                cmd.Parameters.AddWithValue("@EducationAdministrator", school.EducationAdministrator);
                cmd.Parameters.AddWithValue("@EducationType", school.EducationType);
                cmd.Parameters.AddWithValue("@Graduation", school.Graduation);
                cmd.Parameters.AddWithValue("@TotalMark", school.TotalMark);
                cmd.Parameters.AddWithValue("@Department", department.Department);
                cmd.Parameters.AddWithValue("@AcceptanceType", department.AcceptanceType);
                cmd.Parameters.AddWithValue("@UniversityCommandNo", department.UniversityCommandNo);
                cmd.Parameters.AddWithValue("@AdministratorCommandNo", department.AdministratorCommandNo);
                cmd.Parameters.AddWithValue("@Seq", department.Seq);
                cmd.Parameters.AddWithValue("@StartingYear", department.startinYear);
                cmd.Parameters.AddWithValue("@stage", department.Stage);

                if(!department.AcceptanceType.Contains("زانکۆلاین"))
                {
                    cmd.Parameters.AddWithValue("@invoiceId", Invoice.InvoiceId);
                    cmd.Parameters.AddWithValue("@invoiceDate", Invoice.InvoiceDate);
                    cmd.Parameters.AddWithValue("@Amount", Invoice.Amount);
                }

                // Execute the command
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
