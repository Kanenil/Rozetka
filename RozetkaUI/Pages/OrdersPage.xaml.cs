using BAL.DTO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public OrdersPage(UserEntityDTO user)
        {
            InitializeComponent();
            User = user;
        }
        private UserEntityDTO _user;

        public UserEntityDTO User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }

        private void ToMainPageClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.pageFrame.Navigate(new Main_Page());
        }

        private void SortByNumber(object sender, RoutedEventArgs e)
        {

        }

        private void SortByPrice(object sender, RoutedEventArgs e)
        {

        }

        private void SortByCountProducts(object sender, RoutedEventArgs e)
        {

        }

        private void SortByStatus(object sender, RoutedEventArgs e)
        {

        }
    }
}
