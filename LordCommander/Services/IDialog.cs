using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace LordCommander.Services
{
    public interface IDialog
    {
        Task<MessageDialogResult> ShowMessage(string title, string message,
            MessageDialogStyle style = MessageDialogStyle.Affirmative);

        Task<ProgressDialogController> ShowProgressDialog(string title, string message, bool cancellable = false);
    }
}