using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }
        public int Age { get; set; }
        public string Username { get; set; } = string.Empty;
        public List<Course>? Courses { get; set; }
        public Users? Users { get; set; }

    }
}
