using Microsoft.AspNetCore.Mvc;
using webapi_first.Dtos.User;
using webapi_first.Models;
using webapi_first.Repositories;

namespace webapi_first.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRequest)
        {
            var response = await _authRepository.Register(new User { Username = userRequest.Username }, userRequest.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userRequest)
        {
            var response = await _authRepository.Login(userRequest.Username, userRequest.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }
    }
}
