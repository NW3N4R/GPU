using System.ComponentModel;

namespace GPU.Models
{
    public class StudentDepartmentInfo
    {
        public int Id { get; set; }


        [DisplayName("بەشی وەرگیراو")]

        public string Department { get; set; }

        [DisplayName("جۆری وەرگرتن")]
        public string AcceptanceType { get; set; }

        [DisplayName("ژ. فەرمانی زانکۆیی")]
        public string UniversityCommandNo { get; set; }
        
        [DisplayName("ژ.فەرمانی کارگێڕی")]
        public string AdministratorCommandNo { get; set; }

        [DisplayName("زنجیرە")]
        public int Seq { get; set; }

        [DisplayName("ساڵی تەواوکردن")]
        public string Graduate { get; set; }
        
        [DisplayName("ساڵی دەستپێک")]
        public string startinYear { get; set; }

        [DisplayName("قۆناغ")]
        public int Stage { get; set; }

        [DisplayName("خوێندکاری بەشە ناوخۆییە؟")]
        public string ResidenceType { get; set; }
        public int SID { get; set; }
    }
}
