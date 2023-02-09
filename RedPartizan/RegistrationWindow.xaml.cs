using CompuMarket_App.DB;
using System;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace CompuMarket_App
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }
        private void RegBut_Click(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text.Trim(); string Email = emailBox.Text.Trim().ToLower();
            string pass = passBox.Password.Trim(); string pass1 = pass1Box.Password.Trim();
            string First = FirstBox.Text.Trim(); string Last = LastBox.Text.Trim();

            StringBuilder errors = new StringBuilder();

            if (login.Length < 5)
            {
                errors.AppendLine("(Логин) Введите минимум 5 символов!");
                loginBox.BorderBrush = Brushes.Red;
            }
            else
                loginBox.BorderBrush = Brushes.Gray;

            if (pass.Length < 5)
            {
                errors.AppendLine("(Пароль) Введите минимум 5 символов!");
                passBox.BorderBrush = Brushes.Red;
            }
            else
                passBox.BorderBrush = Brushes.Gray;

            if (pass != pass1 || pass1.Length < 5)
            {
                errors.AppendLine("(Подтверждение пароля) Пароли не соотвествуют");
                pass1Box.BorderBrush = Brushes.Red;
            }
            else
                pass1Box.BorderBrush = Brushes.Gray;

            if (First.Length < 5)
            {
                errors.AppendLine("(Имя) Введите минимум 5 символов!");
                FirstBox.BorderBrush = Brushes.Red;
            }
            else
                FirstBox.BorderBrush = Brushes.Gray;

            if (Last.Length < 5)
            {
                errors.AppendLine("(Фамилия) Введите минимум 5 символов!");
                LastBox.BorderBrush = Brushes.Red;
            }
            else
                LastBox.BorderBrush = Brushes.Gray;

            if (Email.Length < 5 || !Email.Contains("@") || !Email.Contains("."))
            {
                errors.AppendLine("(Email) Введите действующий email");
                emailBox.BorderBrush = Brushes.Red;
            }
            else
                emailBox.BorderBrush = Brushes.Gray;

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString()); return;
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString()); return;
            }
            try
            {
                MessageBox.Show("Регистрация прошла успешно!");
                Users user = new Users(login, pass, First, Last, Email);
                AutoWorldEntitieser.GetContext().Users.Add(user);
                AutoWorldEntitieser.GetContext().SaveChanges();
                AuthWindow f = new AuthWindow();
                f.Show(); Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow f = new AuthWindow();
            f.Show(); Hide();
        }
    }
}

   