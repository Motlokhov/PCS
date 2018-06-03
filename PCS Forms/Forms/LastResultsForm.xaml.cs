using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Core.Test;

namespace PCS_Forms.Windows
{
    using Database;

    /// <summary>
    /// Interaction logic for LastResults.xaml
    /// </summary>
    public partial class LastResultsForm : Window
    {
        List<Result> results;
        public LastResultsForm()
        {
            InitializeComponent();
            this.Show();
            results = Result.LastResults();
            DataResults.ItemsSource = results;
        }
        

       void button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int number = (int)button.Tag - 1;
                var result = results[number];
                new ResultsForm(result).Show();
            }
        }
        
    }
}
