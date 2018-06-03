using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PCS_Forms.Windows
{
    /// <summary>
    /// Interaction logic for PrimaryWindow.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private Core Core = Core.Construct();

        public LoginForm()
        {
            InitializeComponent();
            DataContext = Core;
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordBox.Password;
            if (Core.Psy.CheckPassword(password))
            {
                Close();
                return;
            }
            MessageBox.Show("Неправильный пароль!", "Проверка доступа", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
                App.Current.Shutdown(0);
        }
    }
}
