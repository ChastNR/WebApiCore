using System.Threading.Tasks;
using Tools.Messages.Contracts;

namespace Tools.Messages
{
    public interface IMessageSender
    {
        Task SendMessageAsync(Message message);

        //Task SendServiceMessageAsync(ServiceMessage message);
    }
}