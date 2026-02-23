using CourseManagement.Models;

namespace CourseManagement.Dtos.StudentDtos
{
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Password { get; set; } = string.Empty;
        public Roles Role { get; set; } 
    }
}
