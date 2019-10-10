using System;
using DataRepository.Interfaces.Base;

namespace DataRepository.Contracts
{
    [Serializable]
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}