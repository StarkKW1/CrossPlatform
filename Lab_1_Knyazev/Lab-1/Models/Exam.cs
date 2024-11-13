using System.ComponentModel.DataAnnotations;

namespace Lab_1.Models
{
    public class Exam
    {
        [Key][ScaffoldColumn(false)]
        public int Code { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Professor { get; set; } = string.Empty;
        public int Auditori { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.Now;

        //public static implicit operator Exam(ExamDTO ex)
        //{
        //    var exam = new Exam();
        //    exam.Code = ex.Code;
        //    exam.Subject = ex.Subject;
        //    //exam.Professor = ex.Professor;
        //    exam.Auditori = ex.Auditori;
        //    exam.StartTime = ex.StartTime;
        //    exam.EndTime = ex.EndTime;
        //    return exam;
        //}

        //public Exam() { }

        //public Exam(ExamDTO ex)
        //{
        //    Code = ex.Code;
        //    Subject = ex.Subject;
        //    Professor = ex.Professor;
        //    Auditori = ex.Auditori;
        //    StartTime = ex.StartTime;
        //    EndTime = ex.EndTime;
        //}

        //public Exam Update(ExamDTO ex)
        //{
        //    Subject = ex.Subject;
        //    //Professor = ex.Professor;
        //    Auditori = ex.Auditori;
        //    StartTime = ex.StartTime;
        //    EndTime = ex.EndTime;
        //    return this;
        //}
    }

    //public class ExamDTO
    //{
    //    [Key][ScaffoldColumn(false)]
    //    public int Code { get; set; }
    //    public string Subject { get; set; } = string.Empty;
    //    //public string Professor { get; set; } = string.Empty;
    //    public int Auditori { get; set; }
    //    public DateTime StartTime { get; set; } = DateTime.Now;
    //    public DateTime EndTime { get; set; } = DateTime.Now;

    //    public static implicit operator ExamDTO(Exam exam)
    //    {
    //        var ex = new ExamDTO();
    //        ex.Code = exam.Code;
    //        ex.Subject = exam.Subject;
    //        //ex.Professor = exam.Professor;
    //        ex.Auditori = exam.Auditori;
    //        ex.StartTime = exam.StartTime;
    //        ex.EndTime = exam.EndTime;
    //        return ex;
    //    }

    //    //public ExamDTO() { }

    //    //public ExamDTO(Exam ex)
    //    //{
    //    //    Code = ex.Code;
    //    //    Subject = ex.Subject;
    //    //    Professor = ex.Professor;
    //    //    Auditori = ex.Auditori;
    //    //    StartTime = ex.StartTime;
    //    //    EndTime = ex.EndTime;
    //    //}
    //}
}
