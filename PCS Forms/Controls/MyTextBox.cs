using System.Windows.Controls;

namespace PCS_Forms.Controls
{
    using System.Windows.Input;
    using System.Windows.Media;
    public class MyTextBox : TextBox
    {
        public MyTextBox()
        {
            this.Margin = new System.Windows.Thickness(5, 5, 5, 5);
            this.TextChanged += MyTextBox_TextChanged;
            this.TextAlignment = System.Windows.TextAlignment.Center;
            this.Background = Brushes.Yellow;
            this.KeyDown += textbox_KeyDown;
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

        internal void ValueChanged(string value)
        {
            this.Text = value;
        }

        private void textbox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                KeyEventArgs tabkey = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, Key.Tab);
                tabkey.RoutedEvent = Keyboard.KeyDownEvent;
                InputManager.Current.ProcessInput(tabkey);
            }
        }
    }
}
