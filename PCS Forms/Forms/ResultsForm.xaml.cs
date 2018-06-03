using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using Core.Test;
using System.Windows.Forms;

namespace PCS_Forms.Windows
{
    
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    
    public partial class ResultsForm : Window
    {
        Report.Report _report;
        public ResultsForm(Result result)
        {
            InitializeComponent();
            DataContext = result;
            mainListView.ItemsSource = result.Interpretation;
            _report = new Report.Report(result);
        }

        public void WriteResults()
        {
            
        }

        private void ButtonWord_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.OverwritePrompt = true;
            saveFile.Filter = "docx файл(*.docx)|*.docx";
            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = saveFile.FileName;
                _report.SaveTo(path);
            }
            
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
