using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab_1.Data;
using Lab_1.Models;
using Microsoft.AspNetCore.Authorization;
using Lab_1.BLL;

namespace Lab_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly Administrator administrator;

        public StudentsController(Administrator administrator)
        {
            this.administrator = administrator;
        }

        [HttpPost] // POST: api/Students
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Student>> AddStudent([FromBody] Student student)
        {
            return await administrator.AddStudent(student) ? CreatedAtAction(nameof(GetStudent), new { id = student.ID }, student) : BadRequest("failed to add student");
        }

        [HttpGet] // GET: api/Students
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await administrator.GetStudents();
            if (students == null)
                return NotFound();
            return students;
        }

        [HttpGet("{id}")] // GET: api/Students/5
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await administrator.GetStudent(id);
            if (student == null)
                return NotFound();
            return student;
        }

        [HttpGet("{id}/GetExams")] // GET: api/Students/5/GetExams
        public async Task<ActionResult<IEnumerable<ExamDTO>>> GetStudentExams(int id, bool current)
        {
            var exams = await administrator.GetStudentExams(id, current);
            if (exams == null)
                return NotFound();
            return exams;
        }

        [HttpPut] // PUT: api/Students/5
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> PutStudent([FromBody] Student stud)
        {
            return (await administrator.UpdateStudent(stud)) ? CreatedAtAction(nameof(GetStudent), new { id = stud.ID }, stud) : NotFound();
        }

        [HttpPut("{id}/ModifyExams")] // PUT: api/Students/5/AddOnExams
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> ModifyStudentExams(int id, [FromBody] List<int> ExamsID, bool mode)
        {
            return (await administrator.ModifyStudentExams(id, ExamsID, mode)) ? Ok() : NotFound();
        }

        [HttpDelete("{id}")] // DELETE: api/Students/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            return await administrator.DeleteStudent(id) ? Ok() : NotFound();
        }
    }
}
