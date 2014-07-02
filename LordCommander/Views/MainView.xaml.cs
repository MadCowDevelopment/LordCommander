using System.ComponentModel.Composition;
using MahApps.Metro.Controls;

namespace LordCommander.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(IMainView))]
    public partial class MainView : MetroWindow, IMainView
    {
        public MainView()
        {
            InitializeComponent();
        }
    }

    public interface IMainView
    {
    }
}
