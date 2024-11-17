using GPU.Models;
using NuGet.Packaging;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using GPU.Helpers;

namespace GPU.Services
{
    public class ArchiveService
    {
        public static ObservableCollection<PersonalStudent> ar_Student = new ObservableCollection<PersonalStudent>();
        public static ObservableCollection<StudentContactInfo> ar_Contacts = new ObservableCollection<StudentContactInfo>();
        public static ObservableCollection<StudentParentInfo> ar_Parent = new ObservableCollection<StudentParentInfo>();
        public static ObservableCollection<Student12Grade> ar_Grade = new ObservableCollection<Student12Grade>();
        public static ObservableCollection<StudentDepartmentInfo> ar_department = new ObservableCollection<StudentDepartmentInfo>();
        public static ObservableCollection<StudentStages> ar_Stages = new ObservableCollection<StudentStages>();
        public static ObservableCollection<InvoiceInfo> ar_Invoices = new ObservableCollection<InvoiceInfo>();
        public static ObservableCollection<StudentSupport> ar_Supports = new ObservableCollection<StudentSupport>();

        public static async Task Insert(int id = 0, bool isUpdate = false)
        {
            if (!isUpdate)
            {
                using (SqlCommand cmd = new SqlCommand("LoadAllArchives", DbConnectionHelper.con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Personal = new PersonalStudent
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Age = reader.GetInt32(2),
                                Sex = reader.GetString(3),
                                MartialStatus = reader.GetString(4),
                                BloodGroup = reader.GetString(5),
                                Religion = reader.GetString(6),
                                IdentityNo = reader.GetString(7),
                                Nationality = reader.GetString(8),
                                RationingFormNo = reader.GetString(9),
                            };
                            ar_Student.Add(Personal);
                            var grade = new Student12Grade
                            {
                                Id = reader.GetInt32(10),
                                ExamNo = reader.GetString(11),
                                SchoolName = reader.GetString(12),
                                EducationAdministrator = reader.GetString(13),
                                EducationType = reader.GetString(14),
                                Graduation = reader.GetString(15),
                                TotalMark = reader.GetString(16),
                                SID = reader.GetInt32(17),
                            };
                            ar_Grade.Add(grade);
                            var contact = new StudentContactInfo
                            {
                                Id = reader.GetInt32(18),
                                Phone = reader.GetString(19),
                                StudentEmail = reader.GetString(20),
                                Province = reader.GetString(21),
                                City = reader.GetString(22),
                                District = reader.GetString(23),
                                Village = reader.GetString(24),
                                SID = reader.GetInt32(25),
                            };
                            ar_Contacts.Add(contact);
                            var parent = new StudentParentInfo
                            {
                                Id = reader.GetInt32(26),
                                Name = reader.GetString(27),
                                Profession = reader.GetString(28),
                                Phone = reader.GetString(29),
                                Email = reader.GetString(30),
                                SID = reader.GetInt32(31),
                                CardInfoNo = reader.GetString(32),
                                CardInfoIssuePlace = reader.GetString(33),

                            };
                            ar_Parent.Add(parent);
                            var dep = new StudentDepartmentInfo()
                            {
                                Id = reader.GetInt32(34),
                                AcceptanceType = reader.GetString(35),
                                UniversityCommandNo = reader.GetString(36),
                                AdministratorCommandNo = reader.GetString(37),
                                Seq = reader.GetInt32(38),
                                SID = reader.GetInt32(39),
                                Department = reader.GetString(40),
                                Graduate = reader.GetString(41),
                                startinYear = reader.GetString(42),
                                Stage = reader.GetInt32(43),
                                ResidenceType = reader.GetString(44),
                            };
                            ar_department.Add(dep);
                            var sup = new StudentSupport
                            {
                                id = reader.IsDBNull(45) ? 0 : reader.GetInt32(45),
                                office = reader.IsDBNull(46) ? string.Empty : reader.GetString(46),
                                WrittenNo = reader.IsDBNull(47) ? string.Empty : reader.GetString(47),
                                WrittenDate = reader.IsDBNull(48) ? string.Empty : reader.GetString(48),
                                Amount = reader.IsDBNull(49) ? string.Empty : reader.GetString(49),
                                sid = reader.IsDBNull(50) ? 0 : reader.GetInt32(50),

                            };
                            ar_Supports.Add(sup);
                        }

                    }
                    await Task.WhenAll(
                        Getinvoices(id),
                        GetStages(id));
                }
            }
            else
            {
                Update(id);
            }
        }
        private static async Task Getinvoices(int sid = 0)
        {
            string command = sid == 0 ? "select * from Ar_InvoiceInfo " : "select * from Ar_InvoiceInfo where sid = @sid";
            using (SqlCommand cmd = new SqlCommand(command, DbConnectionHelper.con))
            {
                if (sid != 0)
                {
                    cmd.Parameters.AddWithValue("@sid", sid);
                }

                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    while (await rd.ReadAsync())
                    {
                        var model = new InvoiceInfo
                        {
                            id = rd.GetInt32(0),
                            InvoiceId = rd.GetString(1),
                            InvoiceDate = rd.GetString(2),
                            Amount = rd.GetString(3),
                            SID = rd.GetInt32(4),
                        };
                        ar_Invoices.Add(model);
                    }
                }
            }
        }
        private static async Task GetStages(int sid = 0)
        {
            string command = sid == 0 ? "select * from Ar_StudentStages " : "select * from Ar_StudentStages where sid = @sid";
            using (SqlCommand cmd = new SqlCommand(command, DbConnectionHelper.con))
            {
                if (sid != 0)
                {
                    cmd.Parameters.AddWithValue("@sid", sid);
                }

                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    while (await rd.ReadAsync())
                    {
                        var model = new StudentStages
                        {
                            id = rd.GetInt32(0),
                            Stage = rd.GetInt32(1),
                            Year = rd.GetString(2),
                            Sid = rd.GetInt32(3),
                            Status = rd.GetString(4),
                        };
                        ar_Stages.Add(model);
                    }
                }
            }
        }
        public static async void Update(int id)
        {
            Delete(id);
            await Insert(id,false);
        }
        public static void Delete(int id)
        {
            var studentToRemove = ar_Student.FirstOrDefault(s => s.Id == id); if (studentToRemove != null) { ar_Student.Remove(studentToRemove); }
            var contactToRemove = ar_Contacts.FirstOrDefault(c => c.SID == id); if (contactToRemove != null) { ar_Contacts.Remove(contactToRemove); }
            var parentToRemove = ar_Parent.FirstOrDefault(p => p.SID == id); if (parentToRemove != null) { ar_Parent.Remove(parentToRemove); }
            var gradeToRemove = ar_Grade.FirstOrDefault(g => g.Id == id); if (gradeToRemove != null) { ar_Grade.Remove(gradeToRemove); }
            var departmentToRemove = ar_department.FirstOrDefault(d => d.SID == id); if (departmentToRemove != null) { ar_department.Remove(departmentToRemove); }
            var supportToRemove = ar_Supports.FirstOrDefault(s => s.sid == id); if (supportToRemove != null) { ar_Supports.Remove(supportToRemove); }

            foreach (var invoice in ar_Invoices.Where(s => s.SID == id).ToList())
            {
                ar_Invoices.Remove(invoice);
            }
            foreach (var stages in ar_Stages.Where(x => x.Sid == id).ToList())
            {
                ar_Stages.Remove(stages);
            }
        }

    }
}
