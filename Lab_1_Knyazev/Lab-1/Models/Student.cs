using System.ComponentModel.DataAnnotations;

namespace Lab_1.Models
{
    public class Student
    {
        [Key][ScaffoldColumn(false)]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        //public string GroupNumber { get; set; }
        public Group Group { get; set; }
        public List<Session> Sessions { get; set; } = new List<Session>();


        public Student() { }

        public Student(StudentDTO student, Group group)
        {
            this.ID = student.ID;
            this.Name = student.Name;
            this.Surname = student.Surname;
            this.Group = group;
        }

        public static implicit operator StudentDTO(Student student)
        {
            var stud = new StudentDTO();
            stud.ID = student.ID;
            stud.Name = student.Name;
            stud.Surname = student.Surname;
            stud.GroupNumber = student.Group.Number;
            return stud;
        }

        public Student Update(StudentDTO student)
        {
            this.ID = student.ID;
            this.Name = student.Name;
            this.Surname = student.Surname;
            return this;
        }

        //public Student SetGroup(StudentDTO student)
        //{
        //    this.ID = student.ID;
        //    this.Name = student.Name;
        //    this.Surname = student.Surname;
        //    return this;
        //}

        public void AddExam(Exam exam, int semestr = 0)
        {
            var ses = Sessions.FirstOrDefault(ses => ses.Student == this && ses.Exam == exam);
            if ((ses == null) && (exam.Date.ToUniversalTime() <= DateTime.UtcNow.AddHours(1)))
            {
                Sessions.Add(new Session(this, exam, semestr));
            }
        }

        public void RemoveExam(Exam exam)
        {
            var ses = Sessions.FirstOrDefault(ses => ses.Student == this && ses.Exam == exam);
            if ((ses != null) && (exam.Date.ToUniversalTime() >= DateTime.UtcNow.AddHours(1)))
            {
                Sessions.Remove(ses);
            }
        }

        public bool ModifyExam(Exam exam, bool mode, int semestr = 0)
        {
            bool flag = false;
            var ses = Sessions.FirstOrDefault(ses => ses.Student == this && ses.Exam == exam);
            if (mode)
            {
                if ((ses == null) && (exam.Date.ToUniversalTime() >= DateTime.UtcNow.AddHours(1)))
                {
                    Sessions.Add(new Session(this, exam, semestr));
                    flag = true;
                }
            }
            else
            {
                if ((ses != null) && (exam.Date.ToUniversalTime() >= DateTime.UtcNow.AddHours(1)))
                {
                    Sessions.Remove(ses);
                    flag = true;
                }
            }
            return flag;
        }

        public void SetMark(Exam exam, int mark)
        {
            var ses = Sessions.FirstOrDefault(ses => ses.Student == this && ses.Exam == exam);
            if ((ses != null)/* && (exam.Date.ToUniversalTime() >= DateTime.UtcNow)*/)
            {
                ses.SetMark(mark);
            }
        }

        public int GetAvrMark(int semestr = 0)
        {
            int AvrMark = 0;
            foreach (var ses in Sessions)
            {
                if (semestr == 0)
                {
                    AvrMark += ses.Mark;
                }
                else
                {
                    if (ses.Semestr == semestr)
                        AvrMark += ses.Mark;
                }
                        
            }
            return AvrMark / Sessions.Count;
        }

        public int GetFailCount(int semestr = 0)
        {
            int failCount = 0;
            //foreach (var exam in Exams)
            //{
            //    if (exam.Value < 3)
            //        failCount++;
            //}
            foreach (var ses in Sessions)
            {
                if (ses.Mark < 3)
                    failCount++;
            }
            return failCount;
        }
    }

    public class StudentDTO
    {
        [Key][ScaffoldColumn(false)]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string GroupNumber { get; set; } = string.Empty;

        //public static implicit operator Student(StudentDTO stud)
        //{
        //    var student = new Student();
        //    student.ID = stud.ID;
        //    student.Name = stud.Name;
        //    student.Surname = stud.Surname;
        //    //student.Group = stud.Group;
        //    return student;
        //}
    }
}
