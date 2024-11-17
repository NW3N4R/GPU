using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel;

namespace GPU.Models
{
    public class HomeModel
    {

       
        public string Title { get; set; }

        public decimal No { get; set; }

        public string Filter { get; set; }
    }
}
