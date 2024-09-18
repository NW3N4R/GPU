using System.ComponentModel;

namespace GPU.Models
{
    public class Student12Grade
    {
        public int Id { get; set; }

        [DisplayName("ژ. ئەزموونی")]
        public string ExamNo { get; set; }

        [DisplayName("ناوی قووتابخانە")]
        public string SchoolName { get; set; }

        [DisplayName("بەڕێوبەرایەتی پەروەردە")]
        public string EducationAdministrator { get; set; }

        [DisplayName("جۆری خوێندن")]
        public string EducationType { get; set; }

        [DisplayName("ساڵی دەرچوون")]
        public string Graduation { get; set; }

        [DisplayName("کۆنمرە")]
        public string TotalMark { get; set; }
        public int SID { get; set; }
    }
}
