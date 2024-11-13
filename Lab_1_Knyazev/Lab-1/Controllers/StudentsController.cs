using Lab_1.BLL;
using Lab_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<StudentDTO>> AddStudent([FromBody] StudentDTO stud)
        {
            var student = await administrator.AddStudent(stud);
            return student != null ? CreatedAtAction(nameof(GetStudent), new { id = student.ID }, student) : BadRequest("failed to add student");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            var students = await administrator.GetStudents();
            if (students == null)
                return NotFound();
            return students;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
            var student = await administrator.GetStudent(id);
            if (student == null)
                return NotFound();
            return student;
        }

        [HttpGet("{id}/GetExams")]
        public async Task<ActionResult<IEnumerable<Exam>>> GetStudentExams(int id, bool current)
        {
            var exams = await administrator.GetStudentExams(id, current);
            if (exams == null)
                return NotFound();
            return exams;
        }

        [HttpGet("{id}/GetMarks")]
        [Authorize(Roles = "user, admin")]
        public async Task<ActionResult<Dictionary<int, int>>> GetStudentMarks(int id, int semestr)
        {
            var marks = await administrator.GetStudentMarks(id, semestr);
            if (marks == null)
                return NotFound();
            return marks;
        }

        [HttpGet("{id}/GetAvrMark")]
        [Authorize(Roles = "user, admin")]
        public async Task<ActionResult<int>> GetStudentAvrMark(int id, int semestr)
        {
            var avrMark = await administrator.GetStudentAvrMark(id, semestr);
            if (avrMark == -1)
                return NotFound();
            return avrMark;
        }

        [HttpGet("{id}/GetFails")]
        [Authorize(Roles = "user, admin")]
        public async Task<ActionResult<int>> GetStudentFailCount(int id, int semestr)
        {
            var failCount = await administrator.GetStudentFailCount(id, semestr);
            if (failCount == -1)
                return NotFound();
            return failCount;
        }

        [HttpPut]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> PutStudent([FromBody] StudentDTO stud)
        {
            return (await administrator.UpdateStudent(stud)) ? CreatedAtAction(nameof(GetStudent), new { id = stud.ID }, stud) : NotFound();
        }

        [HttpPut("{id}/ModifyExams")] // При mode == true записывает студента на экзамент, а при false убирает с них (значение семстра при удалении не важно)
        [Authorize(Roles = "admin")] // Dictionary<Ключ(int): code экзамена на котррый нужно записать этого студента, Значение(int): семестр к которому относится экзамен>
        public async Task<IActionResult> ModifyStudentExams(int id, [FromBody] Dictionary<int, int> ExamsID, bool mode)
        {
            return (await administrator.ModifyStudentExams(id, ExamsID, mode)) ? Ok() : NotFound();
        }

        [HttpPut("{id}/AddToExam")]
        [Authorize(Roles = "admin")] // Dictionary<Ключ(int): code экзамена на котррый нужно записать этого студента, Значение(int): семестр к которому относится экзамен>
        public async Task<IActionResult> AddStudentToExam(int id, [FromBody] Dictionary<int, int> ExamsID)
        {
            return (await administrator.ModifyStudentExams(id, ExamsID, true)) ? Ok() : NotFound();
        }

        [HttpPut("{id}/RemoveFromExam")]
        [Authorize(Roles = "admin")] // List<(int): список экзаменов с которых нужно убрать студента>
        public async Task<IActionResult> RemoveStudentFromExam(int id, [FromBody] List<int> ExamsID)
        {
            return (await administrator.ModifyStudentExams(id, ExamsID.Select(ex => new KeyValuePair<int, int>(ex, 0)).ToDictionary(), false)) ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            return await administrator.DeleteStudent(id) ? Ok() : NotFound();
        }
    }
}
