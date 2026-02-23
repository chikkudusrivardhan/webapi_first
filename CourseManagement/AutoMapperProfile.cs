using AutoMapper;
using CourseManagement.Dtos.CourseDtos;
using CourseManagement.Dtos.StudentDtos;
using CourseManagement.Models;

namespace CourseManagement
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StudentDto, Student>();
            CreateMap<Student, StudentDto>();

            CreateMap<Course, CourseDto>(); 
            CreateMap<CourseDto, Course>();

            CreateMap<Course, AddCourseDto>();
            CreateMap<AddCourseDto, Course>();

            CreateMap<Student, AddStudentDto>();
            CreateMap<AddStudentDto, Student>();

            CreateMap<RegisterDto, Users>();
        }
    }
}
