using GPU.Models;
using GPU.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace GPU.Helpers
{
    public class Helper_StudentTable
    {
        public static List<StaticalTableModel> _student = new List<StaticalTableModel>();
        public static List<StaticalTableModel> arStudents = new List<StaticalTableModel>();
        public static List<StaticalTableModel> combinedStudents = new List<StaticalTableModel>();
        public static List<StaticalTableModel> GetStudentTable()
        {
            _student.Clear();
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            var users = ManagerServices._auths.Where(x => x.usrid.ToString() == httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var deps = WebPropsServices.MergedProps.Where(x => x.propType == "Department" && users.Any(z => x.id == z.depid));
            foreach (var student in StudentServices._Student.Where(x=> StudentServices._department.Any(dep=>dep.SID ==x.Id && deps.Any(de=>de.Name.Contains(dep.Department)))).ToList())
            {
                var contact = StudentServices._Contacts.FirstOrDefault(x => x.SID == student.Id);
                var school = StudentServices._Grade.FirstOrDefault(x => x.SID == student.Id);
                var dep = StudentServices._department.FirstOrDefault(x => x.SID == student.Id);
                var model = new StaticalTableModel
                {
                    id = student.Id,
                    Name = student.Name,
                    MartialStatus = student.MartialStatus,
                    Age = student.Age,
                    Religion = student.Religion,
                    Nationality = student.Nationality,
                    Sex = student.Sex,
                    Province = contact?.Province,
                    EducationType = school?.EducationType,
                    SchoolGraduation = school?.Graduation,
                    Department = dep?.Department,
                    AcceptanceType = dep?.AcceptanceType,
                    ResidenceType = dep?.ResidenceType,
                    StartingYear = dep?.startinYear,
                    Stage = dep.Stage,
                };
                _student.Add(model);
            }
            return _student;
        }
        public static List<StaticalTableModel> SearchHelper(StaticalTableModel tbl)
        {




            var model = _student.Where(
             x => (!string.IsNullOrWhiteSpace(tbl.Name) ? (x.Name != null && x.Name.Contains(tbl.Name)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.Department) && tbl.Department != "-" ? (x.Department != null && x.Department.Contains(tbl.Department)) : true) &&
            (tbl.Age != 0 ? x.Age == tbl.Age : true) &&
            (!string.IsNullOrWhiteSpace(tbl.MartialStatus) && tbl.MartialStatus != "-" ? (x.MartialStatus != null && x.MartialStatus.Contains(tbl.MartialStatus)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.Religion) && tbl.Religion != "-" ? (x.Religion != null && x.Religion.Contains(tbl.Religion)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.Nationality) && tbl.Nationality != "-" ? (x.Nationality != null && x.Nationality.Contains(tbl.Nationality)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.SchoolGraduation) && tbl.SchoolGraduation != "-" ? (x.SchoolGraduation != null && x.SchoolGraduation.Contains(tbl.SchoolGraduation)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.EducationType) && tbl.EducationType != "-" ? (x.EducationType != null && x.EducationType.Contains(tbl.EducationType)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.Province) && tbl.Province != "-" ? (x.Province != null && x.Province.Contains(tbl.Province)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.AcceptanceType) && tbl.AcceptanceType != "-" ? (x.AcceptanceType != null && x.AcceptanceType.Contains(tbl.AcceptanceType)) : true) &&
            (tbl.Stage != 0 ? x.Stage == tbl.Stage : true) &&
            (!string.IsNullOrWhiteSpace(tbl.ResidenceType) && tbl.ResidenceType != "-" ? (x.ResidenceType != null && x.ResidenceType.Contains(tbl.ResidenceType)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.StartingYear) && tbl.StartingYear != "-" ? (x.StartingYear != null && x.StartingYear.Contains(tbl.StartingYear)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.Sex) && tbl.Sex != "-" ? (x.Sex != null && x.Sex.Contains(tbl.Sex)) : true)).ToList();

            return model;
        }
        public static List<StaticalTableModel> GetArchiveStudentTable()
        {

            arStudents.Clear();
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            var users = ManagerServices._auths.Where(x => x.usrid.ToString() == httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var deps = WebPropsServices.MergedProps.Where(x => x.propType == "Department" && users.Any(z => x.id == z.depid));
            foreach (var student in ArchiveService.ar_Student.Where(x => ArchiveService.ar_department.Any(dep => dep.SID == x.Id && deps.Any(de => de.Name.Contains(dep.Department)))))
            {
                var contact = ArchiveService.ar_Contacts.FirstOrDefault(x => x.SID == student.Id);
                var school = ArchiveService.ar_Grade.FirstOrDefault(x => x.SID == student.Id);
                var dep = ArchiveService.ar_department.FirstOrDefault(x => x.SID == student.Id);
                var model = new StaticalTableModel
                {
                    id = student.Id,
                    Name = student.Name,
                    MartialStatus = student.MartialStatus,
                    Age = student.Age,
                    Religion = student.Religion,
                    Nationality = student.Nationality,
                    Sex = student.Sex,
                    Province = contact?.Province,
                    EducationType = school?.EducationType,
                    SchoolGraduation = school?.Graduation,
                    Department = dep?.Department,
                    AcceptanceType = dep?.AcceptanceType,
                    ResidenceType = dep?.ResidenceType,
                    StartingYear = dep?.startinYear,
                    Stage = dep == null ? 0 : dep.Stage,
                    Graduation = dep?.Graduate,
                };

                arStudents.Add(model);
            }
            return arStudents;
        }
        public static List<StaticalTableModel> ArSearchHelper(StaticalTableModel tbl)
        {
            var model = arStudents.Where(
             x => (!string.IsNullOrWhiteSpace(tbl.Name) ? (x.Name != null && x.Name.Contains(tbl.Name)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.Department) && tbl.Department != "-" ? (x.Department != null && x.Department.Contains(tbl.Department)) : true) &&
            (tbl.Age != 0 ? x.Age == tbl.Age : true) &&
            (!string.IsNullOrWhiteSpace(tbl.MartialStatus) && tbl.MartialStatus != "-" ? (x.MartialStatus != null && x.MartialStatus.Contains(tbl.MartialStatus)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.Religion) && tbl.Religion != "-" ? (x.Religion != null && x.Religion.Contains(tbl.Religion)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.Nationality) && tbl.Nationality != "-" ? (x.Nationality != null && x.Nationality.Contains(tbl.Nationality)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.SchoolGraduation) && tbl.SchoolGraduation != "-" ? (x.SchoolGraduation != null && x.SchoolGraduation.Contains(tbl.SchoolGraduation)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.EducationType) && tbl.EducationType != "-" ? (x.EducationType != null && x.EducationType.Contains(tbl.EducationType)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.Province) && tbl.Province != "-" ? (x.Province != null && x.Province.Contains(tbl.Province)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.AcceptanceType) && tbl.AcceptanceType != "-" ? (x.AcceptanceType != null && x.AcceptanceType.Contains(tbl.AcceptanceType)) : true) &&
            (tbl.Stage != 0 ? x.Stage == tbl.Stage : true) &&
            (!string.IsNullOrWhiteSpace(tbl.ResidenceType) && tbl.ResidenceType != "-" ? (x.ResidenceType != null && x.ResidenceType.Contains(tbl.ResidenceType)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.StartingYear) && tbl.StartingYear != "-" ? (x.StartingYear != null && x.StartingYear.Contains(tbl.StartingYear)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.Sex) && tbl.Sex != "-" ? (x.Sex != null && x.Sex.Contains(tbl.Sex)) : true) &&
            (!string.IsNullOrWhiteSpace(tbl.Graduation) && tbl.Graduation != "-" ? (x.Graduation != null && x.Graduation.Contains(tbl.Graduation)) : true)).ToList();

            return model;
        }
        static IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        public static List<StaticalTableModel> GetStatical()
        {
            combinedStudents.Clear();
            int usrId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var usr = ManagerServices._employees.FirstOrDefault(x => x.id == usrId);

            if (usr.StuList)
            {
                GetStudentTable();
                combinedStudents.AddRange(_student);

            }

            if (usr.ArchList)
            {

                GetArchiveStudentTable();
                combinedStudents.AddRange(arStudents);
            }

            return combinedStudents;
        }
        public static List<StaticalTableModel> StaticalSearch(StaticalTableModel tbl)
        {
            combinedStudents.Clear();

            int usrId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var usr = ManagerServices._employees.FirstOrDefault(x => x.id == usrId);

            if (usr.StuList)
            {
                combinedStudents.AddRange(SearchHelper(tbl));

            }
            if (usr.ArchList)
            {
                combinedStudents.AddRange(ArSearchHelper(tbl));

            }
            return combinedStudents;
        }

    }
}
