using System;

namespace DataAccess.Contracts
{
    public class User
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string PasswordHash { get; set; }
        
        public int Age { get; set; }
        
        public DateTime CreationDate { get; } = DateTime.Now;
    }
}