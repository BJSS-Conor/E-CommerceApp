using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroService.Contracts.Requests;
using UserMicroService.Models;
using UserMicroService.Secutirty;
using UserMicroService.Services;

namespace UserMicroService.UnitTests.Services
{
    public class UT_LoginService
    {
        private ILoginService _loginService;
        private UserDbContext _userDbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _userDbContext = new UserDbContext(options);

            var passwordHash = PasswordEncryption.HashPassword("passwordHash");
            _userDbContext.Users.AddRange(new List<User>
            {
                new User(1, "TestUser1", passwordHash),
                new User(2, "TestUser2", passwordHash),
                new User(3, "TestUser3", passwordHash),
            });
            _userDbContext.SaveChanges();

            _loginService = new LoginService(_userDbContext);
        }

        [Test]
        public void Login_ValidCredentials()
        {
            var username = "TestUser1";
            var password = "passwordHash";
            LoginRequest req = new LoginRequest(username, password);

            string expectedPasswordHash = PasswordEncryption.HashPassword(password);

            var res = _loginService.Login(req);
            
            Assert.That(res.User.Username, Is.EqualTo(username));
            Assert.That(res.User.PasswordHash, Is.EqualTo(expectedPasswordHash));
            Assert.That(res.Error, Is.Null);
            Assert.That(res.Success, Is.True);
        }

        [Test]        
        public void Login_InvalidUsername() 
        {
            var username = "InvalidUser";
            var password = "Password";
            LoginRequest req = new LoginRequest(username, password);

            var expectedError = "Username or Password is incorrect";

            var res = _loginService.Login(req);

            Assert.That(res.Success, Is.False);
            Assert.That(res.User, Is.Null);
            Assert.That(res.Error, Is.EqualTo(expectedError));
        }

        [Test]
        public void Login_InvalidPassword()
        {
            var username = "TestUser1";
            var password = "InvalidPassword";
            LoginRequest req = new LoginRequest(username, password);

            var expectedError = "Username or Password is incorrect";

            var res = _loginService.Login(req);

            Assert.That(res.Success, Is.False);
            Assert.That(res.User, Is.Null);
            Assert.That(res.Error, Is.EqualTo(expectedError));
        }

        [TearDown]
        public void TearDown()
        {
            _userDbContext.Dispose();
        }
    }
}
