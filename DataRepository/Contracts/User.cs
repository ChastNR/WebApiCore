using System;
using DataRepository.Interfaces.Base;

namespace DataRepository.Contracts
{
    [Serializable]
    public class User : IUserEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public int Age { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}