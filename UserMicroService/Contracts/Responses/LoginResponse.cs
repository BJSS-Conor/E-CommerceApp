using UserMicroService.Models;

namespace UserMicroService.Contracts.Responses
{
    public class LoginResponse
    {
        public bool Success {  get; set; }
        public User User { get; set; }        
        public string Error { get; set; }

        public LoginResponse(bool success, User user, string error)
        {
            Success = success;
            User = user;
            Error = error;
        }
    }
}
