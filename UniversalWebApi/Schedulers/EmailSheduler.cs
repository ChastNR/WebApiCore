using System.Collections.Generic;
using System.Threading.Tasks;
using UniversalWebApi.Extensions.EmailSender;
using UniversalWebApi.Models;
using SqlRepository.Interfaces;

namespace UniversalWebApi.Schedulers
{
    public class EmailScheduler : IEmailScheduler
    {
        private readonly IDataRepository _repository;
        private readonly IEmailSender _emailSender;

        public EmailScheduler(IDataRepository repository, IEmailSender emailSender)
        {
            _repository = repository;
            _emailSender = emailSender;
        }

        public async Task Start(string mailSubject, string message)
        {
            var users = await _repository.GetAllAsync<User>();

            foreach (var user in users)
            {
                await _emailSender.SendEmailAsync(user.Email, mailSubject, message);
            }
        }

        public async Task Start(IEnumerable<User> users, string mailSubject, string message)
        {
            foreach (var user in users)
            {
                await _emailSender.SendEmailAsync(user.Email, mailSubject, message);
            }
        }
    }
}