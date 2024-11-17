using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPU.Models
{
    public class UsersAccessToDepsModel
    {
        public int id { get; set; }

        public int usrid { get; set; }

        public int depid { get; set; }

        public string EmpName { get; set; }

        public string DepName { get; set; }
    }
}
