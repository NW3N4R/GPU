using System.Collections.ObjectModel;
using System.Data.SqlClient;
using GPU.Models;
namespace GPU.Helpers
{
    public class Helper_PersonalStudent
    {
        public static ObservableCollection<PersonalStudent> _Student = new ObservableCollection<PersonalStudent>();

        public static async Task<ObservableCollection<PersonalStudent>> GetStudents(string queary)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = queary;
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
                                        StudentParentInfo parent, Student12Grade school,
                                        StudentDepartmentInfo department, InvoiceInfo Invoice,
                                        StudentSupport support)
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
                cmd.Parameters.AddWithValue("@CardInfoNo", parent.CardInfoNo);
                cmd.Parameters.AddWithValue("@CardInfoIssuePlace", parent.CardInfoIssuePlace);
                cmd.Parameters.AddWithValue("@ResidenceType", department.ResidenceType);

                if (!string.IsNullOrEmpty(department.AcceptanceType) && !department.AcceptanceType.Contains("زانکۆلاین"))
                {
                    cmd.Parameters.AddWithValue("@invoiceId", Invoice.InvoiceId);
                    cmd.Parameters.AddWithValue("@invoiceDate", Invoice.InvoiceDate);
                    cmd.Parameters.AddWithValue("@Amount", Invoice.Amount);
                }
                if (support.office != "" || !string.IsNullOrEmpty(support.office) || !string.IsNullOrWhiteSpace(support.office))
                {
                    cmd.Parameters.AddWithValue("@office", support.office);
                    cmd.Parameters.AddWithValue("@writtenNo", support.WrittenNo);
                    cmd.Parameters.AddWithValue("@writtenDate", support.WrittenDate);
                    cmd.Parameters.AddWithValue("@SupAmount", support.Amount);
                }
                // Execute the command
                await cmd.ExecuteNonQueryAsync();
            }
        }


        public static async Task Update(PersonalStudent student, StudentContactInfo contact,
                                      StudentParentInfo parent, Student12Grade school,
                                      StudentDepartmentInfo department, StudentSupport support)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {

                // -- Personal Info
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", student.Id);
                cmd.Parameters.AddWithValue("@StudentName", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                cmd.Parameters.AddWithValue("@Sex", student.Sex);
                cmd.Parameters.AddWithValue("@MartialStatus", student.MartialStatus);
                cmd.Parameters.AddWithValue("@BloodGroup", student.BloodGroup);
                cmd.Parameters.AddWithValue("@Religion", student.Religion);
                cmd.Parameters.AddWithValue("@identityNo", student.IdentityNo);
                cmd.Parameters.AddWithValue("@Nationality", student.Nationality);
                cmd.Parameters.AddWithValue("@RationingFormNo", student.RationingFormNo);
                // -- Contact Info
                cmd.Parameters.AddWithValue("@StudentPhone", contact.Phone);
                cmd.Parameters.AddWithValue("@StudentEmail", contact.StudentEmail);
                cmd.Parameters.AddWithValue("@Province", contact.Province);
                cmd.Parameters.AddWithValue("@City", contact.City);
                cmd.Parameters.AddWithValue("@District", contact.District);
                cmd.Parameters.AddWithValue("@Village", contact.Village);
                // -- Parent Info
                cmd.Parameters.AddWithValue("@ParentName", parent.Name);
                cmd.Parameters.AddWithValue("@Profession", parent.Profession);
                cmd.Parameters.AddWithValue("@ParentPhone", parent.Phone);
                cmd.Parameters.AddWithValue("@ParentEmail", parent.Email);
                cmd.Parameters.AddWithValue("@CardInfoNo", parent.CardInfoNo);
                cmd.Parameters.AddWithValue("@CardInfoIssuePlace", parent.CardInfoIssuePlace);
                // -- 12 Grade
                cmd.Parameters.AddWithValue("@ExamNo", school.ExamNo);
                cmd.Parameters.AddWithValue("@SchoolName", school.SchoolName);
                cmd.Parameters.AddWithValue("@EducationAdministrator", school.EducationAdministrator);
                cmd.Parameters.AddWithValue("@EducationType", school.EducationType);
                cmd.Parameters.AddWithValue("@Graduation", school.Graduation);
                cmd.Parameters.AddWithValue("@TotalMark", school.TotalMark);
                // -- Deparment
                cmd.Parameters.AddWithValue("@Department", department.Department);
                cmd.Parameters.AddWithValue("@UniversityCommandNo", department.UniversityCommandNo);
                cmd.Parameters.AddWithValue("@AdministratorCommandNo", department.AdministratorCommandNo);
                cmd.Parameters.AddWithValue("@Seq", department.Seq);
                cmd.Parameters.AddWithValue("@StartingYear", department.startinYear);
                cmd.Parameters.AddWithValue("@stage", department.Stage);
                cmd.Parameters.AddWithValue("@ResidenceType", department.ResidenceType);
                if (support.office != "" || !string.IsNullOrEmpty(support.office) || !string.IsNullOrWhiteSpace(support.office))
                {
                    // -- support 
                    cmd.Parameters.AddWithValue("@office", support.office);
                    cmd.Parameters.AddWithValue("@writtenNo", support.WrittenNo);
                    cmd.Parameters.AddWithValue("@writtenDate", support.WrittenDate);
                    cmd.Parameters.AddWithValue("@SupAmount", support.Amount);
                }

                if (department.Graduate == "0")
                {
                    // acceptance type is the same so student stays 
                    cmd.CommandText = "UpdateStudent";
                    await cmd.ExecuteNonQueryAsync();

                }
                else
                {
                    SqlParameter SidParam = new SqlParameter("@sid", System.Data.SqlDbType.Int);
                    SidParam.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(SidParam);
                    // acceptance type is difference so student movies
                    // move student from list of current student to the archive list
                    cmd.CommandText = "MoveToArchive";
                    cmd.Parameters.AddWithValue("@AcceptanceType", department.AcceptanceType);
                    cmd.Parameters.AddWithValue("@Graduate", department.Graduate);
                    await cmd.ExecuteNonQueryAsync();
                    int sid = (int)SidParam.Value;

                    if (!string.IsNullOrEmpty(department.AcceptanceType) && !department.AcceptanceType.Contains("زانکۆلاین"))
                    {
                        foreach (var Invoice in Helper_Invoice._Invoices.Where(x => x.SID == student.Id))
                        {
                            using (SqlCommand invoiceCmd = new SqlCommand("MoveInvoiceToArchive", DbConnectionHelper.con))
                            {
                                invoiceCmd.CommandType = System.Data.CommandType.StoredProcedure;
                                invoiceCmd.Parameters.AddWithValue("@id", Invoice.id);
                                invoiceCmd.Parameters.AddWithValue("@InvoiceId", Invoice.InvoiceId);
                                invoiceCmd.Parameters.AddWithValue("@InvoiceDate", Invoice.InvoiceDate);
                                invoiceCmd.Parameters.AddWithValue("@Amount", Invoice.Amount);
                                invoiceCmd.Parameters.AddWithValue("@sid", sid);

                                await invoiceCmd.ExecuteNonQueryAsync();
                            }
                        }
                    }


                    using (SqlCommand AfterMatchCMD = new SqlCommand("Delete from PersonalStudent where id = @id", DbConnectionHelper.con))
                    {
                        AfterMatchCMD.Parameters.AddWithValue("@id", student.Id);
                        await AfterMatchCMD.ExecuteNonQueryAsync();
                    }
                }



            }
        }


        public static async Task Ar_Update(PersonalStudent student, StudentContactInfo contact,
                                      StudentParentInfo parent, Student12Grade school,
                                      StudentDepartmentInfo department, StudentSupport support)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                
                //-- Personal Info
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", student.Id);
                cmd.Parameters.AddWithValue("@StudentName", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                cmd.Parameters.AddWithValue("@Sex", student.Sex);
                cmd.Parameters.AddWithValue("@MartialStatus", student.MartialStatus);
                cmd.Parameters.AddWithValue("@BloodGroup", student.BloodGroup);
                cmd.Parameters.AddWithValue("@Religion", student.Religion);
                cmd.Parameters.AddWithValue("@identityNo", student.IdentityNo);
                cmd.Parameters.AddWithValue("@Nationality", student.Nationality);
                cmd.Parameters.AddWithValue("@RationingFormNo", student.RationingFormNo);
                // -- Contact Info
                cmd.Parameters.AddWithValue("@StudentPhone", contact.Phone);
                cmd.Parameters.AddWithValue("@StudentEmail", contact.StudentEmail);
                cmd.Parameters.AddWithValue("@Province", contact.Province);
                cmd.Parameters.AddWithValue("@City", contact.City);
                cmd.Parameters.AddWithValue("@District", contact.District);
                cmd.Parameters.AddWithValue("@Village", contact.Village);
                // --Parent Info
                cmd.Parameters.AddWithValue("@ParentName", parent.Name);
                cmd.Parameters.AddWithValue("@Profession", parent.Profession);
                cmd.Parameters.AddWithValue("@ParentPhone", parent.Phone);
                cmd.Parameters.AddWithValue("@ParentEmail", parent.Email);
                cmd.Parameters.AddWithValue("@CardInfoNo", parent.CardInfoNo);
                cmd.Parameters.AddWithValue("@CardInfoIssuePlace", parent.CardInfoIssuePlace);
                //--  12 Grade
                cmd.Parameters.AddWithValue("@ExamNo", school.ExamNo);
                cmd.Parameters.AddWithValue("@SchoolName", school.SchoolName);
                cmd.Parameters.AddWithValue("@EducationAdministrator", school.EducationAdministrator);
                cmd.Parameters.AddWithValue("@EducationType", school.EducationType);
                cmd.Parameters.AddWithValue("@Graduation", school.Graduation);
                cmd.Parameters.AddWithValue("@TotalMark", school.TotalMark);
                //-- Deparment
                cmd.Parameters.AddWithValue("@Department", department.Department);
                cmd.Parameters.AddWithValue("@UniversityCommandNo", department.UniversityCommandNo);
                cmd.Parameters.AddWithValue("@AdministratorCommandNo", department.AdministratorCommandNo);
                cmd.Parameters.AddWithValue("@Seq", department.Seq);
                cmd.Parameters.AddWithValue("@StartingYear", department.startinYear);
                cmd.Parameters.AddWithValue("@stage", department.Stage);
                cmd.Parameters.AddWithValue("@ResidenceType", department.ResidenceType);

                if (support.office != "" || !string.IsNullOrEmpty(support.office) || !string.IsNullOrWhiteSpace(support.office))
                {
                    //-- support 
                    cmd.Parameters.AddWithValue("@office", support.office);
                    cmd.Parameters.AddWithValue("@writtenNo", support.WrittenNo);
                    cmd.Parameters.AddWithValue("@writtenDate", support.WrittenDate);
                    cmd.Parameters.AddWithValue("@SupAmount", support.Amount);
                }
                // Execute the command


                if (department.Graduate != "0")
                {
                    // acceptance type is the same so student stays 
                    cmd.CommandText = "Ar_UpdateStudent";
                    cmd.Parameters.AddWithValue("@Graduate", department.Graduate);
                    await cmd.ExecuteNonQueryAsync();

                }
                else
                {
                    cmd.Parameters.AddWithValue("@AcceptanceType", department.AcceptanceType);
                    SqlParameter SidParam = new SqlParameter("@sid", System.Data.SqlDbType.Int);
                    SidParam.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(SidParam);
                    cmd.CommandText = "MoveToStudentList"; // acceptance type is difference so student movies move student from Archive to the list of current student
                    await cmd.ExecuteNonQueryAsync();
                    int sid = (int)SidParam.Value;
                    if (!string.IsNullOrEmpty(department.AcceptanceType) && !department.AcceptanceType.Contains("زانکۆلاین"))
                    {
                        foreach (var Invoice in Helper_Invoice._Invoices.Where(x => x.SID == student.Id))
                        {
                            using (SqlCommand invoiceCmd = new SqlCommand("MoveInvoiceToStudentList", DbConnectionHelper.con))
                            {
                                invoiceCmd.CommandType = System.Data.CommandType.StoredProcedure;
                                invoiceCmd.Parameters.AddWithValue("@id", Invoice.id);
                                invoiceCmd.Parameters.AddWithValue("@InvoiceId", Invoice.InvoiceId);
                                invoiceCmd.Parameters.AddWithValue("@InvoiceDate", Invoice.InvoiceDate);
                                invoiceCmd.Parameters.AddWithValue("@Amount", Invoice.Amount);
                                invoiceCmd.Parameters.AddWithValue("@sid", sid);

                                await invoiceCmd.ExecuteNonQueryAsync();
                            }
                        }
                    }


                    using (SqlCommand AfterMatchCMD = new SqlCommand("Delete from Ar_PersonalStudent where id = @id", DbConnectionHelper.con))
                    {
                        AfterMatchCMD.Parameters.AddWithValue("@id", student.Id);
                        await AfterMatchCMD.ExecuteNonQueryAsync();
                    }
                }
            }

        }
    }
}
