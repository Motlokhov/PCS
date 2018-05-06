using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using PCS_Forms.Core;
using PCS_Forms.DataOut;

namespace PCS_Forms.Windows
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        public ResultsWindow()
        {
            InitializeComponent();
        }

        public void WriteResults()
        {
            //this.AddHeader( AnalyseData.Methodology.Method, analyse.DateTesting);
            //this.AddPsychologist(analyse.Psychologist);
            //this.AddTested(analyse.Student);
            //this.AddCalculatingResults(analyse.ListReportData);
            //this.Show();
        }

        private void AddHeader(Method method, System.DateTime dateTime)
        {
            this.ResultsList.Document.Blocks.Add(this.AddParagraph("Методика: " + method.ToString(), FontWeights.SemiBold));
            this.ResultsList.Document.Blocks.Add(this.AddParagraph("Дата тестирования: " + dateTime.ToString("d"),FontWeights.Bold));
            this.ResultsList.Document.Blocks.LastBlock.BorderThickness = new Thickness(0,0,0,10);
        }

        private void AddTested(Student student)
        {
            this.ResultsList.Document.Blocks.Add(this.AddParagraph("Тестируемый: " + student.ReturnFullName(), FontWeights.Bold));
            this.ResultsList.Document.Blocks.Add(this.AddParagraph("Образование: " + student.ValueOfEducation(), FontWeights.SemiBold));
            this.ResultsList.Document.Blocks.Add(this.AddParagraph("Особенности: " + student.ValueOfDefect(), FontWeights.SemiBold));
            this.ResultsList.Document.Blocks.Add(this.AddParagraph("Состав семьи: " + student.ValueOfFamily(), FontWeights.SemiBold));
            this.ResultsList.Document.Blocks.Add(this.AddParagraph("Приводы: " + student.ValueOfDetained(), FontWeights.SemiBold));
            this.ResultsList.Document.Blocks.Add(this.AddParagraph("Суицид: " + student.ValueOfSuicide(), FontWeights.SemiBold));
            this.ResultsList.Document.Blocks.LastBlock.BorderThickness = new Thickness(0, 0, 0, 5);
        }


        private void AddPsychologist(Psychologist psychologist)
        {
            this.ResultsList.Document.Blocks.Add(this.AddParagraph("Тестирование провел: " + psychologist.ReturnFullName(), FontWeights.Bold));
            this.ResultsList.Document.Blocks.LastBlock.BorderThickness = new Thickness(0, 0, 0, 5);
        }

        private void AddCalculatingResults(List<ReportData> listRepData)
        {
            foreach (ReportData report_data in listRepData)
            {
                if (report_data.Type == ReportType.asChart)
                {
                    AddChart(report_data);
                }
                if (report_data.Type == ReportType.asString)
                {
                    this.ResultsList.Document.Blocks.Add(this.AddParagraph(report_data.TestName, FontWeights.Bold));
                    foreach (string data in report_data.Data)
                    {
                        if (!string.IsNullOrEmpty(data))
                            this.ResultsList.Document.Blocks.Add(this.AddParagraph(data, FontWeights.Normal));
                    }
                }
            }
        }

        private void AddChart(ReportData report_data)
        {

        }

        private Paragraph AddParagraph(string text, FontWeight fontweights)
        {
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(text));
            paragraph.FontWeight = fontweights;
            return paragraph;
        }

        private void ButtonWord_Click(object sender, RoutedEventArgs e)
        {
            
           
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
