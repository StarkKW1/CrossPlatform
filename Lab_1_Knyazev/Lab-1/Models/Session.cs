namespace Lab_1.Models
{
    public class Session
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int ExamCode { get; set; }
        public Exam Exam { get; set; }

        public int Semestr { get; set; } = 0; // Семестр обучения
        public int Mark { get; set; } = 0;   // оценка студента

        public Session() { }

        public Session(Student student, Exam exam, int semestr = 0) 
        {
            this.Student = student;
            this.StudentId = student.ID;
            this.Exam = exam;
            this.ExamCode = exam.Code;
            this.Semestr = semestr;
        }

        public void SetMark(int mark)
        {
            if (Exam.Date.ToUniversalTime() <= DateTime.UtcNow)
                this.Mark = mark;
        }
    }
}
