using CourseManagement.Dtos.StudentDtos;
using CourseManagement.Models;

namespace CourseManagement.Services.Interfaces
{
    public interface IStudentService
    {
        Task<ServiceResponse<List<StudentDto>>> GetAllStudent();
        Task<ServiceResponse<StudentDto>> GetStudentbyId(int id);
        //Task<ServiceResponse<AddStudentDto>> AddStudent(AddStudentDto addStudent);
        Task<ServiceResponse<StudentDto>> UpdateStudent(StudentDto student);
        Task<ServiceResponse<StudentDto>> DeleteStudent(int id);


    }
}
