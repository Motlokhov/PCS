using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PCS_Forms.Windows
{
    using Database;
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        Core Core = Core.Construct();

        public RegistrationForm()
        {
            InitializeComponent();
            this.Password.PasswordChanged += Password_PasswordChanged;
            this.RepeatPassowrd.PasswordChanged+= Password_PasswordChanged;
            DataContext = Core;
        }

        void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox password = sender as PasswordBox;
            if (password != null)
                if (string.IsNullOrEmpty(password.Password))
                    password.Background = Brushes.Yellow;
                else
                    password.Background = Brushes.White;
        }

        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(Password.Password.Trim()) && !string.IsNullOrEmpty(RepeatPassowrd.Password.Trim()))
            {
                if (Password.Password == RepeatPassowrd.Password)
                {
                    var isDone = Core.Psy.AddNewToDataBase(Password.Password);
                    if (isDone)
                    {
                        new LoginForm().ShowDialog();
                        Close();
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Пароли не совпадают", "Проверка полей данных", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполненны", "Проверка полей данных", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
           
                
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown(0);
        }

 
    }
}
