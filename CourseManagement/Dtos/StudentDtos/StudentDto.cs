using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Dtos.StudentDtos
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
