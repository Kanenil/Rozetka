using BAL.DTO.Models;
using BAL.Services;
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
    /// Interaction logic for Main_Page.xaml
    /// </summary>
    public partial class Main_Page : Page
    {
        public Main_Page()
        {
            InitializeComponent();
            DataContext = this;
            _categoryService = new CategoryService();
            Category1 = GetCategoriesList()[0];
            Category2 = GetCategoriesList()[1];
            Category3 = GetCategoriesList()[2];
        }
        private CategoryService _categoryService;
        public CategoryEntityDTO Category1 { get; }
        public CategoryEntityDTO Category2 { get; }
        public CategoryEntityDTO Category3 { get; }


        private List<CategoryEntityDTO> GetCategoriesList()
        {
            List<CategoryEntityDTO> list = new List<CategoryEntityDTO>();
            foreach (CategoryEntityDTO item in _categoryService.GetCategories())
            {
                list.Add(item);
            }
            return list;
        }
        private void MoreNotebooks_Click(object sender, RoutedEventArgs e)
        {

            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductListPage(Category1));
        }

        private void MoreComputers_Click(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductListPage(Category2));
        }

        private void MoreMonitors_Click(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductListPage(Category3));
        }

        private ProductEntityDTO GetProduct1(string Name)
        {
            foreach (ProductEntityDTO pr in Category1.Products)
            {
                if (pr.Name == Name)
                {
                    return pr;
                }
            }
            return null;
        }
        private ProductEntityDTO GetProduct2(string Name)
        {
            foreach (ProductEntityDTO pr in Category2.Products)
            {
                if (pr.Name == Name)
                {
                    return pr;
                }
            }
            return null;
        }
        private ProductEntityDTO GetProduct3(string Name)
        {
            foreach (ProductEntityDTO pr in Category3.Products)
            {
                if (pr.Name == Name)
                {
                    return pr;
                }
            }
            return null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string product = (((sender as Button).Parent as StackPanel).Children[1] as TextBlock).Text;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductPage(GetProduct1(product), Category1));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string product = (((sender as Button).Parent as StackPanel).Children[1] as TextBlock).Text;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductPage(GetProduct2(product), Category2));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string product = (((sender as Button).Parent as StackPanel).Children[1] as TextBlock).Text;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductPage(GetProduct3(product), Category3));
        }
    }
}
