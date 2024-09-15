using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GPU.Models
{
    public class StudentContactInfo
    {
        public int Id { get; set; }

        [Display(Name= "ناو")]
        public string StudentPhone { get; set; }

        [DisplayName("ئیمەیڵ")]
        public string StudentEmail { get; set; }

        [DisplayName("پارێزگا")]
        public string Province { get; set; }

        [DisplayName("شار")]
        public string City { get; set; }

        [DisplayName("ناحیە")]
        public string District { get; set; }

        [DisplayName("گوند")]
        public string Village { get; set; }

        public int SID { get; set; }
    }
}
