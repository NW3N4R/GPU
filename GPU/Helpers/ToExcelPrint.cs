using GPU.Models;
using Microsoft.AspNetCore.Components.Web;
using OfficeOpenXml;
using System.Collections.Generic;

namespace GPU.Helpers
{
    public class ToExcelPrint
    {
        public static async Task<MemoryStream> DoPrint(List<StaticalTableModel> students, int pre = 0)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // For non-commercial use
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("لیستی خوێندکاران");

            worksheet.View.RightToLeft = true;

            // Export row numbers as the first column
            int rowIndex = 2;
            for (int i = 1; i <= students.Count(); i++)
            {
                worksheet.Cells[rowIndex, 1].Value = i;
                worksheet.Cells[rowIndex, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[rowIndex, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                rowIndex++;
            }

            #region Headers
            worksheet.Cells[1, 1].Value = "ناوی خوێندکار";
            worksheet.Cells[1, 2].Value = "تەمەن";
            worksheet.Cells[1, 3].Value = "ڕەگەز";
            worksheet.Cells[1, 4].Value = "ڕەوشی کەسی";
            worksheet.Cells[1, 5].Value = "جۆری خوێن";
            worksheet.Cells[1, 6].Value = "ئاین";
            worksheet.Cells[1, 7].Value = "ژ.پێناسی کەسی";
            worksheet.Cells[1, 8].Value = "نەتەوە";
            worksheet.Cells[1, 9].Value = "ژ. فۆرمی بەشە خۆراک";

            worksheet.Cells[1, 10].Value = "ژ.م خوێندکار";
            worksheet.Cells[1, 11].Value = "ئیمەیڵی خوێندکار";
            worksheet.Cells[1, 12].Value = "پارێزگا";
            worksheet.Cells[1, 13].Value = "شار";
            worksheet.Cells[1, 14].Value = "ناحیە";
            worksheet.Cells[1, 15].Value = "گوند";

            worksheet.Cells[1, 16].Value = "ناوی بەخێوکەر";
            worksheet.Cells[1, 17].Value = "پیشەی بەخێوکەر";
            worksheet.Cells[1, 18].Value = "ژمارە تەلەفوونی بەخێوکەر";
            worksheet.Cells[1, 19].Value = "ئیمەیڵی بەخێوکەر";
            worksheet.Cells[1, 20].Value = "ژمارەی کارتی زانیاری";
            worksheet.Cells[1, 21].Value = "شوێنی دەرچوونی کارتی زانیاری";

            worksheet.Cells[1, 22].Value = "ژ. ئەزموونی";
            worksheet.Cells[1, 23].Value = "ناوی قووتابخانە";
            worksheet.Cells[1, 24].Value = "بەڕێوبەرایەتی پەروەردە";
            worksheet.Cells[1, 25].Value = "جۆری خوێندن";
            worksheet.Cells[1, 26].Value = "ساڵی دەرچوون";
            worksheet.Cells[1, 27].Value = "کۆنمرە";

            worksheet.Cells[1, 28].Value = "فەرمانگە / بەرێوبەرایەتی";
            worksheet.Cells[1, 29].Value = "ژمارەی نووسراو";
            worksheet.Cells[1, 30].Value = "بەرواری نووسراو";
            worksheet.Cells[1, 31].Value = "بڕی وەرگیراو";

            worksheet.Cells[1, 32].Value = "بەشی وەرگیراو";
            worksheet.Cells[1, 33].Value = "جۆری وەرگرتن";
            worksheet.Cells[1, 34].Value = "ژ. فەرمانی زانکۆیی";
            worksheet.Cells[1, 35].Value = "ژ.فەرمانی کارگێڕی";
            worksheet.Cells[1, 36].Value = "زنجیرە";
            worksheet.Cells[1, 37].Value = "ساڵی دەستپێک";
            worksheet.Cells[1, 38].Value = "ساڵی تەواوکردن";
            worksheet.Cells[1, 39].Value = "قۆناغ";
            worksheet.Cells[1, 40].Value = "خوێندکاری بەشە ناوخۆییە؟";
            #endregion

            rowIndex = 2;
            foreach (var item in students)
            {
                bool isFound = false;
                if (pre == 0 || pre == 2)
                {
                    isFound = Helper_PersonalStudent._Student?.Any(x => x.Id == item.id) ?? false;
                    worksheet.Cells[rowIndex, 1].Value = Helper_PersonalStudent._Student?.FirstOrDefault(x => x.Id == item.id)?.Name;
                    worksheet.Cells[rowIndex, 2].Value = Helper_PersonalStudent._Student?.FirstOrDefault(x => x.Id == item.id)?.Age;
                    worksheet.Cells[rowIndex, 3].Value = Helper_PersonalStudent._Student?.FirstOrDefault(x => x.Id == item.id)?.Sex;
                    worksheet.Cells[rowIndex, 4].Value = Helper_PersonalStudent._Student?.FirstOrDefault(x => x.Id == item.id)?.MartialStatus;
                    worksheet.Cells[rowIndex, 5].Value = Helper_PersonalStudent._Student?.FirstOrDefault(x => x.Id == item.id)?.BloodGroup;
                    worksheet.Cells[rowIndex, 6].Value = Helper_PersonalStudent._Student?.FirstOrDefault(x => x.Id == item.id)?.Religion;
                    worksheet.Cells[rowIndex, 7].Value = Helper_PersonalStudent._Student?.FirstOrDefault(x => x.Id == item.id)?.IdentityNo;
                    worksheet.Cells[rowIndex, 8].Value = Helper_PersonalStudent._Student?.FirstOrDefault(x => x.Id == item.id)?.Nationality;
                    worksheet.Cells[rowIndex, 9].Value = Helper_PersonalStudent._Student?.FirstOrDefault(x => x.Id == item.id)?.RationingFormNo;

                    worksheet.Cells[rowIndex, 10].Value = Helper_StudentContactInfo._Contacts?.FirstOrDefault(x => x.SID == item.id)?.Phone;
                    worksheet.Cells[rowIndex, 11].Value = Helper_StudentContactInfo._Contacts?.FirstOrDefault(x => x.SID == item.id)?.StudentEmail;
                    worksheet.Cells[rowIndex, 12].Value = Helper_StudentContactInfo._Contacts?.FirstOrDefault(x => x.SID == item.id)?.Province;
                    worksheet.Cells[rowIndex, 13].Value = Helper_StudentContactInfo._Contacts?.FirstOrDefault(x => x.SID == item.id)?.City;
                    worksheet.Cells[rowIndex, 14].Value = Helper_StudentContactInfo._Contacts?.FirstOrDefault(x => x.SID == item.id)?.District;
                    worksheet.Cells[rowIndex, 15].Value = Helper_StudentContactInfo._Contacts?.FirstOrDefault(x => x.SID == item.id)?.Village;

                    worksheet.Cells[rowIndex, 16].Value = Helper_StudentParentInfo._Parent?.FirstOrDefault(x => x.SID == item.id)?.Name;
                    worksheet.Cells[rowIndex, 17].Value = Helper_StudentParentInfo._Parent?.FirstOrDefault(x => x.SID == item.id)?.Profession;
                    worksheet.Cells[rowIndex, 18].Value = Helper_StudentParentInfo._Parent?.FirstOrDefault(x => x.SID == item.id)?.Phone;
                    worksheet.Cells[rowIndex, 19].Value = Helper_StudentParentInfo._Parent?.FirstOrDefault(x => x.SID == item.id)?.Email;
                    worksheet.Cells[rowIndex, 20].Value = Helper_StudentParentInfo._Parent?.FirstOrDefault(x => x.SID == item.id)?.CardInfoNo;
                    worksheet.Cells[rowIndex, 21].Value = Helper_StudentParentInfo._Parent?.FirstOrDefault(x => x.SID == item.id)?.CardInfoIssuePlace;

                    worksheet.Cells[rowIndex, 22].Value = Helper_Student12Grade._Grade?.FirstOrDefault(x => x.SID == item.id)?.ExamNo;
                    worksheet.Cells[rowIndex, 23].Value = Helper_Student12Grade._Grade?.FirstOrDefault(x => x.SID == item.id)?.SchoolName;
                    worksheet.Cells[rowIndex, 24].Value = Helper_Student12Grade._Grade?.FirstOrDefault(x => x.SID == item.id)?.EducationAdministrator;
                    worksheet.Cells[rowIndex, 25].Value = Helper_Student12Grade._Grade?.FirstOrDefault(x => x.SID == item.id)?.EducationType;
                    worksheet.Cells[rowIndex, 26].Value = Helper_Student12Grade._Grade?.FirstOrDefault(x => x.SID == item.id)?.Graduation;
                    worksheet.Cells[rowIndex, 27].Value = Helper_Student12Grade._Grade?.FirstOrDefault(x => x.SID == item.id)?.TotalMark;

                    worksheet.Cells[rowIndex, 28].Value = Helper_StudentSupport._Supports?.FirstOrDefault(x => x.sid == item.id)?.office;
                    worksheet.Cells[rowIndex, 29].Value = Helper_StudentSupport._Supports?.FirstOrDefault(x => x.sid == item.id)?.WrittenNo;
                    worksheet.Cells[rowIndex, 30].Value = Helper_StudentSupport._Supports?.FirstOrDefault(x => x.sid == item.id)?.WrittenDate;
                    worksheet.Cells[rowIndex, 31].Value = Helper_StudentSupport._Supports?.FirstOrDefault(x => x.sid == item.id)?.Amount;

                    worksheet.Cells[rowIndex, 32].Value = Helper_StudentDepartmentInfo._department?.FirstOrDefault(x => x.SID == item.id)?.Department;
                    worksheet.Cells[rowIndex, 33].Value = Helper_StudentDepartmentInfo._department?.FirstOrDefault(x => x.SID == item.id)?.AcceptanceType;
                    worksheet.Cells[rowIndex, 34].Value = Helper_StudentDepartmentInfo._department?.FirstOrDefault(x => x.SID == item.id)?.UniversityCommandNo;
                    worksheet.Cells[rowIndex, 35].Value = Helper_StudentDepartmentInfo._department?.FirstOrDefault(x => x.SID == item.id)?.AdministratorCommandNo;
                    worksheet.Cells[rowIndex, 36].Value = Helper_StudentDepartmentInfo._department?.FirstOrDefault(x => x.SID == item.id)?.Seq;
                    worksheet.Cells[rowIndex, 37].Value = Helper_StudentDepartmentInfo._department?.FirstOrDefault(x => x.SID == item.id)?.startinYear;
                    worksheet.Cells[rowIndex, 38].Value = Helper_StudentDepartmentInfo._department?.FirstOrDefault(x => x.SID == item.id)?.Graduate;
                    worksheet.Cells[rowIndex, 39].Value = Helper_StudentDepartmentInfo._department?.FirstOrDefault(x => x.SID == item.id)?.Stage;
                    worksheet.Cells[rowIndex, 40].Value = Helper_StudentDepartmentInfo._department?.FirstOrDefault(x => x.SID == item.id)?.ResidenceType;
                }
                if (pre == 1 || pre == 2)
                {
                    if (isFound) { rowIndex++; }
                    worksheet.Cells[rowIndex, 1].Value = Helper_PersonalStudent.ar_Student?.FirstOrDefault(x => x.Id == item.id)?.Name;
                    worksheet.Cells[rowIndex, 2].Value = Helper_PersonalStudent.ar_Student?.FirstOrDefault(x => x.Id == item.id)?.Age;
                    worksheet.Cells[rowIndex, 3].Value = Helper_PersonalStudent.ar_Student?.FirstOrDefault(x => x.Id == item.id)?.Sex;
                    worksheet.Cells[rowIndex, 4].Value = Helper_PersonalStudent.ar_Student?.FirstOrDefault(x => x.Id == item.id)?.MartialStatus;
                    worksheet.Cells[rowIndex, 5].Value = Helper_PersonalStudent.ar_Student?.FirstOrDefault(x => x.Id == item.id)?.BloodGroup;
                    worksheet.Cells[rowIndex, 6].Value = Helper_PersonalStudent.ar_Student?.FirstOrDefault(x => x.Id == item.id)?.Religion;
                    worksheet.Cells[rowIndex, 7].Value = Helper_PersonalStudent.ar_Student?.FirstOrDefault(x => x.Id == item.id)?.IdentityNo;
                    worksheet.Cells[rowIndex, 8].Value = Helper_PersonalStudent.ar_Student?.FirstOrDefault(x => x.Id == item.id)?.Nationality;
                    worksheet.Cells[rowIndex, 9].Value = Helper_PersonalStudent.ar_Student?.FirstOrDefault(x => x.Id == item.id)?.RationingFormNo;

                    worksheet.Cells[rowIndex, 10].Value = Helper_StudentContactInfo.ar_Contacts?.FirstOrDefault(x => x.SID == item.id)?.Phone;
                    worksheet.Cells[rowIndex, 11].Value = Helper_StudentContactInfo.ar_Contacts?.FirstOrDefault(x => x.SID == item.id)?.StudentEmail;
                    worksheet.Cells[rowIndex, 12].Value = Helper_StudentContactInfo.ar_Contacts?.FirstOrDefault(x => x.SID == item.id)?.Province;
                    worksheet.Cells[rowIndex, 13].Value = Helper_StudentContactInfo.ar_Contacts?.FirstOrDefault(x => x.SID == item.id)?.City;
                    worksheet.Cells[rowIndex, 14].Value = Helper_StudentContactInfo.ar_Contacts?.FirstOrDefault(x => x.SID == item.id)?.District;
                    worksheet.Cells[rowIndex, 15].Value = Helper_StudentContactInfo.ar_Contacts?.FirstOrDefault(x => x.SID == item.id)?.Village;

                    worksheet.Cells[rowIndex, 16].Value = Helper_StudentParentInfo.ar_Parent?.FirstOrDefault(x => x.SID == item.id)?.Name;
                    worksheet.Cells[rowIndex, 17].Value = Helper_StudentParentInfo.ar_Parent?.FirstOrDefault(x => x.SID == item.id)?.Profession;
                    worksheet.Cells[rowIndex, 18].Value = Helper_StudentParentInfo.ar_Parent?.FirstOrDefault(x => x.SID == item.id)?.Phone;
                    worksheet.Cells[rowIndex, 19].Value = Helper_StudentParentInfo.ar_Parent?.FirstOrDefault(x => x.SID == item.id)?.Email;
                    worksheet.Cells[rowIndex, 20].Value = Helper_StudentParentInfo.ar_Parent?.FirstOrDefault(x => x.SID == item.id)?.CardInfoNo;
                    worksheet.Cells[rowIndex, 21].Value = Helper_StudentParentInfo.ar_Parent?.FirstOrDefault(x => x.SID == item.id)?.CardInfoIssuePlace;

                    worksheet.Cells[rowIndex, 22].Value = Helper_Student12Grade.ar_Grade?.FirstOrDefault(x => x.SID == item.id)?.ExamNo;
                    worksheet.Cells[rowIndex, 23].Value = Helper_Student12Grade.ar_Grade?.FirstOrDefault(x => x.SID == item.id)?.SchoolName;
                    worksheet.Cells[rowIndex, 24].Value = Helper_Student12Grade.ar_Grade?.FirstOrDefault(x => x.SID == item.id)?.EducationAdministrator;
                    worksheet.Cells[rowIndex, 25].Value = Helper_Student12Grade.ar_Grade?.FirstOrDefault(x => x.SID == item.id)?.EducationType;
                    worksheet.Cells[rowIndex, 26].Value = Helper_Student12Grade.ar_Grade?.FirstOrDefault(x => x.SID == item.id)?.Graduation;
                    worksheet.Cells[rowIndex, 27].Value = Helper_Student12Grade.ar_Grade?.FirstOrDefault(x => x.SID == item.id)?.TotalMark;

                    worksheet.Cells[rowIndex, 28].Value = Helper_StudentSupport.ar_Supports?.FirstOrDefault(x => x.sid == item.id)?.office;
                    worksheet.Cells[rowIndex, 29].Value = Helper_StudentSupport.ar_Supports?.FirstOrDefault(x => x.sid == item.id)?.WrittenNo;
                    worksheet.Cells[rowIndex, 30].Value = Helper_StudentSupport.ar_Supports?.FirstOrDefault(x => x.sid == item.id)?.WrittenDate;
                    worksheet.Cells[rowIndex, 31].Value = Helper_StudentSupport.ar_Supports?.FirstOrDefault(x => x.sid == item.id)?.Amount;

                    worksheet.Cells[rowIndex, 32].Value = Helper_StudentDepartmentInfo.ar_department?.FirstOrDefault(x => x.SID == item.id)?.Department;
                    worksheet.Cells[rowIndex, 33].Value = Helper_StudentDepartmentInfo.ar_department?.FirstOrDefault(x => x.SID == item.id)?.AcceptanceType;
                    worksheet.Cells[rowIndex, 34].Value = Helper_StudentDepartmentInfo.ar_department?.FirstOrDefault(x => x.SID == item.id)?.UniversityCommandNo;
                    worksheet.Cells[rowIndex, 35].Value = Helper_StudentDepartmentInfo.ar_department?.FirstOrDefault(x => x.SID == item.id)?.AdministratorCommandNo;
                    worksheet.Cells[rowIndex, 36].Value = Helper_StudentDepartmentInfo.ar_department?.FirstOrDefault(x => x.SID == item.id)?.Seq;
                    worksheet.Cells[rowIndex, 37].Value = Helper_StudentDepartmentInfo.ar_department?.FirstOrDefault(x => x.SID == item.id)?.startinYear;
                    worksheet.Cells[rowIndex, 38].Value = Helper_StudentDepartmentInfo.ar_department?.FirstOrDefault(x => x.SID == item.id)?.Graduate;
                    worksheet.Cells[rowIndex, 39].Value = Helper_StudentDepartmentInfo.ar_department?.FirstOrDefault(x => x.SID == item.id)?.Stage;
                    worksheet.Cells[rowIndex, 40].Value = Helper_StudentDepartmentInfo.ar_department?.FirstOrDefault(x => x.SID == item.id)?.ResidenceType;
                }
                if (!isFound)
                {
                    rowIndex++;
                }
            }

            worksheet.Cells[rowIndex - 1, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

            var startCell = worksheet.Cells[1, 1];
            var endCell = worksheet.Cells[rowIndex - 1, 40];
            var tableRange = worksheet.Cells[startCell.Address + ":" + endCell.Address];

            var table = worksheet.Tables.Add(tableRange, "MyTable");
            var stream = new MemoryStream();
            excelPackage.SaveAs(stream);
            stream.Position = 0;
            return stream;
        }
    }
}
