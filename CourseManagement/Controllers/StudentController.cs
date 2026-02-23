using CourseManagement.Dtos.StudentDtos;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Controllers
{
    [Authorize(Roles = "Student, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("Get All Students")]
        public async Task<ActionResult<ServiceResponse<List<StudentDto>>>> GetAllStudents()
        {
            return await _studentService.GetAllStudent();
        }

        [HttpGet("Get Student By Id")]
        public async Task<ActionResult<ServiceResponse<StudentDto>>> GetStudentById(int id)
        {
            return await _studentService.GetStudentbyId(id);
        }

        //[HttpPost("Add Student")]
        //public async Task<ServiceResponse<AddStudentDto>> AddStudent(AddStudentDto addStudent)
        //{
        //    return await _studentService.AddStudent(addStudent);
        //}

        [HttpPost("Update Student")]
        public async Task<ActionResult<ServiceResponse<StudentDto>>> UpdateStudent(StudentDto student)
        {
            return await _studentService.UpdateStudent(student);
        }

        [HttpDelete("Delete By Id")]
        public async Task<ActionResult<ServiceResponse<StudentDto>>> DeleteStudent(int id)
        {
            return await _studentService.DeleteStudent(id);
        }

    }
}
