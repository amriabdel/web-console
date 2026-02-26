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
        public async Task<ActionResult<IEnumerable<object>>> GetStudents()
        {
            // We use Select to calculate the AverageScore on the fly for the UI
            var students = await _context.Students.Include(s => s.Grades).ToListAsync();
            return Ok(students.Select(s => new {
                s.Id,
                s.Name,
                s.Email,
                Grades = s.Grades,
                AverageScore = s.Grades.Any() ? s.Grades.Average(g => g.Score) : 0
            }));
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPost("{studentId}/grades")]
        public async Task<IActionResult> AddGrade(int studentId, [FromBody] Grade grade)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null) return NotFound();

            grade.StudentId = studentId;
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return Ok(grade);
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

        [HttpDelete("grades/{gradeId}")]
        public async Task<IActionResult> DeleteGrade(int gradeId)
        {
            var grade = await _context.Grades.FindAsync(gradeId);
            
            if (grade == null)
            {
                return NotFound("Grade not found.");
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}