using Lab_1.BLL;
using Lab_1.Models;
using Microsoft.AspNetCore.Mvc;
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
        
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Exam>> AddExam([FromBody] Exam ex)
        {
            Exam exam = await administrator.AddExam(ex);
            return exam != null ? CreatedAtAction(nameof(GetExams), new { id = exam.Code }, exam) : BadRequest("failed to add exam");
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams(DateTime? TimeFrom, DateTime? TimeTo)
        {
            var exams = await administrator.GetExams(TimeFrom, TimeTo);
            if (exams == null)
                return NotFound();
            return exams.Select(ex => ex).ToList();
        }
        
        [HttpGet("{code}")]
        public async Task<ActionResult<Exam>> GetExam(int code)
        {
            var exam = await administrator.GetExam(code);
            if (exam == null)
                return NotFound();
            return exam;
        }
        
        [HttpGet("{code}/GetStudents")]
        [Authorize(Roles = "admin, user")]
        public async Task<ActionResult<List<StudentDTO>>> GetStudentsOnExam(int code)
        {
            var students = await administrator.GetStudentOnExam(code);
            if (students == null)
                return NotFound();
            return students;
        }

        [HttpGet("{code}/GetStudentsWithFail")]
        [Authorize(Roles = "admin, user")]
        public async Task<ActionResult<List<StudentDTO>>> GetStudentsOnExamWithFail(int code)
        {
            var students = await administrator.GetStudentOnExamWithFail(code);
            if (students == null)
                return NotFound();
            return students;
        }

        [HttpPut("{code}/SetMarks")]
        [Authorize(Roles = "admin")] // Dictionary<Ключ(int): ID студента, Значение(int): оценка за этот экзамен>
        public async Task<IActionResult> SetMarkStudents(int code, Dictionary<int, int> StudentsMarks)
        {
            var status = await administrator.SetMarks(code, StudentsMarks);
            if (status == 1)
                return Ok();
            else if (status == 0)
            {
                return NotFound();
            }
            else
            {
                return BadRequest("Mark value must be in [2, 5]");
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutExam([FromBody] Exam exam)
        {
            return await administrator.UpdateExam(exam) ? Ok() : NotFound();
        }
        
        [HttpDelete("{code}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteExam(int code)
        {
            return await administrator.DeleteExam(code) ? Ok() : NotFound();
        }
    }
}
