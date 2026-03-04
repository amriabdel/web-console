using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Models;
using StudentAPI.DTOs;

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
        public async Task<ActionResult> GetStudents()
        {
            var students = await _context.Students
                .Include(s => s.Grades)
                .ToListAsync();

            var result = students.Select(s => new
            {
                s.Id,
                s.Name,
                s.Email,
                Grades = s.Grades.Select(g => new
                {
                    g.Id,
                    g.Subject,
                    g.Score
                }),
                AverageScore = s.Grades.Any()
                    ? s.Grades.Average(g => g.Score)
                    : 0
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentCreateDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Email = dto.Email
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudents), new { id = student.Id }, student);
        }

        [HttpPost("{studentId}/grades")]
        public async Task<IActionResult> AddGrade(int studentId, [FromBody] GradeCreateDto dto)
        {
            var student = await _context.Students.FindAsync(studentId);

            if (student == null)
                return NotFound("Student not found");

            var grade = new Grade
            {
                StudentId = studentId,
                Subject = dto.Subject,
                Score = dto.Score
            };

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            return Ok(grade);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("grades/{gradeId}")]
        public async Task<IActionResult> DeleteGrade(int gradeId)
        {
            var grade = await _context.Grades.FindAsync(gradeId);

            if (grade == null)
                return NotFound();

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}