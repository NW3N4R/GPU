using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GPU.Models
{
    public class InvoiceInfo
    {
        public int id { get; set; }

        [DisplayName("ناوی خوێندکار")]
        public string Name { get; set; }

        [DisplayName("ژمارەی وەسڵ")]
        public string? InvoiceId { get; set; }

        [DisplayName("بەرواری وەسڵ")]
        public string? InvoiceDate { get; set; }

        [DisplayName("بڕ")]
        public string? Amount { get; set; }

        public bool isFirst { get; set; }

        public bool isArchive { get; set; }

        public int SID { get; set; }
    }
}
