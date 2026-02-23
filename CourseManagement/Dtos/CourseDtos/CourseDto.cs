using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Dtos.CourseDtos
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
    }
}
