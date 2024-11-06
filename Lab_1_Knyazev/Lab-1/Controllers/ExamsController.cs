using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab_1.Data;
using Lab_1.Models;
using Lab_1.BLL;
using Microsoft.AspNetCore.Authorization;

namespace Lab_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamsController : ControllerBase
    {
        private readonly Administrator administrator;

        public ExamsController(Administrator administrator)
        {
            this.administrator = administrator;
        }
        
        [HttpPost] // POST: api/Exams
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ExamDTO>> AddExam([FromBody] ExamDTO exam)
        {
            return await administrator.AddExam(exam) ? CreatedAtAction(nameof(GetExams), new { id = exam.Code }, exam) : BadRequest("failed to add exam");
        }
        
        [HttpGet] // GET: api/Exams
        public async Task<ActionResult<IEnumerable<ExamDTO>>> GetExams(DateTime? TimeFrom, DateTime? TimeTo)
        {
            var exams = await administrator.GetExams(TimeFrom, TimeTo);
            if (exams == null)
                return NotFound();
            return exams;
        }
        
        [HttpGet("{code}")] // GET: api/Exams/5
        public async Task<ActionResult<ExamDTO>> GetExam(int code)
        {
            var exam = await administrator.GetExam(code);
            if (exam == null)
                return NotFound();
            return exam;
        }
        
        [HttpGet("{code}/GetStudents")] // GET: api/Exams/5
        [Authorize(Roles = "admin, user")]
        public async Task<ActionResult<List<Student>>> GetStudentsOnExam(int code)
        {
            var students = await administrator.GetStudentOnExam(code);
            if (students == null)
                return NotFound();
            return students;
        }
        
        [HttpPut] // PUT: api/Exams
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutExam([FromBody] ExamDTO exam)
        {
            return await administrator.UpdateExam(exam) ? CreatedAtAction(nameof(GetExams), new { id = exam.Code }, exam) : NotFound();
        }
        
        [HttpDelete("{code}")] // DELETE: api/Exams/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteExam(int code)
        {
            return await administrator.DeleteExam(code) ? Ok() : NotFound();
        }
    }
}
