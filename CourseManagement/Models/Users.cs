using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int Age { get; set; }

        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];

        public Roles Role { get; set; }
        public Student? Student { get; set; }
        public List<Course>? OwnedCoursers { get; set; }
    }
}
