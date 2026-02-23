using CourseManagement.Dtos.CourseDtos;
using CourseManagement.Dtos.StudentDtos;
using CourseManagement.Models;

namespace CourseManagement.Services.Interfaces
{ 
    public interface IEnrollStudents
    {
        Task<ServiceResponse<StudentDto>> EnrollStudents(CourseEnrollDto courseEnrollDto);
    }
}
