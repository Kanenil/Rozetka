using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using DAL.Interfaces;
using RozetkaUI.Pages;
using System;
using System.Collections.Generic;
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
            if (!(App.Current.MainWindow as MainWindow).IsLogined)
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.modalFrame.Navigate(new LoginPage());
            }
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.modalFrame.Navigate(new RegistrationPage());
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).LoginedUser = null;
        }

        private void navBar_Loaded(object sender, RoutedEventArgs e)
        {
            ICategoryService categoryService = new CategoryService();
            var list = categoryService.GetCategories();
            foreach (var cat in list)
                foreach (var product in cat.Products)
                    product.Images = product.Images.OrderBy(x => x.Priority).ToList();

            CategoryMenuMain.ItemsSource = list;
        }

        private void catalogButton_Click(object sender, RoutedEventArgs e)
        {
            closeMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            categoriesButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void moveToCategory(object sender, RoutedEventArgs e)
        {
            var content = (CategoryEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            closeCategories.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductListPage(content));
        }

        private void CategoryMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                closeCategories.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
