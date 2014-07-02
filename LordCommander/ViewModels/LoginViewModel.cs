using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Caliburn.Micro;
using LordCommander.Client;
using LordCommander.Views;

namespace LordCommander.ViewModels
{
    [Export(typeof(LoginViewModel))]
    public class LoginViewModel : Screen
    {
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IGameProxy _gameProxy;
        private readonly IProgressDialog _progressDialog;

        [ImportingConstructor]
        public LoginViewModel(IAuthenticationHelper authenticationHelper, IGameProxy gameProxy, IProgressDialog progressDialog)
        {
            _authenticationHelper = authenticationHelper;
            _gameProxy = gameProxy;
            _progressDialog = progressDialog;
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public async Task Login()
        {
            var progress = await _progressDialog.ShowProgressDialog("Login", "Logging in to game service...");
            var result = await _authenticationHelper.Login(Email, Password);
            _gameProxy.Connect(result);
            _gameProxy.SignIn();
            RaiseLoggedIn(result);
            await progress.CloseAsync();
        }

        public event Action<LoginViewModel, LoginResult> LoggedIn;

        private void RaiseLoggedIn(LoginResult result)
        {
            var handler = LoggedIn;
            if (handler != null) handler(this, result);
        }

        public async void Register()
        {
            var progress = await _progressDialog.ShowProgressDialog("Register", "Registering new user...");
            await _authenticationHelper.Register(Email, Password, Password);
            await progress.CloseAsync();
            await Login();
        }
    }
}