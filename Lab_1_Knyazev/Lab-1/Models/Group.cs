using System.ComponentModel.DataAnnotations;

namespace Lab_1.Models
{
    public class Group
    {
        [Key][ScaffoldColumn(false)]
        public string Number { get; set; }
        public Student? Leader { get; set; }

        public Group() { }

        public Group(string number, Student leader) 
        {
            this.Number = number;
            this.Leader = leader;
        }

        public static implicit operator GroupDTO(Group gr)
        {
            var group = new GroupDTO();
            group.Number = gr.Number;
            if (gr.Leader != null)
                group.LeaderID = gr.Leader.ID;
            else
                group.LeaderID = 0;
            return group;
        }

        public Group SetLeader(Student leader)
        {
            this.Leader = leader;
            return this;
        }
    }

    public class GroupDTO
    {
        [Key][ScaffoldColumn(false)]
        public string Number { get; set; }
        public int? LeaderID { get; set; }
    }
}
