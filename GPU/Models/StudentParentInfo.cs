using System.ComponentModel;

namespace GPU.Models
{
    public class StudentParentInfo
    {
        public int Id { get; set; }

        [DisplayName("ناوی بەخێوکەر")]
        public string Name { get; set; }

        [DisplayName("پیشەی بەخێوکەر")]
        public string Profession { get; set; }

        [DisplayName("ژمارە تەلەفوونی بەخێوکەر")]
        public string Phone { get; set; }

        [DisplayName("ئیمەیڵی بەخێوکەر")]
        public string Email { get; set; }

        [DisplayName("ژمارەی کارتی زانیاری")]
        public string CardInfoNo { get; set; }

        [DisplayName("شوێنی دەرچوونی کارتی زانیاری")]
        public string CardInfoIssuePlace { get; set; }
        public int SID { get; set; }
    }
}
