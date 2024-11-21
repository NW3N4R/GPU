using Microsoft.Data.SqlClient;
using System.Security.Claims;
using GPU.Models;
using System.Data;
using System.Diagnostics;
using GPU.Services;
using System.IO;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
namespace GPU.Helpers
{
    public class Helper_PersonalStudent
    {

        #region Create
        public static async Task Create(PersonalStudent student, StudentContactInfo contact,
                                     StudentParentInfo parent, Student12Grade school,
                                     StudentDepartmentInfo department, InvoiceInfo Invoice,
                                     StudentSupport support)
        {
            bool isSuccess = false;
            try
            {
                string FolderName = "";
                using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
                {
                    if (department.Graduate == "0")
                    {
                        cmd.CommandText = "InsertStudent";
                        FolderName = "Student List Images";
                    }
                    else
                    {
                        cmd.CommandText = "Ar_InsertStudent";
                        FolderName = "Archive List Images";

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

                    cmd.Parameters.AddWithValue("@office", support.office);
                    cmd.Parameters.AddWithValue("@writtenNo", support.WrittenNo);
                    cmd.Parameters.AddWithValue("@writtenDate", support.WrittenDate);
                    cmd.Parameters.AddWithValue("@SupAmount", support.Amount);
                    if (!string.IsNullOrEmpty(department.AcceptanceType) && !department.AcceptanceType.Contains("زانکۆلاین"))
                    {
                        cmd.Parameters.AddWithValue("@invoiceId", Invoice.InvoiceId);
                        cmd.Parameters.AddWithValue("@invoiceDate", Invoice.InvoiceDate);
                        cmd.Parameters.AddWithValue("@Amount", Invoice.Amount);
                    }



                    SqlParameter sidParam = new SqlParameter("@sidOutput", SqlDbType.Int);
                    sidParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(sidParam);

                    int rf = await cmd.ExecuteNonQueryAsync();
                    isSuccess = rf > 0;
                    int sid = (int)sidParam.Value;
                    if (isSuccess)
                    {
                        if (department.Graduate == "0")
                        {
                            await StudentServices.Insert(sid);
                        }
                        else
                        {
                            await ArchiveService.Insert(sid);
                        }
                        await Task.WhenAll(
                            SavePersonalPic(student.PersonalPicture, sid.ToString(), $@"{FolderName}\\Personal Pictures"),
                            SaveOtherFiles(department.OtherFiles, sid.ToString(), $@"{FolderName}\\Pdf Files"));
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                isSuccess = false;
            }


            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            var model = new InfoModel
            {
                UserId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                isSuccess = isSuccess,
                CountDown = 0
            };
            DbConnectionHelper.Infos.Add(model);
        }

        #endregion
        public static async Task<bool> Update(PersonalStudent student, StudentContactInfo contact,
                                      StudentParentInfo parent, Student12Grade school,
                                      StudentDepartmentInfo department, StudentSupport support)
        {
            bool isSuccess = false;
            string dir = GetTheStudentDir(student), fileName = "0", folder = "/";
            try
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
                        int rf = await cmd.ExecuteNonQueryAsync();
                        isSuccess = rf > 0;
                        fileName = student.Id.ToString();
                        folder = "Student List Images";
                        await StudentServices.Insert(student.Id, true);

                    }
                    else
                    {
                        SqlParameter SidParam = new SqlParameter("@sid", System.Data.SqlDbType.Int);
                        SidParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(SidParam);
                        // acceptance type is difference so student moves
                        // move student from list of current student to the archive list
                        cmd.CommandText = "MoveToArchive";
                        cmd.Parameters.AddWithValue("@AcceptanceType", department.AcceptanceType);
                        cmd.Parameters.AddWithValue("@Graduate", department.Graduate);
                        await cmd.ExecuteNonQueryAsync();
                        int sid = (int)SidParam.Value;
                       

                        if (!string.IsNullOrEmpty(department.AcceptanceType) && !department.AcceptanceType.Contains("زانکۆلاین"))
                        {
                            foreach (var Invoice in StudentServices._Invoices.Where(x => x.SID == student.Id))
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

                        // extras >> when a row gets updated user can not update the Status table within same process so we just get what we have 
                        // then send it to moving parameter to move the datas to the ar_studentStages table
                        // best regards hiwa fucks it
                        foreach (var status in StudentServices._Stages.Where(x => x.Sid == student.Id))
                        {
                            using (SqlCommand invoiceCmd = new SqlCommand("MoveStatusToArchive", DbConnectionHelper.con))
                            {
                                invoiceCmd.CommandType = System.Data.CommandType.StoredProcedure;
                                invoiceCmd.Parameters.AddWithValue("@id", status.id);
                                invoiceCmd.Parameters.AddWithValue("@stage", status.Stage);
                                invoiceCmd.Parameters.AddWithValue("@year", status.Year);
                                invoiceCmd.Parameters.AddWithValue("@status", status.Status);
                                invoiceCmd.Parameters.AddWithValue("@sid", sid);
                                await invoiceCmd.ExecuteNonQueryAsync();
                            }
                        }

                        StudentServices.Delete(student.Id);
                        await ArchiveService.Insert(sid, false);
                        using (SqlCommand AfterMatchCMD = new SqlCommand("Delete from PersonalStudent where id = @id", DbConnectionHelper.con))
                        {
                            AfterMatchCMD.Parameters.AddWithValue("@id", student.Id);
                            int rf = await AfterMatchCMD.ExecuteNonQueryAsync();
                            isSuccess = rf > 0;
                            return isSuccess;
                        }


                    }

                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine($"exxx >> {ex.Message}");
                isSuccess = false;
            }

            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            var model = new InfoModel
            {
                UserId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                isSuccess = isSuccess,
                CountDown = 0
            };
            DbConnectionHelper.Infos.Add(model);
            return isSuccess;
        }

        public static async Task<bool> Ar_Update(PersonalStudent student, StudentContactInfo contact,
                                      StudentParentInfo parent, Student12Grade school,
                                      StudentDepartmentInfo department, StudentSupport support)
        {

            bool isSuccess = false;
            string dir = GetTheStudentDir(student);

            try
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
                        int rf = await cmd.ExecuteNonQueryAsync();
                        isSuccess = rf > 0;
                        await ArchiveService.Insert(student.Id, true);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@AcceptanceType", department.AcceptanceType);
                        SqlParameter SidParam = new SqlParameter("@sid", System.Data.SqlDbType.Int);
                        SidParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(SidParam);
                        // acceptance type is difference so student movies move student from Archive to the list of current student
                        cmd.CommandText = "MoveToStudentList";
                        await cmd.ExecuteNonQueryAsync();
                        int sid = (int)SidParam.Value;
              
                        if (!string.IsNullOrEmpty(department.AcceptanceType) && !department.AcceptanceType.Contains("زانکۆلاین"))
                        {
                            foreach (var Invoice in ArchiveService.ar_Invoices.Where(x => x.SID == student.Id))
                            {
                                try
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
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"Invoice error >> {ex.Message}");

                                }
                            }


                        }

                        // extras >> when a row gets updated user can not update the Status table within same process so we just get what we have 
                        // then send it to moving parameter to move the datas to the ar_studentStages table
                        // best regards hiwa fucks it
                        
                        
                        foreach (var status in ArchiveService.ar_Stages.Where(x => x.Sid == student.Id))
                        {
                            try
                            {
                                using (SqlCommand invoiceCmd = new SqlCommand("MoveStatusToStudentList", DbConnectionHelper.con))
                                {
                                    invoiceCmd.CommandType = System.Data.CommandType.StoredProcedure;
                                    invoiceCmd.Parameters.AddWithValue("@id", status.id);
                                    invoiceCmd.Parameters.AddWithValue("@stage", status.Stage);
                                    invoiceCmd.Parameters.AddWithValue("@year", status.Year);
                                    invoiceCmd.Parameters.AddWithValue("@status", status.Status);
                                    invoiceCmd.Parameters.AddWithValue("@sid", sid);
                                    await invoiceCmd.ExecuteNonQueryAsync();
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"stages error >> {ex.Message}");
                            }
                        }

                        ArchiveService.Delete(student.Id);
                        await StudentServices.Insert(sid, false);
                        using (SqlCommand AfterMatchCMD = new SqlCommand("Delete from Ar_PersonalStudent where id = @id", DbConnectionHelper.con))
                        {
                            AfterMatchCMD.Parameters.AddWithValue("@id", student.Id);
                            int rf = await AfterMatchCMD.ExecuteNonQueryAsync();
                            isSuccess = rf > 0;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"exxx >> {ex.Message}");
                isSuccess = false;


            }

            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            var model = new InfoModel
            {
                UserId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                isSuccess = isSuccess,
                CountDown = 0
            }; DbConnectionHelper.Infos.Add(model);

