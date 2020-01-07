using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tools.Messages.Contracts;

namespace Tools.Messages.SmsSender
{
    public class SmsSender : IMessageSender
    {
        private readonly SmsOptions _smsOptions;

        public SmsSender(IOptions<SmsOptions> smsOptions)
        {
            _smsOptions = smsOptions.Value;
        }

        public Task SendMessageAsync(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}