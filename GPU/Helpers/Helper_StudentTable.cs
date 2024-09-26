using GPU.Models;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;

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
            foreach (var student in Helper_PersonalStudent._Student)
            {
                var contact = Helper_StudentContactInfo._Contacts.FirstOrDefault(x => x.SID == student.Id);
                var school = Helper_Student12Grade._Grade.FirstOrDefault(x => x.SID == student.Id);
                var dep = Helper_StudentDepartmentInfo._department.FirstOrDefault(x => x.SID == student.Id);
                var model = new StaticalTableModel
                {
                    id = student.Id,
                    Name = student.Name,
                    MartialStatus = student.MartialStatus,
                    Age = student.Age,
                    Religion = student.Religion,
                    Nationality = student.Nationality,
                    Province = contact?.Province,
                    EducationType = school?.EducationType,
                    SchoolGraduation = school?.Graduation,
                    Department = dep?.Department,
                    AcceptanceType = dep?.AcceptanceType,
                    ResidenceType = dep?.ResidenceType,
                    StartingYear = dep?.startinYear,
                    Stage = dep.Stage
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
            (!string.IsNullOrWhiteSpace(tbl.StartingYear) && tbl.StartingYear != "-" ? (x.StartingYear != null && x.StartingYear.Contains(tbl.StartingYear)) : true)).ToList();

            return model;
        }
        public static List<StaticalTableModel> GetArchiveStudentTable()
        {

            arStudents.Clear();
            foreach (var student in Helper_PersonalStudent.ar_Student)
            {
                var contact = Helper_StudentContactInfo.ar_Contacts.FirstOrDefault(x => x.SID == student.Id);
                var school = Helper_Student12Grade.ar_Grade.FirstOrDefault(x => x.SID == student.Id);
                var dep = Helper_StudentDepartmentInfo.ar_department.FirstOrDefault(x => x.SID == student.Id);
                var model = new StaticalTableModel
                {
                    id = student.Id,
                    Name = student.Name,
                    MartialStatus = student.MartialStatus,
                    Age = student.Age,
                    Religion = student.Religion,
                    Nationality = student.Nationality,
                    Province = contact?.Province,
                    EducationType = school?.EducationType,
                    SchoolGraduation = school?.Graduation,
                    Department = dep?.Department,
                    AcceptanceType = dep?.AcceptanceType,
                    ResidenceType = dep?.ResidenceType,
                    StartingYear = dep?.startinYear,
                    Stage = dep.Stage
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
            (!string.IsNullOrWhiteSpace(tbl.StartingYear) && tbl.StartingYear != "-" ? (x.StartingYear != null && x.StartingYear.Contains(tbl.StartingYear)) : true)).ToList();

            return model;
        }
        public static List<StaticalTableModel> GetStatical()
        {
            combinedStudents.Clear();
            GetStudentTable();
            GetArchiveStudentTable();
            combinedStudents.AddRange(_student);
            combinedStudents.AddRange(arStudents);
            return combinedStudents;
        }
        public static List<StaticalTableModel> StaticalSearch(StaticalTableModel tbl)
        {
            combinedStudents.Clear();
            combinedStudents.AddRange(SearchHelper(tbl));
            combinedStudents.AddRange(ArSearchHelper(tbl));
            return combinedStudents;
        }

    }
}
