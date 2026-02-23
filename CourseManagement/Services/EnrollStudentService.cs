using AutoMapper;
using CourseManagement.Database;
using CourseManagement.Dtos.CourseDtos;
using CourseManagement.Dtos.StudentDtos;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Services
{
    public class EnrollStudentService : IEnrollStudents
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EnrollStudentService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<StudentDto>> EnrollStudents(CourseEnrollDto courseEnrollDto)
        {
            var serviceResponse = new ServiceResponse<StudentDto>();
            try 
            {
                var student = await _context.Student.Include(c => c.Courses)
                    .FirstOrDefaultAsync(c => c.StudentId == courseEnrollDto.StudentsStudentId);
                if(student is null)
                {
                    serviceResponse.Message = "Student Does not Exists";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                var course = await _context.Course.Include(c => c.Students)
                    .FirstOrDefaultAsync(c => c.CourseId == courseEnrollDto.CoursesCourseId);
                if (course is null)
                {
                    serviceResponse.Message = "Course Does not Exists";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                student.Courses!.Add(course);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<StudentDto>(student);
                serviceResponse.Message = "Enrolled Successfully";

            }
            catch (Exception ex) 
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
