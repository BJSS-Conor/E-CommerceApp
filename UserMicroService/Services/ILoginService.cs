using UserMicroService.Contracts.Requests;
using UserMicroService.Contracts.Responses;

namespace UserMicroService.Services
{
    public interface ILoginService
    {
        public LoginResponse Login(LoginRequest req);
    }
}
