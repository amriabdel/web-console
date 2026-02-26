using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;

namespace StudentAPI.Data
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; } // Add this line
    }
}