using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Caliburn.Micro;
using LordCommander.Client;
using LordCommander.Shared;
using LordCommander.Views;

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

        private void login_LoggedIn(LoginViewModel sender, LoginResult loginResult)
        {
            sender.LoggedIn -= login_LoggedIn;
            var menuViewModel = _container.GetExportedValue<MenuViewModel>();
            menuViewModel.GameStarted += menuViewModel_GameStarted;
            MainContent = menuViewModel;
        }

        private void menuViewModel_GameStarted(MenuViewModel sender, GameDto game)
        {
            sender.GameStarted -= menuViewModel_GameStarted;
            var gameViewModel = _container.GetExportedValue<GameViewModel>();
            gameViewModel.Initialize(game);
            MainContent = gameViewModel;
        }

        public PropertyChangedBase MainContent { get; private set; }
    }
}
