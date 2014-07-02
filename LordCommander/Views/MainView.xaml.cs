using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace LordCommander.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(IDialog))]
    [Export(typeof(MainView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MainView : IDialog
    {
        public MainView()
        {
            InitializeComponent();
        }

        public Task<ProgressDialogController> ShowProgressDialog(string title, string message, bool cancellable = false)
        {
            return this.ShowProgressAsync(title, message, cancellable);
        }

        public Task<MessageDialogResult> ShowMessage(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            return this.ShowMessageAsync(title, message, style);
        }
    }

    public interface IDialog
    {
        Task<MessageDialogResult> ShowMessage(string title, string message,
            MessageDialogStyle style = MessageDialogStyle.Affirmative);

        Task<ProgressDialogController> ShowProgressDialog(string title, string message, bool cancellable = false);
    }
}
