using System.ComponentModel;
using Caliburn.Micro;

namespace LordCommander.ViewModels
{
    public class MainViewModel : Screen
    {
        public MainViewModel()
        {
            MainContent = new LoginViewModel(this);
            OnPropertyChanged(new PropertyChangedEventArgs("MainContent"));
        }

        public PropertyChangedBase MainContent { get; private set; }

        public void ShowMenu()
        {
            MainContent = new MenuViewModel();
        }
    }
}
