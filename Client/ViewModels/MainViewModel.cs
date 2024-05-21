using System.ComponentModel;

namespace Client.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private CurrentUser _currentUser;

        public CurrentUser CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        // Constructor
        public MainViewModel()
        {
            // Get the singleton instance of CurrentUser
            CurrentUser = CurrentUser.GetInstance();

            // Set username and email (replace "JohnDoe" and "john@example.com" with actual data)
            CurrentUser.SetUsername("JohnDoe");
            CurrentUser.SetEmail("john@example.com");
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
