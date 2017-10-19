using PCS_Forms.Core;
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
    public partial class LoginWindow : Window
    {
        Psychologist psychologist;
        public LoginWindow(int id)
        {
            InitializeComponent();
            this.Show();
            psychologist =new Psychologist(id);
            this.FullNamePsychologist.Content = psychologist.ReturnFullName();
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.psychologist.CheckPassword(this.PasswordBox.Password))
            {
                this.Hide();
                App.Current.MainWindow.Show();
            }
            else
                MessageBox.Show("Введен не правильный пароль!");
        }

        private void LoginForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(App.Current.MainWindow!=null)
                App.Current.MainWindow.Close();
        }
    }
}
