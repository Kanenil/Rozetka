﻿using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata;
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
    /// Interaction logic for ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private UserEntityDTO _loginedUser;

        public UserEntityDTO LoginedUser
        {
            get => _loginedUser;
            set
            {
                _loginedUser = value;
                OnPropertyChanged();
            }
        }
        public ProductListPage(CategoryEntityDTO category)
        {
            InitializeComponent();
            DataContext = this;
            Category= category;
            var mainWindow = (App.Current.MainWindow as MainWindow);
            mainWindow.OnLoginedUserChanged += () =>
            {
                LoginedUser = mainWindow.LoginedUser;
            };
            LoginedUser = mainWindow.LoginedUser;
        }
        public CategoryEntityDTO Category { get; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var content = (ProductEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductPage(this, content, Category));
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new AddProductPage(this, Category));
        }

        private void EditProduct(object sender, RoutedEventArgs e)
        {
            var content = (ProductEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new AddProductPage(this, Category, content));
            e.Handled = true;
        }

        private async void DeleteProduct(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            var content = (ProductEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            if (MessageBox.Show($"Видалити {content.Name}?","Увага", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                IProductService productService = new ProductService();
                await productService.DeleteProduct(content);
                Category.Products.Remove(content);
                CollectionViewSource.GetDefaultView(Category.Products).Refresh();
            }
        }
    }
}
