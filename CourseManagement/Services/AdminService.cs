using CourseManagement.Database;
using CourseManagement.Dtos.StudentDtos;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using CourseManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Services
{
    public class AdminService
    {
        private readonly DataContext _context;

        public AdminService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<RegisterDto>> AddUser()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<int>> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
