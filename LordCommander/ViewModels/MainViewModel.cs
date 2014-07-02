using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Caliburn.Micro;
using LordCommander.Client;

namespace LordCommander.ViewModels
{
    [Export(typeof(MainViewModel))]
    public class MainViewModel : Screen
    {
        private readonly CompositionContainer _container;

        [ImportingConstructor]
        public MainViewModel(CompositionContainer container)
        {
            _container = container;
            var loginViewModel = _container.GetExportedValue<LoginViewModel>();
            loginViewModel.LoggedIn += login_LoggedIn;
            MainContent = loginViewModel;
        }

        private void login_LoggedIn(LoginViewModel sender, LoginResult obj)
        {
            sender.LoggedIn -= login_LoggedIn;
            MainContent = _container.GetExportedValue<MenuViewModel>();
        }

        public PropertyChangedBase MainContent { get; private set; }
    }
}
