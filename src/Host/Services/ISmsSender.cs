using System.Threading.Tasks;

namespace Host.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
