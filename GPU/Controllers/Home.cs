using GPU.Helpers;
using GPU.Models;
using Microsoft.AspNetCore.Mvc;

namespace GPU.Controllers
{
    public class Home : Controller
    {
        public async Task<IActionResult> Index()
        {

            List<HomeModel> list = new List<HomeModel>();
            HomeModel model = new HomeModel();  
            model.ITCount = StaticalHelper._Statical.Count(x=>x.Department.Contains("تەکنەلۆژیای زانیاری"));
            model.NurseCount = StaticalHelper._Statical.Count(x=>x.Department.Contains("پەرستاری"));
            model.VitCount = StaticalHelper._Statical.Count(x=>x.Department.Contains("تەکنیکی ڤیتەرنەری"));
            model.FarmingCount = StaticalHelper._Statical.Count(x=>x.Department.Contains("پڕۆژە کشتوکاڵیەکان"));
            model.MeasureMentCount = StaticalHelper._Statical.Count(x=>x.Department.Contains("ڕوو پێوی"));
            model.AdminiCount = StaticalHelper._Statical.Count(x=>x.Department.Contains("کارگێری کار"));

            model.StudentListCount = Helper_PersonalStudent._Student.Count();
            model.ArchiveCount = Helper_PersonalStudent.ar_Student.Count();
            model.RowCount = model.StudentListCount + model.ArchiveCount;

            model.MaleCount = Helper_PersonalStudent._Student.Count(x=>x.Sex.Contains("نێر")) + Helper_PersonalStudent.ar_Student.Count(x => x.Sex.Contains("نێر"));
            model.FemaleCount = Helper_PersonalStudent._Student.Count(x=>x.Sex.Contains("مێ"))+ Helper_PersonalStudent.ar_Student.Count(x => x.Sex.Contains("مێ"));
            
            model.ZankoLine = StaticalHelper._Statical.Count(x => x.AcceptanceType.Contains("زانکۆلاین"));
            model.Parallel = StaticalHelper._Statical.Count(x => x.AcceptanceType.Contains("پاراڵیل"));
            model.Evening = StaticalHelper._Statical.Count(x => x.AcceptanceType.Contains("ئێواران"));
        
            model.Garmian = StaticalHelper._Statical.Count(x => x.Province.Contains("گەرمیان"));
            model.Slemani = StaticalHelper._Statical.Count(x => x.Province.Contains("سلێمانی"));
            model.Hawler = StaticalHelper._Statical.Count(x => x.Province.Contains("هەولێر"));
            model.Karkuk = StaticalHelper._Statical.Count(x => x.Province.Contains("کەرکووک"));
            model.Diala = StaticalHelper._Statical.Count(x => x.Province.Contains("دیالە"));


            list.Add(model);
            int RowCount = model.RowCount;
            return View((list,RowCount));
        }
    }
}
