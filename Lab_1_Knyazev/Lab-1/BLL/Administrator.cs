using Lab_1.Data;
using Lab_1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab_1.BLL
{
    public class Administrator
    {
        private readonly Lab1Context _context;

        public Administrator(Lab1Context context)
        {
            _context = context;
        }

        #region Group
        public async Task<GroupDTO> AddGroup(GroupDTO gr)
        {
            if (await _context.Groups.FindAsync(gr.Number) != null)
                return null;

            var student = await _context.Students.FindAsync(gr.LeaderID);
            if ((student == null) && (gr.LeaderID != 0))
                return null;

            var group = new Group(gr.Number, student);
            await _context.Groups.AddAsync(group);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return group;
        }

        public async Task<List<GroupDTO>?> GetGroups()
        {
            return await _context.Groups.Include(gr => gr.Leader).Select(gr => (GroupDTO)gr).ToListAsync();
        }

        public async Task<GroupDTO?> GetGroup(string number)
        {
            return (GroupDTO)await _context.Groups.Include(gr => gr.Leader).FirstOrDefaultAsync(gr => gr.Number == number);
        }

        public async Task<List<StudentDTO>?> GetGroupStudents(string number)
        {
            return await _context.Students.Include(st => st.Group).Where(st => st.Group.Number == number).Select(st => (StudentDTO)st).ToListAsync();
        }

        public async Task<bool> UpdateGroup(GroupDTO gr)
        {
            var group = await _context.Groups.FindAsync(gr.Number);
            if (group == null)
                return false;

            var student = await _context.Students.FindAsync(gr.LeaderID);
            if (student == null)
                return false;

            _context.Groups.Update(group.SetLeader(student));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteGroup(string number)
        {
            var group = await _context.Groups.FindAsync(number);
            if (group == null)
                return false;

            _context.Groups.Remove(group);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }
        #endregion



        #region Student
        public async Task<StudentDTO> AddStudent(StudentDTO stud)
        {
            var group = await _context.Groups.FindAsync(stud.GroupNumber);
            if (group == null)
                return null;

            var student = new Student(stud, group);

            _context.Students.AddAsync(student);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return student;
        }

        public async Task<List<StudentDTO>?> GetStudents()
        {
            return await _context.Students.Include(st => st.Group).Select(st => (StudentDTO)st).ToListAsync();
        }

        public async Task<StudentDTO?> GetStudent(int studentId)
        {
            return (StudentDTO)await _context.Students.Include(st => st.Group).FirstOrDefaultAsync(p => p.ID == studentId);
        }

        public async Task<List<Exam>?> GetStudentExams(int studentId, bool current)
        {
            if (_context.Students == null)
                return null;
            var student = await _context.Students.Include(st => st.Sessions).ThenInclude(ses => ses.Exam).FirstOrDefaultAsync(st => st.ID == studentId);
            if (student == null)
                return null;

            if (current)
                return student.Sessions.Where(ses => ses.Exam.Date.ToUniversalTime() >= DateTime.UtcNow).OrderBy(ses => ses.Exam.Date).Select(ses => ses.Exam).ToList();
            else
                return student.Sessions.OrderBy(ses => ses.Exam.Date).Select(ses => ses.Exam).ToList();
        }

        public async Task<Dictionary<int, int>?> GetStudentMarks(int studentId, int semestr = 0)
        {
            if (_context.Students == null)
                return null;
            var student = await _context.Students.Include(st => st.Sessions).ThenInclude(ses => ses.Exam).FirstOrDefaultAsync(st => st.ID == studentId);
            if (student == null)
                return null;
            
            if (semestr == 0)
                return student.Sessions.OrderBy(ses => ses.Exam.Date).Select(ses => new KeyValuePair<int, int>(ses.Exam.Code, ses.Mark)).ToDictionary();
            else
                return student.Sessions.Where(ses => ses.Semestr == semestr).OrderBy(ses => ses.Exam.Date).Select(ses => new KeyValuePair<int, int>(ses.Exam.Code, ses.Mark)).ToDictionary();
        }

        public async Task<int> GetStudentAvrMark(int studentId, int semestr = 0)
        {
            if (_context.Students == null)
                return -1;
            var student = await _context.Students.Include(st => st.Sessions).ThenInclude(ses => ses.Exam).FirstOrDefaultAsync(st => st.ID == studentId);
            if (student == null)
                return -1;

            return student.GetAvrMark(semestr);
        }

        public async Task<int> GetStudentFailCount(int studentId, int semestr = 0)
        {
            if (_context.Students == null)
                return -1;
            var student = await _context.Students.Include(st => st.Sessions).ThenInclude(ses => ses.Exam).FirstOrDefaultAsync(st => st.ID == studentId);
            if (student == null)
                return -1;

            return student.GetFailCount(semestr);
        }

        public async Task<bool> UpdateStudent(StudentDTO stud)
        {
            var student = await _context.Students.Include(st => st.Group).FirstOrDefaultAsync(st => st.ID == stud.ID);
            if (student == null) 
                return false;

            var group = await _context.Groups.FindAsync(stud.GroupNumber);
            if (group == null)
                return false;

            student.Update(stud).Group = group;
            _context.Students.Update(student);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ModifyStudentExams(int id, Dictionary<int, int> ExamsID, bool mode)
        {
            var student = await _context.Students.Include(st => st.Sessions).Include(st => st.Group).FirstOrDefaultAsync(st => st.ID == id);
            if (student == null)
                return false;

            foreach (var examId in ExamsID)
            {
                var exam = await _context.Exams.FindAsync(examId.Key);
                if (exam == null)
                    return false;

                if (!student.ModifyExam(exam, mode, examId.Value))
                    return false;

                _context.Students.Update(student);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return false;

            _context.Students.Remove(student);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }
        #endregion



        #region Exam
        public async Task<Exam> AddExam(Exam exam)
        {
            await _context.Exams.AddAsync(exam);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return exam;
        }

        public async Task<List<Exam>?> GetExams(DateTime? TimeFrom, DateTime? TimeTo)
        {
            if (TimeFrom == null & TimeTo == null)
                return await _context.Exams.ToListAsync();
            else if (TimeFrom == null)
                return await _context.Exams.Where(Exam => Exam.Date <= TimeTo).ToListAsync();
            else if (TimeTo == null)
                return await _context.Exams.Where(Exam => Exam.Date >= TimeFrom).ToListAsync();
            else
                return await _context.Exams.Where(Exam => Exam.Date >= TimeFrom & Exam.Date <= TimeTo).ToListAsync();
        }

        public async Task<Exam?> GetExam(int code)
        {
            var exam = await _context.Exams.FindAsync(code);
            if (exam == null)
                return null;
            return exam;
        }

        public async Task<List<StudentDTO>?> GetStudentOnExam(int code)
        {
            var selectedPeople = from student in _context.Students.Include(st => st.Group)
                                 from session in student.Sessions
                                 where session.Exam.Code == code
                                 select student;

            return await selectedPeople.Select(st => (StudentDTO)st).ToListAsync();
        }

        public async Task<int> SetMarks(int code, Dictionary<int, int> StudentMarks)
        {
            //var selectedPeople = from student in _context.Students
            //                     from session in student.Sessions
            //                     where (session.Exam.Code == code) && (StudentMarks.Keys.Contains(session.Student.ID))
            //                     select student;
            //var exam = await _context.Exams.FindAsync(code);
            //foreach (var student in selectedPeople)
            //{
            //    student.SetMark(exam, StudentMarks[student.ID]);
            //    _context.Students.Update(student);
            //}

            var exam = await _context.Exams.FindAsync(code);
            if (exam == null)
                return 0;

            foreach (var StudentMark in StudentMarks)
            {
                //if ((StudentMark.Value < 2) && (StudentMark.Value > 5))
                //    return -1;
                if ((StudentMark.Value < 2) || (StudentMark.Value > 5))
                    return -1;
                var student = await _context.Students.Include(st => st.Sessions).ThenInclude(ses => ses.Exam).FirstOrDefaultAsync(st => st.ID == StudentMark.Key);
                if (student == null)
                    return 0;
                student.SetMark(exam, StudentMarks[student.ID]);
                _context.Students.Update(student);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
            return 1;
        }

        public async Task<bool> UpdateExam(Exam exam)
        {
            _context.Exams.Update(exam);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteExam(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
                return false;

            _context.Exams.Remove(exam);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
