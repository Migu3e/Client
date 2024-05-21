using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Firestore;
using Google.Cloud.Firestore;

namespace Client.ViewModels
{
    internal class MainViewModel
    {
        private UserData _data;

        public UserData CurrentUserData
        {
            get
            { return _data; }
            set { _data = value; }
        }

    }
}
