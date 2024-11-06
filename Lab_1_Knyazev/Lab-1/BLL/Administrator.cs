using Lab_1.Data;
using Lab_1.Models;
using Microsoft.EntityFrameworkCore;
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

        #region Student
        public async Task<bool> AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
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

        public async Task<List<Student>?> GetStudents()
        {
            //var Students = await _context.Students.Include("Exams").ToListAsync();
            //return Students.Select(pass => new StudentDTO(pass)).ToList();
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetStudent(int studentId)
        {
            return await _context.Students.FirstOrDefaultAsync(p => p.ID == studentId);
        }

        public async Task<List<ExamDTO>?> GetStudentExams(int studentId, bool current)
        {
            var exams = from exam in _context.Exams
                                 from stud in exam.Students
                                 where stud.ID == studentId
                                 select exam;
            if (current)
            {
                return await exams.Where(ex => ex.StartTime >= DateTime.UtcNow).OrderBy(ex => ex.StartTime).Select(ex => new ExamDTO(ex)).ToListAsync();
            }
            else
            {
                return await exams.OrderBy(ex => ex.StartTime).Select(ex => new ExamDTO(ex)).ToListAsync();
            }
        }

        public async Task<bool> UpdateStudent(Student stud)
        {
            _context.Students.Update(stud);

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

        public async Task<bool> ModifyStudentExams(int id, List<int> ExamsID, bool mode)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return false;

            foreach (var examId in ExamsID)
            {
                var exam = await _context.Exams.FindAsync(examId);
                if (exam == null)
                    return false;
                if (exam.StartTime.ToUniversalTime() >= DateTime.UtcNow.AddHours(1))
                {
                    if (mode)
                        exam.Students.Add(student);
                    else
                        exam.Students.Remove(student);
                }
                else
                    return false;
            }

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
        public async Task<bool> AddExam(ExamDTO ex)
        {
            //var exam = await _context.Exams.FindAsync(ex.Code);
            //if (exam != null)
            //    return false;

            await _context.Exams.AddAsync(new Exam(ex));

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

        public async Task<List<ExamDTO>?> GetExams(DateTime? TimeFrom, DateTime? TimeTo)
        {
            if (TimeFrom == null & TimeTo == null)
                return await _context.Exams.Select(u => new ExamDTO(u)).ToListAsync();
            else if (TimeFrom == null)
                return await _context.Exams.Where(Exam => Exam.StartTime <= TimeTo).Select(u => new ExamDTO(u)).ToListAsync();
            else if (TimeTo == null)
                return await _context.Exams.Where(Exam => Exam.StartTime >= TimeFrom).Select(u => new ExamDTO(u)).ToListAsync();
            else
                return await _context.Exams.Where(Exam => Exam.StartTime >= TimeFrom & Exam.StartTime <= TimeTo).Select(u => new ExamDTO(u)).ToListAsync();
        }

        public async Task<ExamDTO?> GetExam(int code)
        {
            var ex = await _context.Exams.FindAsync(code);
            if (ex == null)
                return null;
            return new ExamDTO(ex);
        }

        public async Task<List<Student>?> GetStudentOnExam(int code)
        {
            var exam = await _context.Exams.Include("Students").FirstOrDefaultAsync(ex => ex.Code == code);
            if (exam == null) 
                return null;
            return exam.Students;
        }

        public async Task<bool> UpdateExam(ExamDTO ex)
        {
            var exam = await _context.Exams.FindAsync(ex.Code);
            if (exam == null)
                return false;

            _context.Exams.Update(exam.Update(ex));

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
