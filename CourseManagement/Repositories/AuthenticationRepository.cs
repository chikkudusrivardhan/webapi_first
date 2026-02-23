using AutoMapper;
using CourseManagement.Database;
using CourseManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CourseManagement.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticationRepository(IConfiguration configuration, DataContext context, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var serviceResponse = new ServiceResponse<string>();
            var loginUser = await _context.Users.FirstOrDefaultAsync(c => c.UserName.ToLower().Equals(username.ToLower()));
            if (loginUser == null) 
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User not found";
            }
            else if (VerifyHash(password, loginUser.PasswordHash, loginUser.PasswordSalt))
            {
                serviceResponse.Data = CreateToken(loginUser);
                serviceResponse.Message = "Login successfully";
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Wrong Credentials";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> Register(Users studentUser, string password)
        {
            var serviceResponse = new ServiceResponse<int>();
            try
            {
                if (await UserExists(studentUser.UserName))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User Already Exists";
                    return serviceResponse;
                }
                CreateHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                studentUser.PasswordHash = passwordHash;
                studentUser.PasswordSalt = passwordSalt;

                serviceResponse.Message = "Registered Successfully";


                if (studentUser.Role == Roles.Student)
                {
                    var student = new Student
                    {
                        Username = studentUser.UserName,
                        Age = studentUser.Age,
                        UserId = studentUser.UserId
                    };

                    studentUser.Student = student;
                    _context.Users.Add(studentUser);

                    _context.Student.Add(student);
                    await _context.SaveChangesAsync();
                }

                if (studentUser.Role == Roles.Admin)
                {
                    throw new Exception("Not Authorized to Create Admin Login");
                }
                serviceResponse.Data = studentUser.UserId;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        private async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(c => c.UserName.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordSalt = hmac.Key;
            }
        }

        private bool VerifyHash(String password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA256(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(Users loginUser)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, loginUser.UserId.ToString()),
                new Claim(ClaimTypes.Name, loginUser.UserName),
                new Claim(ClaimTypes.Role, loginUser.Role.ToString())
            };

            var appsettingskey = _configuration.GetSection("AppSettings:Token").Value;
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding
                .UTF8.GetBytes(appsettingskey!));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var Descriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds,
                Subject = new ClaimsIdentity(claims)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken token = handler.CreateToken(Descriptor);

            return handler.WriteToken(token);
        }
    }
}
