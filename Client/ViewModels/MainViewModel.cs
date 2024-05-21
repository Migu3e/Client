using Client.Net;
using Client.Core;
using System.ComponentModel;
using Client.Firestore;

namespace Client.ViewModels
{
    internal class MainViewModel
    {
        public RelayCommand ConnectToServerCommand { get; set; }

        private Server _server;

        public MainViewModel() 
        {
            _server = new Server();
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(CurrentUser.GetInstance().DisplayName()));
        }
        
    }
}
