using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Client.ViewModels
{
    
    public class LoginViewModel : ViewModelBase
    {
        
        
        //feild
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        
        //properties
        public string Username
        {
            get => _username;
            set
            {
                if (value == _username) return;
                _username = value ?? throw new ArgumentNullException(nameof(value));
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password
        {
            get => _password;
            set
            {
                if (Equals(value, _password)) return;
                _password = value ?? throw new ArgumentNullException(nameof(value));
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (value == _errorMessage) return;
                _errorMessage = value ?? throw new ArgumentNullException(nameof(value));
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                if (value == _isViewVisible) return;
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }
        //->commands
        
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        public ICommand RegisterCommand { get; }
        
        //constractor

        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p=> ExecuteRecoverPassCommand("",""));
        }

        private bool ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(_username) || _username.Length < 3
                                                     || _password == null || _password.Length < 3)
            {
                validData = false;
            }
            else
            {
                validData = true;
            }
            return validData;        }

        private void ExecuteLoginCommand(object obj)
        {
            throw new NotImplementedException();
        }
    }   
}
