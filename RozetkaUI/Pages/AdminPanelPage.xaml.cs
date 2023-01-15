using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for AdminPanelPage.xaml
    /// </summary>
    public partial class AdminPanelPage : Page, INotifyPropertyChanged
    {
        private IUserService _userService;
        public AdminPanelPage()
        {
            InitializeComponent();
            _userService = new UserService();
            DataContext = this;

            if ((App.Current.MainWindow as MainWindow).LoginedUser.UserRoles.FirstOrDefault(x => x.Role.Name == "SuperAdmin") != null)
            {
                adminPanel.Visibility = Visibility.Collapsed;
                superAdminPanel.Visibility = Visibility.Visible;

                userPanel.Visibility = Visibility.Collapsed;
                userSuperPanel.Visibility = Visibility.Visible;
            }
            else
            {
                adminPanel.Visibility = Visibility.Visible;
                superAdminPanel.Visibility = Visibility.Collapsed;

                userPanel.Visibility = Visibility.Visible;
                userSuperPanel.Visibility = Visibility.Collapsed;
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var allusers = _userService.GetAllUsers().ToList();

            var mainWindow = App.Current.MainWindow as MainWindow;

            Users = allusers.Where(x => x.UserRoles.First().Role.Name == "User").ToList();
            Admins = allusers.Where(x => x.UserRoles.First().Role.Name == "Admin" && x.UserRoles.Count == 1 && x.UserRoles.First().UserId != mainWindow.LoginedUser.Id).ToList();
        }

        public bool IsSuperAdmin { get; set; }

        private List<UserEntityDTO> _users;

        public List<UserEntityDTO> Users
        {
            get
            {
                if (_users != null)
                    CollectionViewSource.GetDefaultView(_users).Refresh();
                return _users ?? (_users = new List<UserEntityDTO>());
            }
            set
            {
                if (_users == value)
                    return;
                _users = value;
                OnPropertyChanged();
            }
        }

        private List<UserEntityDTO> _admins;

        public List<UserEntityDTO> Admins
        {
            get
            {
                if (_admins != null)
                    CollectionViewSource.GetDefaultView(_admins).Refresh();
                return _admins ?? (_admins = new List<UserEntityDTO>());
            }
            set
            {
                if (_admins == value)
                    return;
                _admins = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void MakeAdmin(object sender, RoutedEventArgs e)
        {
            var user = (UserEntityDTO)(sender as Button).DataContext;
            if (MessageBox.Show($"Зробити {user.FirstName} {user.SecondName} адміном?", "Увага!",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                await _userService.EditUserRole(user.UserRoles.First(), "Admin");
                Users.Remove(user);
                Admins.Add(user);
            }
        }

        private async void DeleteAdmin(object sender, RoutedEventArgs e)
        {
            var user = (UserEntityDTO)(sender as Button).DataContext;
            if (MessageBox.Show($"Позбавити {user.FirstName} {user.SecondName} адмінських прав?", "Увага!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                await _userService.EditUserRole(user.UserRoles.First(), "User");
                Admins.Remove(user);
                Users.Add(user);
            }
        }
    }
}
