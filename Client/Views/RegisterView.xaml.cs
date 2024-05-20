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
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;

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
            notifier.Dispose();
            this.Close();

        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text == "" || txtPass.Password == "" || txtEmail.Text == "")
            {
                notifier.ShowError("Fields Are Empty");
            }
            else
            {
                var db = FirestoreHelper.database;
                if (await CheackIfAlreadyExistAsync()) // Check if email already exists
                {
                    return; // If email exists, show message and return
                }


                else
                {
                    if (!txtEmail.Text.Contains("@gmail.com"))
                    {
                        notifier.ShowError("Email Is Not Leagal");
                    }
                    else
                    {
                        // Email does not exist, proceed with registration
                        var data = GetWriteData();
                        DocumentReference docRef = db.Collection("UserData").Document(data.UserName);
                        await docRef.SetAsync(data); // Set data to Firestore asynchronously
                        var loginPage = new LoginView();
                        loginPage.Show();
                        notifier.Dispose();
                        this.Close();
                    }
                }
            }
           

            
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

            // Check if username exists
            DocumentReference docRefUser = db.Collection("UserData").Document(username);

            try
            {
                DocumentSnapshot snapshotUser = await docRefUser.GetSnapshotAsync();
                if (snapshotUser.Exists)
                {
                    notifier.ShowError("Username exists");
                    return true; // Username exists in the database
                }

                // Check if email exists
                Query emailQuery = db.Collection("UserData").WhereEqualTo("Email", email);
                QuerySnapshot emailQuerySnapshot = await emailQuery.GetSnapshotAsync();
                if (emailQuerySnapshot.Documents.Count > 0)
                {
                    notifier.ShowError("Email exists");
                    return true; // Email exists in the database
                }

                return false; // Neither username nor email exists
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking user existence: " + ex.Message);
                return false; // Error occurred while retrieving data
            }
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });




    }
}
