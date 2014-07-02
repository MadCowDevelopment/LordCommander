using System.ComponentModel.Composition;
using System.Threading.Tasks;
using LordCommander.Dialogs;
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
        private MetroProgressDialog _dialog;

        public MainView()
        {
            InitializeComponent();
        }

        public Task<ProgressDialogController> ShowProgressDialog(string title, string message)
        {
            return this.ShowProgressAsync(title, message);
        }
    }

    public interface IProgressDialog
    {
        Task<ProgressDialogController> ShowProgressDialog(string title, string message);
    }
}
