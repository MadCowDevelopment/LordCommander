using System;
using Caliburn.Micro;
using LordCommander.Client;

namespace LordCommander.ViewModels
{
    public class LoginViewModel : PropertyChangedBase
    {
        private string _userName;
        private string _password;

        public LoginViewModel()
        {
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value == _userName) return;
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password) return;
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        public void Login()
        {
            Console.WriteLine("So far so good");

            var authHelper = new AuthenticationHelper(ServerConstants.Base);
            var result = authHelper.Login(UserName, Password);
            var proxy = new GameProxy();
            proxy.Connect(result);

            proxy.SignIn();
        }

        public void Register()
        {
            Console.WriteLine("So far so good");

            var authHelper = new AuthenticationHelper(ServerConstants.Base);
            authHelper.Register(UserName, Password, Password);
            var proxy = new GameProxy();

        }

        //public bool CanLogin()
        //{
        //    //return !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);
        //}
    }
}