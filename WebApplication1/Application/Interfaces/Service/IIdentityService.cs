using AiComp.Domain.Entities;

namespace AiComp.Application.Interfaces.Service
{
    public interface IIdentityService
    {
        public Task<bool> AuntenticateUser(string email, string password);
        public Task<string> GenerateToken(User user);
        public Task<User> GetCurrentUser();
    }
}
