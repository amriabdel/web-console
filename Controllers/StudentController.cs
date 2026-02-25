using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Models;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _context;

        public StudentController(StudentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(string name)
        {
            var student = new Student { Name = name };
            _context.Students.Add(student);
            
            // SaveChangesAsync() is the async version of SaveChanges()
            await _context.SaveChangesAsync();
            
            return Ok(student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, string name)
        {
            // Use FindAsync() for primary key lookups
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            student.Name = name;
            await _context.SaveChangesAsync();
            
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            
            return NoContent(); 
        }
    }
}