using webapi_first.Models;

namespace webapi_first.Repositories
{
    public interface IAuthRepository
    {
        public Task<ServiceResponse<string>> Login(string username, string password);
        public Task<ServiceResponse<int>> Register(User user, string password);
        public Task<bool> UserExists(string username);
    }
}
