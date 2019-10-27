using System;

namespace AuthenticationProcessor.Contracts
{
    public class LoginContract
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Valid { get; set; }
        public Guid UserId { get; set; }
    }
}