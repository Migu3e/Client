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
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
/* * */
namespace Client.Views;

public partial class LoginView : Window
{
    public LoginView()
    {
        InitializeComponent();
        FirestoreHelper.SetEnviornmentVariable(); // Call the method to set up Firestore environment
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
        string username = txtUser.Text.Trim();
        string password = txtPass.Password;
        if (username == "" || password == "")
        {
            notifier.ShowError("Error No Fields");
        }

        else
        {
            
            var db = FirestoreHelper.database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            if (data != null)
            {
                if (password == data.Password)
                {
                    var MainPage = new MainWindow();
                    MainPage.Show();
                    this.Close();
                }
                else
                {
                    notifier.ShowError("Password Dosent Much");
                }
            }
            else
            {
                notifier.ShowError("Username Dosent Exist");
            }
        }

    }


    private void btnRegister_Click(object sender, RoutedEventArgs e)
    {
        var registerPage = new RegisterView();
        registerPage.Show();
        this.Close();
    }
    private void btnReset_Click(object sender, RoutedEventArgs e)
    {
        var resetPage = new ForgotPasswordView();
        resetPage.Show();
        this.Close();
    }

    // ToastNotification Copy
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