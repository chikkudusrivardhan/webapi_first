using CourseManagement.Dtos.CourseDtos;
using CourseManagement.Dtos.StudentDtos;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Controllers
{
    [Authorize("Student, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollController : ControllerBase
    {
        private readonly IEnrollStudents _enrollService;

        public EnrollController(IEnrollStudents enrollService)
        {
            _enrollService = enrollService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<StudentDto>>> EnrollStudent(CourseEnrollDto courseEnroll)
        {
            return await _enrollService.EnrollStudents(courseEnroll);
        }

    }
}
