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
    using Database;
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        
        public RegistrationWindow()
        {
            InitializeComponent();
            this.Show();
            this.Password.PasswordChanged += Password_PasswordChanged;
            this.RepeatPassowrd.PasswordChanged+= Password_PasswordChanged;
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
            if (string.IsNullOrEmpty(name.Text)
                | string.IsNullOrEmpty(Surname.Text)
                | string.IsNullOrEmpty(LastName.Text)
                | string.IsNullOrEmpty(Password.Password)
                | string.IsNullOrEmpty(RepeatPassowrd.Password))
                MessageBox.Show("Не все поля заполнены!");
            else if (Password.Password != RepeatPassowrd.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                Password.Password = string.Empty;
                RepeatPassowrd.Password = string.Empty;
            }
            else
            {
                string commandString = "INSERT INTO Psychologist (Name,Surname,Lastname,Password,IsActive) ";
                commandString+= "VALUES ('" + name.Text+"','" + Surname.Text +"','"+ LastName.Text +"','" + Password.Password + "',1)";
                Database database = new Database();
                database.ExecuteScalar(commandString);
                if (MessageBox.Show("Переходим на окно входа") == MessageBoxResult.OK)
                {
                    database.ReadData("SELECT Id FROM Psychologist WHERE IsActive = 1 ORDER BY Id DESC");
                    database.Reader.Read();
                    int id = (int)database.Reader["Id"];
                    LoginWindow login = new LoginWindow(id);
                    this.Hide();
                }
            }
                
        }

        private void RegistrationWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (App.Current.MainWindow != null)
                App.Current.MainWindow.Close();
        }

 
    }
}
