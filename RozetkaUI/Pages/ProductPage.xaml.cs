using BAL.DTO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page, INotifyPropertyChanged
    {
        private Page _prevPage;
        public ProductPage(Page prevPage, ProductEntityDTO product, CategoryEntityDTO category)
        {
            InitializeComponent();
            Product = product;
            _category = category;
            SelectedImage = Product.Images.FirstOrDefault();
            DataContext = this;
            _prevPage= prevPage;
        }
        private CategoryEntityDTO _category;
        public ProductEntityDTO Product { get; }

        private ProductImageEntityDTO _selectedImage;

        public ProductImageEntityDTO SelectedImage
        {
            get { return _selectedImage; }
            set { _selectedImage = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChangeSelectedImage(object sender, RoutedEventArgs e)
        {
            var content = (ProductImageEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            SelectedImage = content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //(App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductListPage(_category));
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(_prevPage);
        }
    }
}
