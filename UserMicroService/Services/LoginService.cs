using UserMicroService.Contracts.Requests;
using UserMicroService.Contracts.Responses;
using UserMicroService.Secutirty;

namespace UserMicroService.Services
{
    public class LoginService : ILoginService
    {
        private UserDbContext _context;

        public LoginService(UserDbContext context)
        {
            _context = context;
        }

        public LoginResponse Login(LoginRequest req)
        {
            LoginResponse res;
            var passwordHash = PasswordEncryption.HashPassword(req.Password);

            var user = _context.Users.FirstOrDefault(u => u.Username == req.Username && u.PasswordHash == passwordHash);
            if(user == null)
            {
                res = new LoginResponse(false, null, "Username or Password is incorrect");                
            } 
            else
            {
                res = new LoginResponse(true, user, null);                
            }

            return res;
        }
    }
}
