namespace MarsAdvancedAutomation.Models
{
    public class LoginTestData
    {
        public ValidCredentials Valid { get; set; }
        public InvalidCredentials Invalid { get; set; }
    }

    public class ValidCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class InvalidCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
