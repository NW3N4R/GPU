using GPU.Helpers;
using GPU.Models;
using Microsoft.AspNetCore.Mvc;

namespace GPU.Controllers
{
    public class Archive : Controller
    {

        IEnumerable<StudentTableModel> students = Helper_StudentTable._stu;
        public async Task<IActionResult> Index()
        {
            await Helper_StudentTable.GetStudent("ar_getstudents");
            return View((students, new StudentTableModel()));
        }

        [HttpPost]
        public IActionResult Search([Bind(Prefix = "table")] StudentTableModel model)
        {
            if (!string.IsNullOrEmpty(model.Name) && model.department != "0" && model.StartingYear != "0")
            {
                // search by all
                students = Helper_StudentTable._stu.Where(x => x.Name.Contains(model.Name) && x.department == model.department && x.StartingYear == model.StartingYear);
            }
            else if (!string.IsNullOrEmpty(model.Name) && model.department != "0" && model.StartingYear == "0")
            {
                //search by name and dep
                students = Helper_StudentTable._stu.Where(x => x.Name.Contains(model.Name) && x.department == model.department);
            }
            else if (!string.IsNullOrEmpty(model.Name) && model.department == "0" && model.StartingYear != "0")
            {
                //search by name and starting year
                students = Helper_StudentTable._stu.Where(x => x.Name.Contains(model.Name) && x.StartingYear == model.StartingYear);
            }
            else if (!string.IsNullOrEmpty(model.Name) && model.department == "0" && model.StartingYear == "0")
            {
                // search by name
                students = Helper_StudentTable._stu.Where(x => x.Name.Contains(model.Name));
            }
            else if (string.IsNullOrEmpty(model.Name) && model.department != "0" && model.StartingYear != "0")
            {
                //search by dep and starting year
                students = Helper_StudentTable._stu.Where(x => x.department == model.department && x.StartingYear == model.StartingYear);
            }
            else if (string.IsNullOrEmpty(model.Name) && model.department != "0" && model.StartingYear == "0")
            {
                // search by dep
                students = Helper_StudentTable._stu.Where(x => x.department == model.department);
            }
            else if (string.IsNullOrEmpty(model.Name) && model.department == "0" && model.StartingYear != "0")
            {
                // search by year
                students = Helper_StudentTable._stu.Where(x => x.StartingYear == model.StartingYear);
            }
            else
            {
                // no result return all
                students = Helper_StudentTable._stu;
            }
            var tbl = new StudentTableModel();
            tbl.Name = model.Name;
            tbl.department = model.department;
            tbl.StartingYear = model.StartingYear;
            var viewModel = (StudentTableModel: students, tbl);
            return View("Index", viewModel);

        }
        public async Task<IActionResult> Details(int? id)
        {

            await DbConnectionHelper.LoadAll("ar_");
            if (id == null)
            {
                return NotFound();
            }

            var personalStudent = Helper_PersonalStudent._Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = Helper_StudentContactInfo._Contacts.FirstOrDefault(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = Helper_StudentParentInfo._Parent.FirstOrDefault(x => x.SID == id) as StudentParentInfo;
            var student12Grade = Helper_Student12Grade._Grade.FirstOrDefault(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = Helper_StudentDepartmentInfo._departmen.FirstOrDefault(x => x.SID == id) as StudentDepartmentInfo;
            var invoice = Helper_Invoice._Invoices.FirstOrDefault(x => x.SID == id) as InvoiceInfo;
            var support = Helper_StudentSupport._Supports.FirstOrDefault(x => x.sid == id) as StudentSupport;

            var viewModel = (PersonalStudent: personalStudent, StudentContactInfo: studentContactInfo,
                             StudentParentInfo: studentParentInfo, Student12Grade: student12Grade,
                             StudentDepartmentInfo: studentDepartmentInfo, InvoiceInfo: invoice,
                             StudentSupport: support);


            if (personalStudent == null)
            {
                return NotFound("404");
            }

            return View(viewModel);
        }
    }
}
