namespace CourseManagement.Dtos.CourseDtos
{
    public class AddCourseDto
    {
        public string CourseName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
    }
}
