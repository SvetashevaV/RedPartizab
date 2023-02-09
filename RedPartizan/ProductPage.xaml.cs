using CompuMarket_App.DB;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompuMarket_App
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private readonly Users _currentUser;
        public ProductPage(Users authUser)
        {
            InitializeComponent();
            _currentUser = authUser;
            DataContext = _currentUser;
            var allTypes = AutoWorldEntitieser.GetContext().Categories.ToList();
            //allTypes.Insert(0, new Category
            //{ name = "Все типы" });
            //ComboType.ItemsSource = allTypes;
            //ComboType.SelectedIndex = 0;
            //ComboFilter.SelectedIndex = 0;

            if (authUser.id == 1)
            { RegularRowSize.MinHeight = 50; }
            else
            {  RegularRowSize.MaxHeight = 0; }

            UpdateProduct();
        }
        private void UpdateProduct()
        {
            var currentProduct = AutoWorldEntitieser.GetContext().Products.ToList();
            if (ComboType.SelectedIndex > 0)
             //   currentProduct = currentProduct.Where(p => p.Categories.Contains(ComboType.SelectedItem as Category)).ToList();
            currentProduct = currentProduct.Where(p => p.title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (CheckActive.IsChecked.Value)
                currentProduct = currentProduct.Where(p => p.isActual).ToList();
            if (ComboFilter.SelectedIndex == 0)
                currentProduct = currentProduct.OrderBy(p => p.id).ToList();
            if (ComboFilter.SelectedIndex == 1)
                currentProduct = currentProduct.OrderBy(p => p.cost).ToList();
            if (ComboFilter.SelectedIndex == 2)
                currentProduct = currentProduct.OrderByDescending(p => p.cost).ToList();
            if (ComboFilter.SelectedIndex == 3)
                currentProduct = currentProduct.OrderBy(p => p.title).ToList();

            LViewProduct.ItemsSource = currentProduct;
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProduct();
        }
        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }
        private void CheckActive_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }
        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
                AutoWorldEntitieser.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            UpdateProduct();
        }
        private void AddProdctR_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser.id == 1)
              //  Manager.MainFrame.Navigate(new ProductEditPage((sender as Grid).DataContext as Products));
           // else
                Manager.MainFrame.Navigate(new ViewProduct((sender as Button).DataContext as Products, DataContext as Users));
        }
        private void Grid123_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_currentUser.id == 1)
                Manager.MainFrame.Navigate(new ProductEditPage((sender as Grid).DataContext as Products));
          //  else
             //   Manager.MainFrame.Navigate(new ViewProduct((sender as Grid).DataContext as Products, DataContext as Users));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProductEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var productForRemoving = LViewProduct.SelectedItems.Cast<Products>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {productForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AutoWorldEntitieser.GetContext().Products.RemoveRange(productForRemoving);
                    AutoWorldEntitieser.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    LViewProduct.ItemsSource = AutoWorldEntitieser.GetContext().Products.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}

