using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using CompuMarket_App.DB;

namespace CompuMarket_App
{
    /// <summary>
    /// Логика взаимодействия для ProductEditPage.xaml
    /// </summary>
    public partial class ProductEditPage : Page
    {
        private readonly Products _currentProduct = new Products();
        public ProductEditPage(Products selectedProduct)
        {
            InitializeComponent();
            if (selectedProduct != null)
                _currentProduct = selectedProduct;
            DataContext = _currentProduct;
            if (_currentProduct.id == 0)
                DeleteClick.Visibility = Visibility.Hidden;
        }
        private void MainImage_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg;)|*.png;*.jpeg;*.jpg;|All files (*.*)|*.*",
                InitialDirectory = @"c:\Picture\"
            };
            if (openFileDialog.ShowDialog() == true)
                MainImage.Text = openFileDialog.FileName;
        }
        private void CostBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_currentProduct.id == 0)
                AutoWorldEntitieser.GetContext().Products.Add(_currentProduct);
            AutoWorldEntitieser.GetContext().SaveChanges();
            MessageBox.Show("Информация сохранена");
            Manager.MainFrame.GoBack();
        }
        private void DeleteClick_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите удалить {_currentProduct.title} ?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AutoWorldEntitieser.GetContext().Products.Remove(_currentProduct);
                    AutoWorldEntitieser.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    Manager.MainFrame.GoBack();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}

