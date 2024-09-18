using System.ComponentModel;

namespace GPU.Models
{
    public class StudentParentInfo
    {
        public int Id { get; set; }

        [DisplayName("ناو")]
        public string Name { get; set; }

        [DisplayName("پیشە")]
        public string Profession { get; set; }

        [DisplayName("ژمارە تەلەفوون")]
        public string Phone { get; set; }

        [DisplayName("ئیمەیڵ")]
        public string Email { get; set; }

        [DisplayName("ژمارەی کارتی زانیاری")]
        public string CardInfoNo { get; set; }

        [DisplayName("شوێنی دەرچوونی کارتی زانیاری")]
        public string CardInfoIssuePlace { get; set; }
        public int SID { get; set; }
    }
}
