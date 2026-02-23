using CourseManagement.Dtos.CourseDtos;
using CourseManagement.Models;

namespace CourseManagement.Services.Interfaces
{
    public interface ICourseService
    {
        Task<ServiceResponse<List<CourseDto>>> GetAllCourses();
        Task<ServiceResponse<CourseDto>> GetCoursebyId(int id);
        Task<ServiceResponse<AddCourseDto>> AddCourse(AddCourseDto course);
        Task<ServiceResponse<CourseDto>> UpdateCourse(CourseDto course);
        Task<ServiceResponse<CourseDto>> DeleteCourse(int id);
    }
}
