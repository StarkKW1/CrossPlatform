using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab_1.Models
{
    public class Exam
    {
        [Key][ScaffoldColumn(false)]
        public int Code { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Professor { get; set; } = string.Empty;
        public int? Auditori { get; set; }
        public List<Student>? Students { get; set; } = new List<Student>();
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now;

        public Exam() { }

        public Exam(ExamDTO ex)
        {
            Code = ex.Code;
            Subject = ex.Subject;
            Professor = ex.Professor;
            Auditori = ex.Auditori;
            StartTime = ex.StartTime;
            EndTime = ex.EndTime;
        }

        public Exam Update(ExamDTO ex)
        {
            Subject = ex.Subject;
            Professor = ex.Professor;
            Auditori = ex.Auditori;
            StartTime = ex.StartTime;
            EndTime = ex.EndTime;
            return this;
        }
    }

    public class ExamDTO
    {
        [Key][ScaffoldColumn(false)]
        public int Code { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Professor { get; set; } = string.Empty;
        public int? Auditori { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now;

        public ExamDTO() { }

        public ExamDTO(Exam ex)
        {
            Code = ex.Code;
            Subject = ex.Subject;
            Professor = ex.Professor;
            Auditori = ex.Auditori;
            StartTime = ex.StartTime;
            EndTime = ex.EndTime;
        }
    }
}
