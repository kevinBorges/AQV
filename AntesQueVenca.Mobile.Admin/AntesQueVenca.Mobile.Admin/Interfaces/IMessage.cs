using System.Threading.Tasks;

namespace AntesQueVenca.Mobile.Admin.Interfaces
{
    public interface IMessage
    {
        Task DisplayAlert(string title, string message, string cancel);
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
    }
}
