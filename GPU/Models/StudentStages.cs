using System.ComponentModel;
using System.Runtime.InteropServices;

namespace GPU.Models
{
    public class StudentStages
    {
        public int id { get; set; }

        [DisplayName("قۆناغ")]
        public int Stage { get; set; }

        [DisplayName("ساڵ")]
        public string Year { get; set; }

        [DisplayName("ڕەوش")]
        public string Status { get; set; }

        public string Name { get; set; }
        public int Sid { get; set; }
    }
}
