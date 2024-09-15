using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GPU.Models
{
    public class PersonalStudent
    {
        [DisplayName("ئاید")]
        public int Id { get; set; }

        [DisplayName("ناو")]
        public required string Name { get; set; }

        [Range(16, 60, ErrorMessage = "تەمەن پێویستە لە نێوان ١٦ بۆ ٦٠ ساڵ بێت")]
        [DisplayName("تەمەن")]
        public required int Age { get; set; }

        [DisplayName("ڕەگەز")]
        public required string Sex { get; set; }


        [DisplayName("ڕەوشی کەسێتی")]
        public required string MartialStatus { get; set; }

        [DisplayName("جۆری خوێن")]
        public required string BloodGroup { get; set; }

        [DisplayName("ئاین")]
        public required string Religion { get; set; }

        [DisplayName("ژ. پێناسی کەسی")]
        public required string IdentityNo { get; set; }

        [DisplayName("نەتەوە")]
        public required string Nationality { get; set; }

        [DisplayName("ژ. فۆرمی بەشە خۆراک")]
        public required string RationingFormNo { get; set; }
    }
}
