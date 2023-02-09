using CompuMarket_App.DB;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace CompuMarket_App
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }
        private void AuthBut_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (loginBox.Text.Trim().Length < 5)
            {
                errors.AppendLine("(Логин) Введите минимум 5 символов!");
                loginBox.BorderBrush = Brushes.Red;
            }
            else
                loginBox.BorderBrush = Brushes.Gray;

            if (passBox.Password.Trim().Length < 5)
            {
                errors.AppendLine("(Пароль) Введите минимум 5 символов!");
                passBox.BorderBrush = Brushes.Red;
            }
            else
                passBox.BorderBrush = Brushes.Gray;

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                var authUser = AutoWorldEntitieser.GetContext().Users.Where(p => p.login == loginBox.Text || p.pass == passBox.Password).FirstOrDefault();
                if (authUser != null)
                {
                    MessageBox.Show("Привет " + authUser.login.Trim().ToUpper() + "!");
                    MainWindow UserPageWindow = new MainWindow(authUser);
                    UserPageWindow.Show();
                    Hide();
                }

                else
                    MessageBox.Show("Вы ввели что-то некорректно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow f = new RegistrationWindow();
            f.Show(); Hide();
        }
    }
}

    