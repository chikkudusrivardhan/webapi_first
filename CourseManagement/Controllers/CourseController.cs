using CourseManagement.Dtos.CourseDtos;
using CourseManagement.Models;
using CourseManagement.Services;
using CourseManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Controllers
{
    [Authorize(Roles = "CourseOwner, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("Get All Courses")]
        public async Task<ActionResult<ServiceResponse<List<CourseDto>>>> GetAllCourses()
        {
            return await _courseService.GetAllCourses();
        }

        [HttpGet("Get Course By Id")]
        public async Task<ActionResult<ServiceResponse<CourseDto>>> GetCoursebyId(int id)
        {
            return await _courseService.GetCoursebyId(id);
        }

        [HttpPost("Add Course")]
        public async Task<ActionResult<ServiceResponse<AddCourseDto>>> AddCourse(AddCourseDto course)
        {
            return await _courseService.AddCourse(course);
        }

        [HttpPost("Update Course")]
        public async Task<ActionResult<ServiceResponse<CourseDto>>> UpdateCourse(CourseDto course)
        {
            return await _courseService.UpdateCourse(course);
        }

        [HttpDelete("Delete Course")]
        public async Task<ActionResult<ServiceResponse<CourseDto>>> DeleteCourse(int id)
        {
            return await _courseService.DeleteCourse(id);  
        }

    }
}
