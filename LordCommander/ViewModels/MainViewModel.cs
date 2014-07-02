using System.ComponentModel;
using Caliburn.Micro;

namespace LordCommander.ViewModels
{
    public class MainViewModel : Screen
    {
        public MainViewModel()
        {
            MainContent = new LoginViewModel();
            OnPropertyChanged(new PropertyChangedEventArgs("MainContent"));
        }

        public PropertyChangedBase MainContent { get; private set; }
    }
}
