using System.Threading.Tasks;

namespace EnailServices
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}