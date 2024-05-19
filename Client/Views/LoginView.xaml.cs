using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.Views;

public partial class LoginView : Window
{
    public LoginView()
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

    
    }

    private void btnRegister_Click(object sender, RoutedEventArgs e)
    {
        var registerPage = new RegisterView();
        registerPage.Show();
        this.Close();
    }
}