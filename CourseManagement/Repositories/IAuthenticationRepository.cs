using CourseManagement.Dtos;
using CourseManagement.Dtos.StudentDtos;
using CourseManagement.Models;

namespace CourseManagement.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<ServiceResponse<int>> Register(Users userDto, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
    }
}
