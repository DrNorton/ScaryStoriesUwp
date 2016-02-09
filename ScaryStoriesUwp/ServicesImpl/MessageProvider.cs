using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using ScaryStoriesUwp.Shared.Services;

namespace ScaryStoriesUwp.ServicesImpl
{
    public class MessageProvider : IMessageProvider
    {
        public async Task<bool> ShowCustomOkMessageBox(string message, string title)
        {

            var messageDialog = new MessageDialog(message);
            messageDialog.Title = "Информация";
            messageDialog.Commands.Clear();
            messageDialog.Commands.Add(new UICommand() { Label = "Ок", Id = 0 });
            var result= await messageDialog.ShowAsync();
            return (int)result.Id == 0;
        }

        public async Task<bool> ShowCustomOkNoMessageBox(string message, string title)
        {

            var messageDialog = new MessageDialog(message);
            messageDialog.Title = title;
            messageDialog.Commands.Clear();
            messageDialog.Commands.Add(new UICommand() { Label = "Ок", Id = 0 });
            messageDialog.Commands.Add(new UICommand() { Label = "Нет", Id = 1 });
            var result = await messageDialog.ShowAsync();
            return (int)result.Id == 0;
        }

        public void ShowPhoto(byte[] image)
        {
            throw new NotImplementedException();
        }


    }
}
