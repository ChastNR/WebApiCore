using System.Threading.Tasks;

namespace Tools.Messages
{
    public interface IMessageSender
    {
        Task SendMessageAsync(Message message);

        Task SendServiceMessageAsync(ServiceMessage message);
    }
}