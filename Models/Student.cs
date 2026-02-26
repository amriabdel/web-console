using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
        // A student can have a list of grades
        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}