using CourseManagement.Dtos.StudentDtos;
using CourseManagement.Models;

namespace CourseManagement.Services.Interfaces
{
    public interface IAdminService
    {
        Task<ServiceResponse<int>> DeleteUser(int userId);
        Task<ServiceResponse<RegisterDto>> AddUser(Users addUser);
    }
}
