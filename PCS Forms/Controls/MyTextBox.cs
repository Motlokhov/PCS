using System.Windows.Controls;

namespace PCS_Forms.Controls
{
    using System.Windows.Media;
    public class MyTextBox : TextBox
    {
        public MyTextBox()
        {
            this.Margin = new System.Windows.Thickness(5, 5, 5, 5);
            this.TextChanged += MyTextBox_TextChanged;
            this.TextAlignment = System.Windows.TextAlignment.Center;
            this.Background = Brushes.Yellow;
        }

        void MyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(this.Text))
                this.Background = Brushes.Yellow;
            else
                this.Background = Brushes.White;
        }

        private void value_ValueChanged(string value)
        {
            this.Text = value;   
        }
    }
}
