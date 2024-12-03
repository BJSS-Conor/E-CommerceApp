using UserMicroService.Contracts.Requests;
using UserMicroService.Contracts.Responses;
using UserMicroService.Models;

namespace UserMicroService.Services
{
    public interface IUserRegistrationService
    {
        public Task<UserRegistrationResponse> RegisterUser(UserRegistrationRequest req);        
    }
}
