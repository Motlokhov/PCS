using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Controls;
namespace PCS_Forms
{
    using Controls;
    using Core;
    using Windows;
    using Database;
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {
            InitializeComponent();
            
            this.AddEnumDataInComboBoxes();
            this.MinWidth = 1300;
            this.MinHeight = 768;
            this.Hide();
            this.DatePickerTesting.DisplayDateEnd = DateTime.Today;
            this.CheckHasActiveUser();
        }

        private void CheckHasActiveUser()
        {
            Database database = new Database();
            database.ReadData("SELECT Id FROM Psychologist WHERE IsActive = 1 ORDER BY Id DESC");
            if (database.Reader.Read())
            {
                int id = (int)database.Reader["Id"];
                LoginWindow login = new LoginWindow(id);
                this.FillPsychologistTextBoxes(id);
            }
            else
            {
                RegistrationWindow registration = new RegistrationWindow();
            }
        }

        private void FillPsychologistTextBoxes(int id)
        {
            //this.SetPsychologist(id);
            //TextBoxPsyName.Text = this.Analyse.Psychologist.Name;
            //TextBoxPsySurname.Text = this.Analyse.Psychologist.Surname;
            //TextBoxPsyLastname.Text = this.Analyse.Psychologist.Lastname;
        }
        

        private void AddEnumDataInComboBoxes()
        {
            this.AddEnumCollectionInComboBox(ComboboxEducaton, typeof(Education));
            this.AddEnumCollectionInComboBox(ComboboxCompositionOfFamily, typeof(Composition_of_family));
            this.AddEnumCollectionInComboBox(ComboboxDefects, typeof(Defect));
            this.AddEnumCollectionInComboBox(ComboboxDetained, typeof(Detained));
            this.AddEnumCollectionInComboBox(ComboBoxMethod, typeof(Method));
            this.AddEnumCollectionInComboBox(ComboboxSuicideIfFamily, typeof(Suicide_in_family));
        }

        private void AddEnumCollectionInComboBox(ComboBox combobox, Type enum_type)
        {
            string[] enums = EnumUtils.CollectionValueOf(enum_type);
            foreach (string value in enums)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = value;
                combobox.Items.Add(item);
            }
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
                this.StartIterpretation(true);
                this.ShowResults();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void SaveResults()
        {
            
        }

        private void StartIterpretation(bool saveResult)
        {
            if (this.CheckAllInputControls(this.TestDataGrid))
                if (this.CheckAllInputControls(this.PsyDataGrid))
                    if (this.CheckAllInputControls(this.TestedDataGrid))
                    {
                        
                        //this.SetStudent();
                        //this.SetDate();
                        //if (saveResult)
                        //    thisAnalyse.InterpretationAndSave();
                        //else
                        //    this.Analyse.Interpretation();
                    }
        }

        private void ShowResults()
        {
            //ResultsWindow resultsWindow;
            //if (this.Analyse.ListReportData != null)
            //    if (this.CheckIsWindowOpen("ResultsWindow"))
            //    {
            //        resultsWindow = new ResultsWindow();
            //        resultsWindow.WriteResults(Analyse);
            //        this.Reload();
            //    }
            
        }

        private void SetDate()
        {
           
        }

        private void SetStudent()
        {
            //Student student = new Student(TextBoxName.Text,
            //                TextBoxSurname.Text,
            //                TextBoxLastName.Text,
            //                new Background(
            //                    (Education)ComboboxEducaton.SelectedIndex,
            //                    (Composition_of_family)ComboboxCompositionOfFamily.SelectedIndex,
            //                    (Detained)ComboboxDetained.SelectedIndex,
            //                    (Defect)ComboboxDefects.SelectedIndex,
            //                    (Suicide_in_family)ComboboxSuicideIfFamily.SelectedIndex));
            //Analyse.SetStudent(student);
        }

        private void SetPsychologist(int id)
        {
            //Psychologist psy = new Psychologist(id);
            //Analyse.SetPsychologist(psy);
        }

        private void ComboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainStackPanel.Children.Clear();
            ActiveTestStackPanel.Children.Clear();
            ComboBox combobox = sender as ComboBox;
            try
            {
                if (combobox.SelectedIndex == -1)
                    return;
                //Analyse.CreateMethodology((Method)combobox.SelectedIndex + 1);
                //Analyse.Methodology.AddTestGroupBox(MainStackPanel,ActiveTestStackPanel);
            }
            catch(Exception ex)
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
            LastResults lastresults;
            if (CheckIsWindowOpen("LastResultsWindow"))
                lastresults = new LastResults();
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
                Database database = new Database();
                database.ExecuteScalar("UPDATE Psychologist SET IsActive = 0");
                database.ConnectionClose();
                if(MessageBox.Show("Программа закрывается!","",MessageBoxButton.OK) == MessageBoxResult.OK)
                    this.Close();
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

        private void Reload()
        {
            this.SetEmptyInputContols(this.TestDataGrid);
            this.SetEmptyInputContols(this.TestedDataGrid);
            foreach (FrameworkElement item in this.MainStackPanel.Children)
            {
                MyGroupBox groupBox = item as MyGroupBox;
                if (groupBox != null)
                {
                    var stackPanel = groupBox.GetStackPanel();
                    var groupBox2 = stackPanel.Children;
                    foreach (MyStackPanel control in groupBox2)
                    {
                        foreach (TextBox textBox in control.Children)
                        {
                            if (textBox != null)
                            {
                                textBox.Text = string.Empty;
                            }
                        }
                    }
                }
            }
        }

        private void SetEmptyInputContols(Panel panel)
        {

            foreach (Control item in panel.Children)
            {
                TextBox textbox = item as TextBox;
                if (textbox != null)
                    textbox.Text = string.Empty;
                ComboBox combobox = item as ComboBox;
                if (combobox != null)
                    combobox.SelectedIndex = -1;
                DatePicker datepicker = item as DatePicker;
                if (datepicker != null)
                    datepicker.Text = string.Empty;


            }
        }

        private void ButtoClearResults_Click(object sender, RoutedEventArgs e)
        {
            this.Reload();
        }

        private void ButtonSaveResults_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.StartIterpretation(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
