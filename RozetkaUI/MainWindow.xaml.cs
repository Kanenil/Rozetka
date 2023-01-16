﻿using AutoMapper;
using BAL.DTO.Models;
using DAL.Data;
using RozetkaUI.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RozetkaUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DatabaseSeeder.Seed();
            DataContext = this;
            IsLogined = false;
            pageFrame.Content = new Main_Page();
        }

        private UserEntityDTO _loginedUser;

        public UserEntityDTO LoginedUser
        {
            get => _loginedUser; 
            set
            {
                if (value != null)
                {
                    this.navBar.tipProfile.Visibility = Visibility.Hidden;
                    IsLogined = true;
                }
                else
                {
                    this.navBar.tipProfile.Visibility = Visibility.Visible;
                    IsLogined = false;
                }

                CollectionViewSource.GetDefaultView(this.navBar.Categories).Refresh();
                _loginedUser = value;
                OnLoginedUserChanged?.Invoke();
                OnPropertyChanged(); 
            }
        }
        public event Action OnLoginedUserChanged;

        private bool _isLogined;

        public bool IsLogined
        {
            get { return _isLogined; }
            set { _isLogined = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
