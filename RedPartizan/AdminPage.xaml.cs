using CompuMarket_App.DB;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CompuMarket_App
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent(); UpdateOrder();
        }
        private void UpdateOrder()
        {
            var currentOrder = AutoWorldEntitieser.GetContext().Orders.ToList();
            currentOrder = currentOrder.Where(p => p.id.ToString().Contains(OrderSearch.Text.ToLower())).ToList();
            currentOrder = currentOrder.Where(p => p.Users.id.ToString().Contains(UserSearch.Text.ToLower())).ToList();
            currentOrder = currentOrder.Where(p => p.Number.ToString().Contains(NumberSearch.Text.ToLower())).ToList();
            LViewOrder.ItemsSource = currentOrder;
        }

        private void OrderSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOrder();
        }
        private void UserSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOrder();
        }
        private void NumberSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOrder();
        }
        private void CheckUser_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ViewUser());
        }
    }
}
