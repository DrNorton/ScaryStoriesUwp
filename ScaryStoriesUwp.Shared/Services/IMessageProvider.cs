using System.Threading.Tasks;

namespace ScaryStoriesUwp.Shared.Services
{
    public interface IMessageProvider
    {
        Task<bool> ShowCustomOkMessageBox(string message, string title);

        void ShowPhoto(byte[] image);
        Task<bool> ShowCustomOkNoMessageBox(string message, string title);
    }
}
