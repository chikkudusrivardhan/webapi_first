using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webapi_first.Database;
using webapi_first.Models;

namespace webapi_first.Repositories
{
    public class Authentication
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public Authentication(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var serviceResponse = new ServiceResponse<int>();
            if (await UserExists(user.Username))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User not found";
                return serviceResponse;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            serviceResponse.Data = user.Id;
            serviceResponse.Message = "LoginSuccessfull";
            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var serviceResponse = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Username.ToLower().Equals(username.ToLower()));
            if (user is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "user not found";
                return serviceResponse;
            }
            else if (verifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                serviceResponse.Data = ComputeToken(user);
                serviceResponse.Message = "Token Generated";
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Login Credentials Failed";
            }
            return serviceResponse;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(c => c.Username == username))
            {
                return false;
            }
            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool verifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string ComputeToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)

            };

            var appSettingToken = _configuration.GetSection("AppSettings:Token").Value;
            if (appSettingToken is null)
            {
                throw new Exception("No Token Found");
            }
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingToken));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(1),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        
    }


}
