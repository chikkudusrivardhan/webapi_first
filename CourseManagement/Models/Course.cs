using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [ForeignKey(nameof(Users))]
        public int UserId { get; set; } //Foreign Key
        public string CourseName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public int DurationInMinutes { get; set; }
        public List<Student>? Students { get; set; }

        public Users? Users { get; set; } //reference Property

    }
}
