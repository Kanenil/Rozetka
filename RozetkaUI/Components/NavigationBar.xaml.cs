using RozetkaUI.Pages;
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

namespace RozetkaUI.Components
{
    /// <summary>
    /// Interaction logic for NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        public NavigationBar()
        {
            InitializeComponent();
        }
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var size = e.NewSize;
            if (size.Width < 1280)
            {
                logoText.Visibility = Visibility.Collapsed;
            }
            else
            {
                logoText.Visibility = Visibility.Visible;
            }

            if (size.Width < 1024)
            {
                categoriesButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                categoriesButton.Visibility = Visibility.Visible;
            }
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                closeMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.modalFrame.Navigate(new LoginPage());
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.modalFrame.Navigate(new RegistrationPage());
        }
    }
}
