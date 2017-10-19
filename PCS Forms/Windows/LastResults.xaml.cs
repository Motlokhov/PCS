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
    using Core;
    using System.Collections;
    using PCS_Forms.Controls;
    /// <summary>
    /// Interaction logic for LastResults.xaml
    /// </summary>
    public partial class LastResults : Window
    {
        List<AnalyseData> ListAnalyse;
        public LastResults()
        {
            InitializeComponent();
            ListAnalyse = new List<AnalyseData>();
            this.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadAllLastResults();

            
        }
        
       

        private void LoadAllLastResults()
        {
            Database database = new Database();
            database.ReadData("SELECT Id,Psychologist,Methodology,Date FROM DataTested");
            int i = 1;
            while (database.Reader.Read())
            {
                AnalyseData analyse = new AnalyseData(LoadDataAs.PastTesting);
                analyse.SetStudent(new Student((int)database.Reader["Id"]));
                analyse.SetPsychologist(new Psychologist((int)database.Reader["Psychologist"]));
                analyse.SetDate(Convert.ToDateTime(database.Reader["Date"]));
                analyse.CreateMethodology((Method)Convert.ToByte(database.Reader["Methodology"]));
                ListAnalyse.Add(analyse);
                
                this.DataResults.Items.Add(new DataBinding() 
                {
                    Number = i++,
                    Id=analyse.Student.Id.ToString(),
                    Tested=analyse.Student.ReturnFullName(),
                    Method=analyse.Methodology.Method.ToString(),
                    Date=analyse.DateTesting.ToString("d"),
                });               
            }
            
        }

       void button_Click(object sender, RoutedEventArgs e)
        {
            DataBinding data = this.DataResults.CurrentItem as DataBinding;
           

            Button button = sender as Button;
            if (button != null)
            {
                ListAnalyse[data.Number-1].Interpretation();
                ResultsWindow results = new ResultsWindow();
                results.WriteResults(ListAnalyse[data.Number-1]);
                results.Show();
            }
        }

        
    }

    public class DataBinding
    {
        public int Number { get; set; }
        public string Id { get; set; }
        public string Method { get; set; }
        public string Tested { get; set; }
        public string Date { get; set; }
    }
}
