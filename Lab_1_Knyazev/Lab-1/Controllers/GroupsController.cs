using Lab_1.BLL;
using Lab_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly Administrator administrator;

        public GroupsController(Administrator administrator)
        {
            this.administrator = administrator;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<GroupDTO>> AddExam([FromBody] GroupDTO gr)
        {
            var group = await administrator.AddGroup(gr);
            return group != null ? CreatedAtAction(nameof(GetGroup), new { number = group.Number }, group) : BadRequest("failed to add group");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetGroups()
        {
            var groups = await administrator.GetGroups();
            if (groups == null)
                return NotFound();
            return groups;
        }

        [HttpGet("{number}")]
        public async Task<ActionResult<GroupDTO>> GetGroup(string number)
        {
            var group = await administrator.GetGroup(number);
            if (group == null)
                return NotFound();
            return group;
        }

        [HttpGet("{number}/GetStudents")]
        [Authorize(Roles = "admin, user")]
        public async Task<ActionResult<List<StudentDTO>>> GetGroupStudents(string number)
        {
            var students = await administrator.GetGroupStudents(number);
            if (students == null)
                return NotFound();
            return students;
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutGroup([FromBody] GroupDTO gr)
        {
            return await administrator.UpdateGroup(gr) ? Ok() : NotFound();
        }

        [HttpDelete("{number}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteGroup(string number)
        {
            return await administrator.DeleteGroup(number) ? Ok() : NotFound();
        }
    }
}
