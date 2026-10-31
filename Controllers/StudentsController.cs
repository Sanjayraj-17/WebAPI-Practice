
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models;

namespace WebAPI_Practice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor
        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        // GET: api/students/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetStudentById),
                new { id = student.Id },
                student
            );
        }

        // PUT: api/students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(
            int id,
            [FromBody] Student student)
        {
            var existingStudent = await _context.Students.FindAsync(id);

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            existingStudent.Age = student.Age;
            existingStudent.Department = student.Department;

            await _context.SaveChangesAsync();

            return Ok(existingStudent);
        }
    }
}

