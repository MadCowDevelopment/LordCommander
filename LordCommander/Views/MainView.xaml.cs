using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace LordCommander.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(IProgressDialog))]
    [Export(typeof(MainView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MainView : IProgressDialog
    {
        public MainView()
        {
            InitializeComponent();
        }

        public Task<ProgressDialogController> ShowProgressDialog(string title, string message, bool cancellable = false)
        {
            return this.ShowProgressAsync(title, message, cancellable);
        }
    }

    public interface IProgressDialog
    {
        Task<ProgressDialogController> ShowProgressDialog(string title, string message, bool cancellable = false);
    }
}
