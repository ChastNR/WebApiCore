using System.Threading.Tasks;

namespace Tools.Messages
{
    public interface IMessageSender
    {
        Task SendMessageAsync(MessageContract contract);
    }
}