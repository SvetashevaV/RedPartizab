using CompuMarket_App.DB;
using System.Linq;
using System.Windows.Controls;

namespace CompuMarket_App
{
   
    public partial class UserPage : Page
    {
        private Users _currentUser;
        public UserPage(Users authUser)
        {
            InitializeComponent();
            _currentUser = authUser;
            DataContext = _currentUser;

            var _order = AutoWorldEntitieser.GetContext().Orders.ToList();
            _order = _order.Where(p => p.Users.id == _currentUser.id).ToList();
            LViewProduct.ItemsSource = _order;
        }
    }
}
