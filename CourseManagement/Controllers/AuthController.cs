using Azure;
using CourseManagement.Dtos.StudentDtos;
using CourseManagement.Models;
using CourseManagement.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationRepository _authRepo;

        public AuthController(IAuthenticationRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto Request)
        {
            var response = await _authRepo.Register(new Users { UserName = Request.Username, Age = Request.Age, Role = Request.Role}, Request.Password);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(StudentLoginDto Request)
        {
            var response = await _authRepo.Login(Request.Username, Request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


    }
}
