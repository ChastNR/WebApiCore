using System.Collections.Generic;
using System.Threading.Tasks;
using UniversalWebApi.Models;

namespace UniversalWebApi.Schedulers
{
    public interface IEmailScheduler
    {
        Task Start(string mailSubject, string message);

        Task Start(IEnumerable<User> users, string mailSubject, string message);
    }
}