            return isSuccess;
        }

        static string path = "C:\\Users\\Aurora\\Desktop";
        public static async Task SavePersonalPic(IFormFile file, string FileName, string FolderName)
        {
#if !DEBUG
            path = @$"C:\Users\nwenar\Desktop";
#endif
            if (file != null)
            {

                string fileName = FileName + Path.GetExtension(file.FileName);
                string folderPath = Path.Combine(path, FolderName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string imagePath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
        }
        public static async Task SaveOtherFiles(IEnumerable<IFormFile> files, string FileName, string FolderName)
        {
#if !DEBUG
            path = @$"C:\Users\nwenar\Desktop";
#endif
            if (files != null)
            {
                foreach (var file in files)
                {
                    Random ra = new Random();
                    string fileName = ra.Next() + "_" + FileName + Path.GetExtension(file.FileName);
                    string folderPath = Path.Combine(path, FolderName);

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string imagePath = Path.Combine(folderPath, fileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
        }
        public static string GetTheStudentDir(PersonalStudent student)
        {
            string dir = "C:\\Users\\Aurora\\Desktop\\Student List Images";

            bool isStudent = StudentServices._Student.Any(x => x.Id == student.Id && x.IdentityNo == student.IdentityNo && x.RationingFormNo == student.RationingFormNo);
            if (isStudent)
            {
#if !DEBUG
                dir = @$"C:\Users\nwenar\Desktop\Student List Images";
#endif

            }
            else
            {
                isStudent = ArchiveService.ar_Student.Any(x => x.Id == student.Id && x.IdentityNo == student.IdentityNo && x.RationingFormNo == student.RationingFormNo);
                if (isStudent)
                {
#if !DEBUG
                dir = @$"C:\Users\nwenar\Desktop\Archive List Images";
#endif
                }

            }
            return dir;
        }
        public static async Task DeleteRecord(int id, string procedure)
        {
            string direction = "C:\\Users\\Aurora\\Desktop\\";
#if !DEBUG
            direction = "C:\\Users\\nwenar\\Desktop";
#endif
            using (SqlCommand cmd = new SqlCommand(procedure, DbConnectionHelper.con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                int rf = await cmd.ExecuteNonQueryAsync();
                if (rf > 0)
                {
                    if (procedure == "DeleteStudents")
                    {
                        StudentServices.Delete(id);
                        direction = direction + "Student List Images";
                    }
                    else
                    {
                        ArchiveService.Delete(id);
                        direction = direction + "Archive List Images";

                    }
                    GetDirectionsToDelete(direction,id.ToString());
                }
                IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
                var model = new InfoModel
                {
                    UserId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    isSuccess = rf > 0,
                    CountDown = 0
                };
                DbConnectionHelper.Infos.Add(model);
            }
        }
        public static void GetDirectionsToDelete(string Dir, string id)
        {
            string personalImgDir = Dir + "\\Personal Pictures";
            string OtherFilesDir = Dir + "\\Pdf Files";
            doDelete(personalImgDir, id);
            doDelete(OtherFilesDir, $"_{id}");
        }
        private static void doDelete(string CompleteDirection, string id)
        {
            if (Directory.Exists(CompleteDirection))
            {
                string[] Files = Directory.GetFiles(CompleteDirection);
                foreach (var file in Files)
                {
                    string fileName = Path.GetFileName(file);
                    if (fileName.Contains(id))
                    {
                        File.Delete(file);
                    }
                }
            }
        }
        public static async Task<List<GalleryModel>> GetimagesBack(string Direction,string id)
        {

            List<GalleryModel> _FilesList = new List<GalleryModel>();
            string CompletePath = Direction + "\\Personal Pictures";
            if (Directory.Exists(CompletePath))
            {
                string[] _files = Directory.GetFiles(CompletePath);
                foreach (var item in _files)
                {
                    string fileName = Path.GetFileName(item);
                    if (fileName.Contains(id))
                    {
                        
                        using var stream = new FileStream(CompletePath+"\\"+fileName, FileMode.Open, FileAccess.Read);
                        var memoryStream = new MemoryStream();
                        await stream.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;

                        var formFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", fileName)
                        {
                            Headers = new HeaderDictionary(),
                            ContentType = GetContentType(fileName)
                        };
                        var gallery = new GalleryModel
                        {
                            StudentID = int.Parse(id),
                            _Files = formFile,
                            isPersonal = true,
                        };
                        _FilesList.Add(gallery);
                    }
                }
            }

             CompletePath = Direction + "\\Pdf Files";

            if (Directory.Exists(CompletePath))
            {
                string[] _files = Directory.GetFiles(CompletePath);
                foreach (var item in _files)
                {
                    string fileName = Path.GetFileName(item);
                    if (fileName.Contains("_"+id))
                    {

                        using var stream = new FileStream(CompletePath +"\\"+ fileName, FileMode.Open, FileAccess.Read);
                        var memoryStream = new MemoryStream();
                        await stream.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;

                        var formFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", fileName)
                        {
                            Headers = new HeaderDictionary(),
                            ContentType = GetContentType(fileName)
                        };
                        var gallery = new GalleryModel
                        {
                            StudentID = int.Parse(id),
                            _Files = formFile,
                            isPersonal = false
                        };
                        _FilesList.Add(gallery);
                    }
                }
            }
            return _FilesList;
        }
    
        private static string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".bmp" => "image/bmp",
                ".tiff" or ".tif" => "image/tiff",
                ".webp" => "image/webp",
                ".pdf" => "application/pdf", // Handle PDF files
                _ => "application/octet-stream", // Default if file type is unknown
            };
        }
    }
}
