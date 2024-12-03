using Microsoft.EntityFrameworkCore;
using UserMicroService.Contracts.Requests;
using UserMicroService.Contracts.Responses;
using UserMicroService.Models;
using UserMicroService.Secutirty;

namespace UserMicroService.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        public UserDbContext _context;

        public UserRegistrationService(UserDbContext context)
        {
            _context = context;
        }

        public async Task<UserRegistrationResponse> RegisterUser(UserRegistrationRequest req)
        {
            bool usernameAvailable = await UsernameAvailable(req.Username);

            if (!usernameAvailable)
            {
                UserRegistrationResponse res = new UserRegistrationResponse(false, null, "The username is not available");

                return res;
            }
            else
            {
                var passwordHash = PasswordEncryption.HashPassword(req.Password);
                var user = new User(req.Username, passwordHash);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                UserRegistrationResponse res = new UserRegistrationResponse(true, user, null);

                return res;
            }
        }

        public async Task<bool> UsernameAvailable(string username)
        {
            var exists = await _context.Users.AnyAsync(u => u.Username == username);

            if(exists)
            {
                return false;
            }

            return true;
        }
    }
}
