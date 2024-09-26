using System.ComponentModel;

namespace KTI_DashBoard.Models
{
    public class StudentContactInfo
    {
        public int Id { get; set; }

        [DisplayName("ژمارە موبایل")]
        public string? Phone { get; set; }

        [DisplayName("ئیمەیڵ")]
        public string? StudentEmail { get; set; }

        [DisplayName("پارێزگا")]
        public string? Province { get; set; }

        [DisplayName("شار")]
        public string? City { get; set; }

        [DisplayName("ناحیە")]
        public string? District { get; set; }

        [DisplayName("گوند")]
        public string? Village { get; set; }

        public int SID { get; set; }
    }
}
