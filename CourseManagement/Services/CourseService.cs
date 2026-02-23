using AutoMapper;
using CourseManagement.Database;
using CourseManagement.Dtos.CourseDtos;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CourseManagement.Services
{
    public class CourseService : ICourseService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public CourseService(DataContext context, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
        }
        public async Task<ServiceResponse<List<CourseDto>>> GetAllCourses()
        {
            var serviceResponse = new ServiceResponse<List<CourseDto>>();
            try 
            {
                var courses = await _context.Course.ToListAsync();
                serviceResponse.Data = courses.Select(c => _mapper.Map<CourseDto>(c)).ToList();
                serviceResponse.Message = "Fetched Successfully";
            }
            catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        
        public async Task<ServiceResponse<CourseDto>> GetCoursebyId(int id)
        {
            var serviceResponse = new ServiceResponse<CourseDto>();
            try
            {
                var idCourse = await _context.Course.FirstOrDefaultAsync(c => c.CourseId == id);
                if (idCourse is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Course Not Found";
                }
                serviceResponse.Data = _mapper.Map<CourseDto>(idCourse);
                serviceResponse.Message = "Fetched Successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<AddCourseDto>> AddCourse(AddCourseDto course)
        {
            var serviceResponse = new ServiceResponse<AddCourseDto>();
            try
            {
                var addDupCourse = await _context.Course.FirstOrDefaultAsync(c => c.CourseName == course.CourseName);
                if (addDupCourse is not null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Course Already Exists";
                }
                var userid = int.Parse(_httpContext.HttpContext!.User
                    .FindFirstValue(ClaimTypes.NameIdentifier)!);

                var courseUser = await _context.Users.FirstOrDefaultAsync(c => c.UserId == userid);

                if (courseUser is null)
                {
                    throw new Exception("Something Went Wrong");
                }

                var addedCourse = _mapper.Map<Course>(course);

                addedCourse.UserId = userid;
                _context.Course.Add(addedCourse);

                courseUser.OwnedCoursers!.Add(addedCourse);

                await _context.SaveChangesAsync();

                
                serviceResponse.Data = course;
                serviceResponse.Message = "Added Successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<CourseDto>> UpdateCourse(CourseDto course)
        {
            var serviceResponse = new ServiceResponse<CourseDto>();
            try
            {
                var updateCourse = await _context.Course.FirstOrDefaultAsync(c => c.CourseId == course.CourseId);
                if(updateCourse is null)
                {
                    serviceResponse.Success =false;
                    serviceResponse.Message = "Course Not Found";
                    return serviceResponse;
                }
                _mapper.Map(course, updateCourse);
                await _context.SaveChangesAsync();

                serviceResponse.Message = "Udpdated Successfully";
                serviceResponse.Data = course;
                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<CourseDto>> DeleteCourse(int id)
        {
            var serviceResponse = new ServiceResponse<CourseDto>();
            try
            {
                var deleteCourse = await _context.Course.Include(c => c.Students).FirstOrDefaultAsync(c => c.CourseId == id);
                if (deleteCourse is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Fetched Successfully";
                    return serviceResponse;
                }

                deleteCourse.Students!.Clear();

                _context.Remove(deleteCourse);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<CourseDto>(deleteCourse);
                serviceResponse.Message = "Deleted Course By Id";
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
