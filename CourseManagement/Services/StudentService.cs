using AutoMapper;
using CourseManagement.Database;
using CourseManagement.Dtos.StudentDtos;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;

        public StudentService(DataContext context, IHttpContextAccessor httpContext, IMapper mapper)
        {
            _context = context;
            _httpContext = httpContext;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<StudentDto>>> GetAllStudent()
        {
            var serviceResponse = new ServiceResponse<List<StudentDto>>();
            try
            {
                var students = await _context.Student.ToListAsync();
                serviceResponse.Data = students.Select(c => _mapper.Map<StudentDto>(c)).ToList();
                serviceResponse.Message = "Data Fetched";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<StudentDto>> GetStudentbyId(int id)
        {
            var serviceResponse = new ServiceResponse<StudentDto>();
            try 
            {
                var student = await _context.Student.FirstOrDefaultAsync(c => c.StudentId == id);
                if(student == null)
                {
                    serviceResponse.Success =false;
                    serviceResponse.Message = "user not found";
                    return serviceResponse;
                }

                serviceResponse.Data = _mapper.Map<StudentDto>(student);
                serviceResponse.Message = "Data Fetched";
                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        //public async Task<ServiceResponse<AddStudentDto>> AddStudent(AddStudentDto addStudent)
        //{
        //    var serviceResponse = new ServiceResponse<AddStudentDto>();
        //    try
        //    {
        //        var addDupStudent = await _context.Student.FirstOrDefaultAsync(c => c.Username == addStudent.Username);
        //        if (addDupStudent is not null)
        //        {
        //            serviceResponse.Success = false;
        //            serviceResponse.Message = "Student Already Exists";
        //        }
        //        var addedStudent = _mapper.Map<Student>(addStudent);

        //        _context.Student.Add(addedStudent);
        //        await _context.SaveChangesAsync();

        //        serviceResponse.Data = addStudent;
        //        serviceResponse.Message = "Added Successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        serviceResponse.Success = false;
        //        serviceResponse.Message = ex.Message;
        //    }
        //    return serviceResponse;
        //}
        public async Task<ServiceResponse<StudentDto>> UpdateStudent(StudentDto student)
        {
            var serviceResponse = new ServiceResponse<StudentDto>();
            try 
            { 
                var updateStudent = await _context.Student.FirstOrDefaultAsync(c => c.StudentId == student.StudentId);
                if(updateStudent == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Student not found";
                    return serviceResponse;
                }

                var updateStudentUser = await _context.Users.FirstOrDefaultAsync(c => c.UserId == updateStudent.UserId);
                if (updateStudentUser == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Student not found";
                    return serviceResponse;
                }
                updateStudentUser.UserName = student.Username;
                updateStudentUser.Age = student.Age;
                

                _mapper.Map(student, updateStudent);
                await _context.SaveChangesAsync();

                serviceResponse.Message = "Saved the Changes";
                serviceResponse.Data = student;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<StudentDto>> DeleteStudent(int id)
        {
            var serviceResponse = new ServiceResponse<StudentDto>();
            try 
            {
                var deleteStudent = await _context.Student.Include(c => c.Courses).FirstOrDefaultAsync(c => c.StudentId == id);
                if (deleteStudent == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Student not found";
                    return serviceResponse;
                }

                deleteStudent.Courses!.Clear();

                _context.Remove(deleteStudent);
                await _context.SaveChangesAsync();

                serviceResponse.Message = "operation successfull";

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
