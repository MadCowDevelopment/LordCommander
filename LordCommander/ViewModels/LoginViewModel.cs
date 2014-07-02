using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Caliburn.Micro;
using LordCommander.Client;

namespace LordCommander.ViewModels
{
    [Export(typeof(LoginViewModel))]
    public class LoginViewModel : Screen
    {
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IGameProxy _gameProxy;

        [ImportingConstructor]
        public LoginViewModel(IAuthenticationHelper authenticationHelper, IGameProxy gameProxy)
        {
            _authenticationHelper = authenticationHelper;
            _gameProxy = gameProxy;
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public async Task Login()
        {
            var result = await _authenticationHelper.Login(Email, Password);
            _gameProxy.Connect(result);
            _gameProxy.SignIn();

            RaiseLoggedIn(result);
        }

        public event Action<LoginViewModel, LoginResult> LoggedIn;

        private void RaiseLoggedIn(LoginResult result)
        {
            var handler = LoggedIn;
            if (handler != null) handler(this, result);
        }

        public async void Register()
        {
            await _authenticationHelper.Register(Email, Password, Password);
            await Login();
        }
    }
}