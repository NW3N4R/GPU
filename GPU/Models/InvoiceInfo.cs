using System.ComponentModel;

namespace GPU.Models
{
    public class InvoiceInfo
    {
        public int id { get; set; }

        [DisplayName("ژمارەی وەسڵ")]
        public string InvoiceId { get; set; }

        [DisplayName("بەرواری وەسڵ")]
        public string InvoiceDate { get; set; }

        [DisplayName("بڕ")]
        public string Amount { get; set; }

        public int SID { get; set; }
    }
}
