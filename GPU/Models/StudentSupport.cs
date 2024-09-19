using System.ComponentModel;

namespace GPU.Models
{
    public class StudentSupport
    {
        public int id { get; set; }

        [DisplayName("فەرمانگە / بەرێوبەرایەتی")]
        public string? office { get; set; }

        [DisplayName("ژمارەی نووسراو")]
        public string? WrittenNo { get; set; }

        [DisplayName("بەرواری نووسراو")]
        public string? WrittenDate { get; set; }

        [DisplayName("بڕی پارەی وەرگیراو")]
        public string? Amount { get; set; }

        public int sid { get; set; }


    }
}
