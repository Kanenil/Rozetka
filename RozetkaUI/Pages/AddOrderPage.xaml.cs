using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
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
    /// Interaction logic for AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AddOrderPage(UserEntityDTO user)
        {
            InitializeComponent();
            User = user;
            OrderCount = user.Orders.Count;
            Summury = user.Baskets.Select(x=>x.Product.Price).Sum();
        }

        public int OrderCount { get; set; }
        public decimal Summury { get; set; }

        private UserEntityDTO _user;

        public UserEntityDTO User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }


        private void MoveToBasket(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.pageFrame.Navigate(new BasketPage(User));
        }

        private async void ApplyOrder(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled= false;

            IOrderService orderService = new OrderService();

            var statuses = await orderService.GetOrderStatuses();

            (sender as Button).IsEnabled= true;
        }
    }
    public class CountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value + 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
