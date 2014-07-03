using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Caliburn.Micro;
using LordCommander.Client;
using LordCommander.Services;

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

        public event Action<LoginViewModel, LoginResult> LoggedIn;

        public async Task Login()
        {
            await LoginWithProgress();
        }

        public async void Register()
        {
            var registrationResult = await RegisterWithProgress();
            if (registrationResult.Success) await LoginWithProgress();
        }

        private async Task<ProgressDialogResult> RegisterWithProgress()
        {
            return await _progressDialog.ShowProgressDialog("Register", "Registering new user...",
                async () =>
                {
                    await _authenticationHelper.Register(Email, Password, Password);
                });
        }

        private async Task<ProgressDialogResult> LoginWithProgress()
        {
            return await _progressDialog.ShowProgressDialog("Login", "Logging in to game service...",
                async () =>
                {
                    var result = await _authenticationHelper.Login(Email, Password);
                    await _gameProxy.Connect(result);
                    await _gameProxy.SignIn();
                    RaiseLoggedIn(result);
                });
        }

        private void RaiseLoggedIn(LoginResult result)
        {
            var handler = LoggedIn;
            if (handler != null) handler(this, result);
        }
    }
}