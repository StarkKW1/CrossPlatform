using Microsoft.EntityFrameworkCore.Update.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab_1.Models
{
    public class Student
    {
        [Key][ScaffoldColumn(false)]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;

        //public List<Exam>? Exams { get; set; } = null;

        //public Student() { }

        //public Student(StudentDTO stud)
        //{
        //    this.ID = stud.ID;
        //    this.Name = stud.Name;
        //    this.Surname = stud.Surname;
        //    this.Group = stud.Group;
        //}

        //public Student Update(StudentDTO stud)
        //{
        //    this.Name = stud.Name; 
        //    this.Surname = stud.Surname;
        //    this.Group = stud.Group;
        //    return this;
        //}
    }

    //public class StudentDTO
    //{
    //    [Key][ScaffoldColumn(false)]
    //    public int ID { get; set; }
    //    public string Name { get; set; } = string.Empty;
    //    public string Surname { get; set; } = string.Empty;
    //    public string Group { get; set; } = string.Empty;

    //    public StudentDTO() { }

    //    public StudentDTO(Student stud)
    //    {
    //        this.ID = stud.ID;
    //        this.Name = stud.Name;
    //        this.Surname = stud.Surname;
    //        this.Group = stud.Group;
    //    }
    //}
}
