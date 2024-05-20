using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Data.SqlClient;
using Client.Firestore;
using Google.Cloud.Firestore;
using System;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var loginPage = new LoginView();
            loginPage.Show();
            this.Close();

        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var db = FirestoreHelper.database;
            if (await CheackIfAlreadyExistAsync()) // Check if email already exists
            {
                MessageBox.Show("EMAIL EXIST");
                return; // If email exists, show message and return
            }

            // Email does not exist, proceed with registration
            var data = GetWriteData();
            DocumentReference docRef = db.Collection("UserData").Document(data.UserName);
            await docRef.SetAsync(data); // Set data to Firestore asynchronously
            MessageBox.Show("DONE");
            var loginPage = new LoginView();
            loginPage.Show();
            this.Close();
        }




        private UserData GetWriteData()
        {

            string username = txtUser.Text.Trim();
            string password = txtPass.Password.Trim();
            string email = txtEmail.Text.Trim();

            return new UserData()
            {
                UserName = username,
                Password = password,
                Email = email
            };
        }

        private async Task<bool> CheackIfAlreadyExistAsync()
        {
            string email = txtEmail.Text.Trim();
            string username = txtUser.Text.Trim();
            var db = FirestoreHelper.database;
            DocumentReference docRefEmail = db.Collection("UserData").Document(email);
            DocumentReference docRefUser = db.Collection("UserData").Document(username);


            try
            {
                DocumentSnapshot snapshotUser = await docRefUser.GetSnapshotAsync();
                DocumentSnapshot snapshotEmail = await docRefEmail.GetSnapshotAsync();

                if (snapshotUser.Exists)
                {
                    return true; // Email exists in the database
                }
                if (snapshotEmail.Exists)
                {
                    return true; // Email & User exists in the database
                }
                else
                {
                    return false; // Email does not exist
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking email existence: " + ex.Message);
                return false; // Error occurred while retrieving data
            }
        }





    }
}
