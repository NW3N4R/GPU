using System.ComponentModel;

namespace KTI_DashBoard.Models
{
    public class StudentTableModel
    {

        public int id { get; set; }
        [DisplayName("ناو")]
        public string? Name { get; set; }

        [DisplayName("بەش")]
        public string? department { get; set; }

        [DisplayName("جۆری خوێندن")]
        public string? Acceptance { get; set; }

        [DisplayName("قۆناغ")]
        public int Stage { get; set; }
        
        [DisplayName("ساڵی دەرچوون")]
        public string? Graduate { get; set; }

        [DisplayName("ساڵی دەستپێك")]
        public string? StartingYear { get; set; }

        public bool isFirst { get; set; }


        public bool isLast { get; set; }


    }
}
