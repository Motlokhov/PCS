using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
using Core.Enums;
using Core.Person;
using System.Diagnostics;
using System.Security.Principal;

namespace PCS_Forms
{
    
    using Windows;
    using PCS_Forms.Forms;
    using System.Threading.Tasks;
    
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public Core Core { get; set; }
        
        public MainWindow()
        {
            if (!MainWindow.IsAdmin())
            {
                MainWindow.OpenAsAdmin();
            }
            Hide();
            var splash = new SplashScreenForm();
            InitializeComponent();
            Core = Core.Construct();
            PsyDataGrid.DataContext = Core;
            TestedDataGrid.DataContext = Core;
            ComboBoxesItemsSource();
            OtherGrid.DataContext = Core;
            MainListBox.ItemsSource = Core.Tests;
            MainDatePicker.DisplayDateEnd = DateTime.Today;
            CheckUser();
            
        }

        private async void CheckUser()
        {
            bool isActive = await Task.Factory.StartNew<bool>
                (
                    ()=> Core.CheckActiveUser()
                );
            if (isActive)
            {
                new LoginForm().ShowDialog();
            }
            else
            {
                Core.Psy = Psycologist.Construct();
                new RegistrationForm().ShowDialog();
            }
        }

        private static bool IsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principial = new WindowsPrincipal(id);
            return principial.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static void OpenAsAdmin()
        {
            var processInfo = new ProcessStartInfo();
            string name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            processInfo.FileName = name;
            processInfo.UseShellExecute = true;
            processInfo.Verb = "runas";
            System.Diagnostics.Process.Start(processInfo);
            App.Current.Shutdown(0);
        }

        private void ComboBoxesItemsSource()
        {
            ComboBoxMethods.ItemsSource = Enum.GetValues(typeof(Method));
            ComboboxEducaton.ItemsSource = EnumUtils.CollectionValueOf(typeof(Education));
            ComboboxCompositionOfFamily.ItemsSource = EnumUtils.CollectionValueOf(typeof(CompositionOfFamily));
            ComboboxDefects.ItemsSource = EnumUtils.CollectionValueOf(typeof(Defect));
            ComboboxDefects.ItemsSource = EnumUtils.CollectionValueOf(typeof(Defect));
            ComboboxSuicideIfFamily.ItemsSource = EnumUtils.CollectionValueOf(typeof(SuicideInFamily));
            ComboboxDetained.ItemsSource = EnumUtils.CollectionValueOf(typeof(Detained));
        }

        


        private bool CheckAllInputControls(Grid grid)
        {
            foreach (Control item in grid.Children)
            {
                TextBox textbox = item as TextBox;
                if (textbox != null)
                {
                    if (textbox.Text == string.Empty)
                    {
                        MessageBox.Show("Не все поля заполнены!", textbox.Name, MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    ComboBox combobox = item as ComboBox;
                    if (combobox != null)
                    {
                        if (combobox.SelectedIndex == -1)
                        {
                            MessageBox.Show("Не все поля заполнены!", combobox.Name, MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        DatePicker datepicker = item as DatePicker;
                        if(datepicker!=null)
                            if (datepicker.Text == string.Empty)
                            {
                                MessageBox.Show("Не все поля заполнены!",datepicker.Name,MessageBoxButton.OK,MessageBoxImage.Error);
                                return false;
                            }
                    }
                }
            }
            return true;
        }

        private void ButtonStartInterpretation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = Core.Interpretation();
                new ResultsForm(result).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


       

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null)
                if (string.IsNullOrEmpty(textbox.Text))
                    textbox.Background = Brushes.Yellow;
                else
                    textbox.Background = Brushes.White;
        }

        private void ButtonLastResults_Click(object sender, RoutedEventArgs e)
        {
            LastResultsForm lastresults;
            if (CheckIsWindowOpen("LastResultsWindow"))
                lastresults = new LastResultsForm();
        }


        private bool CheckIsWindowOpen(string window_name)
        {
            for (int i = 0; i < App.Current.Windows.Count; i++)
                if (App.Current.Windows[i].Name == window_name)
                    return false;
            return true;
        }

        private void ButtonOpenTermsDictionary_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonSetAccountAsUnactive(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы собираетесь удалить собственную учетную запись! Вы подтверждаете это действие?", "Удаление учетной записи!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Core.SetAccountAsUnactive();
                if (MessageBox.Show("Программа закрывается!", "", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    App.Current.Shutdown(0);
                }
            }
            
        }

        private void EventExit(object sender, System.ComponentModel.CancelEventArgs e)
        {
                App.Current.Shutdown(0);
        }

       

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите закончить работу с программой?",
                   "Выход из программы.",
                   MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    this.Close();
        }

        private void ButtonClearResults_Click(object sender, RoutedEventArgs e)
        {
            Core.ClearResult();
        }

        private void ButtonSaveResults_Click(object sender, RoutedEventArgs e)
        {

            var result = Core.Interpretation();
            Core.SaveResult(result);
        }


        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            if (checkbox != null)
            {
                var parent = checkbox.Parent as Grid;
                UIElementCollection children = parent.Children;
                foreach (var child in children)
                {
                    if (!child.Equals(checkbox))
                    {
                        var copy = child as Control;
                        if (copy != null)
                        {
                            copy.IsEnabled = (bool)checkbox.IsChecked;
                        }
                    }
                }
            }
        }

        private void main_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;
            }
        }

        private void ComboBoxMethods_MouseEnter(object sender, MouseEventArgs e)
        {
            MainListBox.ItemsSource = Core.Tests;
        }

        private void GroupBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var groupBox = sender as GroupBox;
            var grid = groupBox.Content as Grid;
            grid.IsEnabled = !grid.IsEnabled;
        }

        

       

      
    }
}
