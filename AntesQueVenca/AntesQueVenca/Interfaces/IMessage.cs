using System.Threading.Tasks;

namespace AntesQueVenca.Interfaces
{
    public interface IMessage
    {
        Task DisplayAlert(string title, string message, string cancel);
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
    }
}
