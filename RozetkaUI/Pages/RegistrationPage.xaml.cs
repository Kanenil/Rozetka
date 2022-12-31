using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RozetkaUI.Pages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                CloseModal();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            CloseModal();
        }

        private void CloseModal()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.modalFrame.Navigate(null);
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            CloseModal();
        }

        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();
        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();
        private void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();
        private void ShowPasswordFunction()
        {
            showPasswordIcon.Data = this.FindResource("Eye") as PathGeometry;
            passwordUnmask.Visibility = Visibility.Visible;
            passwordHidden.Foreground = this.FindResource("SecundaryBackgroundColor") as SolidColorBrush;
            passwordUnmask.Text = passwordHidden.Password;
        }

        private void HidePasswordFunction()
        {
            showPasswordIcon.Data = this.FindResource("HiddenEye") as PathGeometry;
            passwordUnmask.Visibility = Visibility.Hidden;
            passwordHidden.Foreground = this.FindResource("SecundaryTextColor") as SolidColorBrush;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var firstName = firstNameTB.Text;
            var secondName = secondNameTB.Text;
            var phone = phoneTB.Text;
            var email = emailTB.Text;
            var password = passwordHidden.Password;

            CloseModal();
        }
    }
}
