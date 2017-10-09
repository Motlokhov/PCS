using System;
using System.Windows;
using System.Collections.Generic;

namespace PCS_Forms
{
    
    using System.Windows.Controls;

    using PCS_Forms.Controls;
    using System.Windows.Media;
    using System.Windows.Documents;
    using Core;
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {

        AnalyseData Analyse;

        public MainWindow()
        {
            InitializeComponent();
            this.AddEnumDataInComboBoxes();
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
                        MessageBox.Show("Не все поля заполнены!",textbox.Name);
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
                            MessageBox.Show("Не все поля заполнены!", combobox.Name);
                            return false;
                        }
                    }
                    else
                    {
                        DatePicker datepicker = item as DatePicker;
                        if(datepicker!=null)
                            if (datepicker.Text == string.Empty)
                            {
                                MessageBox.Show("Не все поля заполнены!",datepicker.Name);
                                return false;
                            }
                    }
                }
            }
            return true;
        }

        private void ButtonStartInterpretation_Click(object sender, RoutedEventArgs e)
        {
            if (this.CheckAllInputControls(this.TestDataGrid))
                if (this.CheckAllInputControls(this.PsyDataGrid))
                    if (this.CheckAllInputControls(this.TestedDataGrid))
                    {
                        this.SetStudent();
                        this.SetPsychologist();
                        this.SetDate(); 
                    }
            this.Analyse.StartInterpretation();
        }

        private void SetDate()
        {
            Analyse.SetDate((DateTime)this.DatePickerTesting.SelectedDate);
        }

        private void SetStudent()
        {
            Student student = new Student(TextBoxName.Text,
                            TextBoxSurname.Text,
                            TextBoxLastName.Text,
                            new Background(
                                (Education)ComboboxEducaton.SelectedIndex,
                                (Composition_of_family)ComboboxCompositionOfFamily.SelectedIndex,
                                (Detained)ComboboxDetained.SelectedIndex,
                                (Defect)ComboboxDefects.SelectedIndex,
                                (Suicide_in_family)ComboboxSuicideIfFamily.SelectedIndex));
            Analyse.SetStudent(student);
        }

        private void SetPsychologist()
        {
            Person psy = new Person(this.TextBoxPsyName.Text, this.TextBoxPsySurname.Text, this.TextBoxPsyLastname.Text);
            Analyse.SetPsychologist(psy);
        }

        private void ComboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTab.HasItems)
                MainTab.Items.Clear();
            ComboBox combo = sender as ComboBox;
            try
            {
                Analyse = new AnalyseData((Method)combo.SelectedIndex + 1);
                MyTabitem tabitem = Analyse.Methodology.AddTabItem();
                this.MainTab.Items.Add(tabitem);
                MainTab.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
               // MessageBox.Show(string.Format("В эту версию программы не входит Методика {0}", EnumUtils.ValueOf((Method)combo.SelectedIndex)));
            }
        }

        private void EventExit(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите закончить работу с программой?",
                "Выход из программы.",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                this.Close();
        }

        
    }
}
