namespace UserMicroService.Contracts.Requests
{
    public class UserRegistrationRequest
    {
        public string Username {  get; set; }
        public string Password { get; set; }

        public UserRegistrationRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
