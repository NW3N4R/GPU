using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GPU.Models;

namespace GPU.Data
{
    public class GPUContext : DbContext
    {
        public GPUContext (DbContextOptions<GPUContext> options)
            : base(options)
        {
        }

        public DbSet<GPU.Models.PersonalStudent> PersonalStudent { get; set; } = default!;
        public DbSet<GPU.Models.StudentContactInfo> StudentContactInfo { get; set; } = default!;
        public DbSet<GPU.Models.StudentParentInfo> studentParentinfo { get; set; } = default!;
        public DbSet<GPU.Models.Student12Grade> student12Grade { get; set; } = default!;
        public DbSet<GPU.Models.StudentDepartmentInfo> studentDepartmentInfo { get; set; } = default!;
    }
}
