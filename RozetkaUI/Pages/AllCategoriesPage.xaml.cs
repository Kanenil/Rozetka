using BAL.DTO.Models;
using BAL.Interfaces;
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
    /// Interaction logic for AllCategoriesPage.xaml
    /// </summary>
    public partial class AllCategoriesPage : Page
    {
        public AllCategoriesPage(List<CategoryEntityDTO> categories)
        {
            InitializeComponent();
            DataContext = this;
            Categories = categories;
        }
        public List<CategoryEntityDTO> Categories { get; }
        private void AddCategory(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new AddCategoryPage(this));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var content = (CategoryEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductListPage(content));
        }

        private void EditCategory(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            var content = (CategoryEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new AddCategoryPage(this, content));
        }

        private async void DeleteCategory(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            var content = (CategoryEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            if (MessageBox.Show($"Видалити {content.Name}?", "Увага", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ICategoryService categoryService = new CategoryService();
                await categoryService.DeleteCategory(content);
                Categories.Remove(content);
                CollectionViewSource.GetDefaultView(Categories).Refresh();

                (App.Current.MainWindow as MainWindow).navBar.Categories.Remove(content);
                CollectionViewSource.GetDefaultView((App.Current.MainWindow as MainWindow).navBar.Categories).Refresh();
            }
        }
    }
}
