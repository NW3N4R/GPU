using GPU.Helpers;
using GPU.Models;
using Microsoft.AspNetCore.Mvc;

namespace GPU.Controllers
{
    public class Home : Controller
    {
        public async Task<IActionResult> Index()
        {


            ViewData["InfoMessage"] = "This is an information bar message!";

            await DbConnectionHelper.LoadAll();
            #region MyRegion
            List<HomeModel> list = new List<HomeModel>();
            HomeModel model = new HomeModel();
            var dep = Helper_StudentDepartmentInfo._department;
            var ardep = Helper_StudentDepartmentInfo.ar_department;
            model.ITCount = dep.Count(x => x.Department.Contains("تەکنەلۆجیای زانیاری")) + ardep.Count(x => x.Department.Contains("تەکنەلۆژیای زانیاری"));
            model.NurseCount = dep.Count(x => x.Department.Contains("پەرستاری")) + ardep.Count(x => x.Department.Contains("پەرستاری"));
            model.VitCount = dep.Count(x => x.Department.Contains("تەکنیکی ڤیتەرنەری")) + ardep.Count(x => x.Department.Contains("تەکنیکی ڤیتەرنەری"));
            model.FarmingCount = dep.Count(x => x.Department.Contains("پڕۆژە کشتوکاڵیەکان")) + ardep.Count(x => x.Department.Contains("پڕۆژە کشتوکاڵیەکان"));
            model.MeasureMentCount = dep.Count(x => x.Department.Contains("ڕوو پێوی")) + ardep.Count(x => x.Department.Contains("ڕوو پێوی"));
            model.AdminiCount = dep.Count(x => x.Department.Contains("کارگێری کار")) + ardep.Count(x => x.Department.Contains("کارگێری کار"));

            model.StudentListCount = Helper_PersonalStudent._Student.Count();
            model.ArchiveCount = Helper_PersonalStudent.ar_Student.Count();
            model.RowCount = model.StudentListCount + model.ArchiveCount;

            model.MaleCount = Helper_PersonalStudent._Student.Count(x => x.Sex.Contains("نێر")) + Helper_PersonalStudent.ar_Student.Count(x => x.Sex.Contains("نێر"));
            model.FemaleCount = Helper_PersonalStudent._Student.Count(x => x.Sex.Contains("مێ")) + Helper_PersonalStudent.ar_Student.Count(x => x.Sex.Contains("مێ"));

            model.ZankoLine = dep.Count(x => x.AcceptanceType.Contains("زانکۆلاین")) + ardep.Count(x => x.AcceptanceType.Contains("زانکۆلاین"));
            model.Parallel = dep.Count(x => x.AcceptanceType.Contains("پاراڵیل"))+  ardep.Count(x => x.AcceptanceType.Contains("پاراڵیل"));
            model.Evening = dep.Count(x => x.AcceptanceType.Contains("ئێواران")) + ardep.Count(x => x.AcceptanceType.Contains("ئێواران"));


            var contact = Helper_StudentContactInfo._Contacts;
            var arcontact = Helper_StudentContactInfo.ar_Contacts;
            model.Garmian = contact.Count(x => x.Province.Contains("گەرمیان")) + arcontact.Count(x => x.Province.Contains("گەرمیان"));
            model.Slemani = contact.Count(x => x.Province.Contains("سلێمانی")) + arcontact.Count(x => x.Province.Contains("سلێمانی"));
            model.Hawler = contact.Count(x => x.Province.Contains("هەولێر")) + arcontact.Count(x => x.Province.Contains("هەولێر"));
            model.Karkuk = contact.Count(x => x.Province.Contains("کەرکووک")) + arcontact.Count(x => x.Province.Contains("کەرکووک"));
            model.Diala = contact.Count(x => x.Province.Contains("دیالە")) + arcontact.Count(x => x.Province.Contains("دیالە"));
            #endregion


            list.Add(model);
            int RowCount = model.RowCount;
            return View((list, RowCount));
        }
    }
}
