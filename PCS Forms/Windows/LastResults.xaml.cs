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
        public LastResults()
        {
            InitializeComponent();
            this.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadAllLastResults();

            
        }
        
       

        private void LoadAllLastResults()
        {
           
            
        }

       void button_Click(object sender, RoutedEventArgs e)
        {
            DataBinding data = this.DataResults.CurrentItem as DataBinding;
           

            Button button = sender as Button;
            if (button != null)
            {
                
                ResultsWindow results = new ResultsWindow();
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
