using System.Threading.Tasks;
using Caliburn.Micro;
using LordCommander.Client;

namespace LordCommander.ViewModels
{
    public class LoginViewModel : PropertyChangedBase
    {
        private readonly MainViewModel _mainViewModel;

        public LoginViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public async Task Login()
        {
            var authHelper = new AuthenticationHelper(ServerConstants.Base);
            var result = authHelper.Login(Email, Password);
            var proxy = new GameProxy();
            proxy.Connect(result);
            proxy.SignIn();

            _mainViewModel.ShowMenu();
        }

        public async void Register()
        {
            var authHelper = new AuthenticationHelper(ServerConstants.Base);
            await authHelper.Register(Email, Password, Password);
            await Login();
        }
    }
}