using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using LordCommander.Client;

namespace LordCommander.ViewModels
{
    public class RegisterViewModel : Screen
    {
        public RegisterViewModel()
        {
            
        }

        private string _userName;
        private string _password;

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

        public void Register()
        {
            Console.WriteLine("So far so good");
        }
    }
}
