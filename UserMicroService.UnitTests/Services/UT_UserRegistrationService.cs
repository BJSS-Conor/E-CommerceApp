using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroService.Contracts.Requests;
using UserMicroService.Contracts.Responses;
using UserMicroService.Models;
using UserMicroService.Secutirty;
using UserMicroService.Services;

namespace UserMicroService.UnitTests.Services
{
    public class UT_UserRegistrationService
    {
        private IUserRegistrationService _userRegistrationService;
        private UserDbContext _userDbContext;

        [SetUp]
        public void Setup()
        {            
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _userDbContext = new UserDbContext(options);

            _userDbContext.Users.AddRange(new List<User>
            {
                new User(1, "TestUser1", "passwordHash"),
                new User(2, "TestUser2", "passwordHash"),
                new User(3, "TestUser3", "passwordHash"),
            });
            _userDbContext.SaveChanges();

            _userRegistrationService = new UserRegistrationService(_userDbContext);
        }

        [Test]
        public async Task CreateUser_ValidUsername()
        {
            string username = "TestUser";
            string password = "Password";
            UserRegistrationRequest req = new UserRegistrationRequest(username, password);

            string expectedPasswordHash = PasswordEncryption.HashPassword(password);                        

            UserRegistrationResponse res = await _userRegistrationService.RegisterUser(req);

            Assert.That(res.Success, Is.True);
            Assert.That(res.Error, Is.Null);
            Assert.That(res.User.Username, Is.EqualTo(username));
            Assert.That(res.User.PasswordHash, Is.EqualTo(expectedPasswordHash));
        }

        [Test]
        public async Task CreateUser_DuplicateUsername()
        {
            string username = "TestUser1";
            string password = "Password";
            UserRegistrationRequest req = new UserRegistrationRequest(username, password);

            string expectedError = "The username is not available";

            UserRegistrationResponse res = await _userRegistrationService.RegisterUser(req);

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
