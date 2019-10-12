using System.ComponentModel.DataAnnotations;

namespace AuthenticationProcessor.Contracts
{
    public class RegistrationContract
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string PasswordCompare { get; set; }

        public string PhoneNumber { get; set; }
    }
}