using CompuMarket_App.DB;
using System.Linq;
using System.Windows.Controls;

namespace CompuMarket_App
{
    /// <summary>
    /// Логика взаимодействия для ViewUser.xaml
    /// </summary>
    public partial class ViewUser : Page
    {
        public ViewUser()
        {
            InitializeComponent();
            UpdateUser();
        }
        private void UpdateUser()
        {
            var currentUser = AutoWorldEntitieser.GetContext().Users.ToList();
            currentUser = currentUser.Where(p => p.id.ToString().Contains(UserIdSearch.Text)).ToList();
            currentUser = currentUser.Where(p => p.FirstName.Contains(FirstNameSearch.Text)).ToList();
            currentUser = currentUser.Where(p => p.SecondName.Contains(LastNameSearch.Text)).ToList();
            currentUser = currentUser.Where(p => p.Email.ToString().Contains(EmailSearch.Text.ToLower())).ToList();
            LViewUser.ItemsSource = currentUser;
        }

        private void UserSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUser();
        }

        private void FirstNameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUser();
        }

        private void EmailSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUser();
        }

        private void LastNameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUser();
        }
    }
}
