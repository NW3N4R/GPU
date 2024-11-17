using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace GPU.Models
{
    public class UsersModel
    {
        public int id { get; set; }
      
        [Required]
        [DisplayName("بەکارهێنەر")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("وشەی نهێنی")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool CanDelete { get; set; }

        public bool isWrongPass { get; set; }
        public bool RememberMe { get; set; }
        public int EMPID { get; set; }
    }
}
