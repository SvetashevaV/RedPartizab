using CompuMarket_App.DB;
using System;
using System.Windows;

namespace CompuMarket_App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Users _currentUser;
        public MainWindow(Users authUser)
        {
            InitializeComponent();
            _currentUser = authUser; 
            DataContext = _currentUser;
            UserName.Text = "Пользователь : " + authUser.FirstName;
            MainFrame.Navigate(new ProductPage(DataContext as Users)); 
            Manager.MainFrame = MainFrame;
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
                UserCab.Visibility = Visibility.Hidden; ExitApp.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
                UserCab.Visibility = Visibility.Visible; ExitApp.Visibility = Visibility.Visible;
            }
        }
        private void UserCab_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser.id == 1)
                MainFrame.Navigate(new AdminPage());
            else
                MainFrame.Navigate(new UserPage(DataContext as Users));
        }
        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow auth = new AuthWindow();
            auth.Show(); Hide();
        }
    }
}
