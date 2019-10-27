using System;

namespace DataRepository.Interfaces.Base
{
    public interface IUserEntity
    {
        Guid Id { get; set; }
    }

    public interface IEntity
    {
        int Id { get; set; }
    }
}