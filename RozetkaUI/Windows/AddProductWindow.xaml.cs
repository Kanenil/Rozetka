﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RozetkaUI.Windows
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public AddProductWindow()
        {
            InitializeComponent();
        }

        int lastIndex = 0;

        private void LoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() != false)
            {
                foreach (var file in fileDialog.FileNames)
                    photosDockPanel.Children.Insert(photosDockPanel.Children.Count - 1, CreatePhoto(file));
            }
        }

        private Border CreatePhoto(string filePath)
        {
            /*
             XAML CODE
             <Border Style="{StaticResource PhotoCard}"
                            Drop="Image_Drop"
                            MouseMove="Image_MouseMove"
                            Name="image1">
                        <Border.Background>
                            <ImageBrush Stretch="UniformToFill" ImageSource="https://storage.ws.pho.to/s2/ba5069b25867b2305fe566efdffa8813bdee34c5_m.jpeg"/>
                        </Border.Background>
                        <Border CornerRadius="7">
                            <Border.Style>
                                <Style>
                                    <Setter Property="Border.Opacity" Value="1"/>
                                    <Setter Property="Border.Visibility" Value="Collapsed"></Setter>
                                    <Style.Triggers>
                                        <DataTrigger Binding="{Binding ElementName=image1, Path=IsMouseOver}" Value="true">
                                            <Setter Property="Border.Visibility" Value="Visible"></Setter>
                                            <Setter Property="Border.Background">
                                                <Setter.Value>
                                                    <SolidColorBrush Color="Black" Opacity="0.6"/>
                                                </Setter.Value>
                                            </Setter>
                                        </DataTrigger>
                                    </Style.Triggers>
                                </Style>
                            </Border.Style>
                            <Grid HorizontalAlignment="Stretch"
                                  VerticalAlignment="Stretch">
                                <Button Style="{StaticResource CardButton}"
                                    VerticalAlignment="Bottom" 
                                    Margin="31,0,67,10">
                                    <Path Data="{StaticResource EditImage}"
                                      Stretch="Uniform" 
                                      Margin="-1 0 0 -2"
                                      Fill="{StaticResource PrimaryBackgroundColor}" 
                                      Width="17" Height="17"
                                      HorizontalAlignment="Center">
                                        <Path.LayoutTransform>
                                            <RotateTransform CenterX="0" CenterY="0" Angle="180" />
                                        </Path.LayoutTransform>
                                    </Path>
                                </Button>
                                <Button Style="{StaticResource CardButton}"
                                    VerticalAlignment="Bottom"
                                    Margin="67,0,31,10">
                                    <Path Data="{StaticResource Delete}"
                                      Stretch="Uniform" 
                                      Fill="{StaticResource PrimaryBackgroundColor}" 
                                      Width="17" Height="17"
                                      HorizontalAlignment="Center">
                                        <Path.LayoutTransform>
                                            <RotateTransform CenterX="0" CenterY="0" Angle="180" />
                                        </Path.LayoutTransform>
                                    </Path>
                                </Button>
                            </Grid>
                        </Border>
                    </Border>
             */

            var main = new Border();
            main.Style = this.Resources["PhotoCard"] as Style;
            main.Drop += Image_Drop;
            main.MouseMove += Image_MouseMove;
            main.Name = $"image{++lastIndex}";
            var image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri(filePath));
            image.Stretch = Stretch.UniformToFill;
            main.Background = image;

            var child = new Border();
            child.CornerRadius = new CornerRadius(7);
            var childStyle = new Style();

            /*
             <Border.Style>
                 <Style>
                   <Setter Property="Border.Opacity" Value="1"/>
                   <Setter Property="Border.Visibility" Value="Collapsed"></Setter>
                   <Style.Triggers>
                        <DataTrigger Binding="{Binding ElementName=image1, Path=IsMouseOver}" Value="true">
                              <Setter Property="Border.Visibility" Value="Visible"></Setter>
                              <Setter Property="Border.Background">
                                  <Setter.Value>
                                       SolidColorBrush Color="Black" Opacity="0.6"/>
                                  </Setter.Value>
                              </Setter>
                          </DataTrigger>
                      </Style.Triggers>
                  </Style>
             </Border.Style>               
             */

            childStyle.Setters.Add(new Setter(Border.OpacityProperty, 1.0));
            childStyle.Setters.Add(new Setter(Border.VisibilityProperty, Visibility.Collapsed));
            var dataTrigger = new DataTrigger()
            {
                Binding = new Binding()
                {
                    Path = new PropertyPath("IsMouseOver"),
                    Source = main
                },
                Value = Boolean.TrueString
            };
            dataTrigger.Setters.Add(new Setter(Border.VisibilityProperty, Visibility.Visible));
            dataTrigger.Setters.Add(new Setter(Border.BackgroundProperty, new SolidColorBrush() { Color = Color.FromRgb(0, 0, 0), Opacity = 0.6 }));
            childStyle.Triggers.Add(dataTrigger);

            child.Style = childStyle;

            var grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Stretch;

            var edit = new Button()
            {
                Style = this.Resources["CardButton"] as Style,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(31, 0, 67, 10),
                Content = new Path()
                {
                    Data = this.FindResource("EditImage") as PathGeometry,
                    Stretch = Stretch.Uniform,
                    Margin = new Thickness(-1, 0, 0, -2),
                    Fill = this.FindResource("PrimaryBackgroundColor") as SolidColorBrush,
                    Width = 17,
                    Height = 17,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    LayoutTransform = new RotateTransform()
                    {
                        CenterX = 0,
                        CenterY = 0,
                        Angle = 180,
                    }
                },
                Name = $"{main.Name}Edit"
            };
            edit.Click += ChangePhoto;
            grid.Children.Add(edit);
            var delete = new Button()
            {
                Style = this.Resources["CardButton"] as Style,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(67, 0, 31, 10),
                Content = new Path()
                {
                    Data = this.FindResource("Delete") as PathGeometry,
                    Stretch = Stretch.Uniform,
                    Fill = this.FindResource("PrimaryBackgroundColor") as SolidColorBrush,
                    Width = 17,
                    Height = 17,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    LayoutTransform = new RotateTransform()
                    {
                        CenterX = 0,
                        CenterY = 0,
                        Angle = 180,
                    }
                },
                Name = $"{main.Name}Delete"
            };
            delete.Click += DeletePhoto;
            grid.Children.Add(delete);
            child.Child= grid;

            child.Child = grid;

            main.Child = child;

            return main;
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop((Border)sender, (sender as Border).Name, DragDropEffects.Move);
            }
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {

            e.Handled = true;
            string tstring = e.Data.GetData(DataFormats.StringFormat).ToString();
            var source = e.OriginalSource;

            if ((source as Border).Name != tstring)
            {
                var index1 = photosDockPanel.Children.IndexOf(sender as Border);
                int index2 = 0;

                for (int i = 0; i < photosDockPanel.Children.Count; i++)
                {
                    if ((photosDockPanel.Children[i] as Border).Name == tstring)
                    {
                        index2 = i;
                        break;
                    }
                }

                var photo1 = photosDockPanel.Children[index1];
                var photo2 = photosDockPanel.Children[index2];

                if (index1 < index2)
                {
                    photosDockPanel.Children.Remove(photosDockPanel.Children[index1]);
                    photosDockPanel.Children.Remove(photosDockPanel.Children[index2 - 1]);

                    photosDockPanel.Children.Insert(index1, photo2);
                    photosDockPanel.Children.Insert(index2, photo1);
                }
                else
                {
                    photosDockPanel.Children.Remove(photosDockPanel.Children[index2]);
                    photosDockPanel.Children.Remove(photosDockPanel.Children[index1 - 1]);

                    photosDockPanel.Children.Insert(index2, photo1);
                    photosDockPanel.Children.Insert(index1, photo2);
                }

            }
        }

        private void ChangePhoto(object sender, RoutedEventArgs e)
        {
            var mainName = (sender as Button).Name.Substring(0, (sender as Button).Name.IndexOf("Edit"));
            int index = 0;
            Border realPhoto = null;
            foreach (Border photo in photosDockPanel.Children)
            {
                if (photo.Name == mainName)
                {
                    index = photosDockPanel.Children.IndexOf(photo);
                    realPhoto = photo;
                    break;
                }
            }

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                photosDockPanel.Children.Remove(realPhoto);
                photosDockPanel.Children.Insert(index, CreatePhoto(fileDialog.FileName));
            }
        }

        private void DeletePhoto(object sender, RoutedEventArgs e)
        {
            var mainName = (sender as Button).Name.Substring(0, (sender as Button).Name.IndexOf("Delete"));
            foreach (Border photo in photosDockPanel.Children)
            {
                if (photo.Name == mainName)
                {
                    photosDockPanel.Children.Remove(photo);
                    break;
                }
            }
        }

        private void Button_Drop(object sender, DragEventArgs e)
        {
            if (e.Effects != DragDropEffects.None)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (var file in files)
                {
                    photosDockPanel.Children.Insert(photosDockPanel.Children.Count - 1, CreatePhoto(file));
                }
            }
        }
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
        private void Button_DragOver(object sender, DragEventArgs e)
        {

            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }



            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var f in files)
            {
                
                if (ImageExtensions.Contains(System.IO.Path.GetExtension(f).ToUpperInvariant()))
                {
                    e.Effects = DragDropEffects.Move;
                }
                else
                {
                    e.Effects = DragDropEffects.None;
                    e.Handled = true;
                    return;
                }
            }

            
        }
    }
}
