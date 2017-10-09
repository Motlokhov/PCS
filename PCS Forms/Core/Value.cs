using System;
using System.Windows;
using System.Windows.Media;
using PCS_Forms.Controls;

namespace PCS_Forms.Core
{
   public class Value
    {
        public int Id { get; private set; }
        public string Meaning { get; private set; }
        public string Limitation { get; private set; }
        public TypeOfValue Type { get; private set; }
        public delegate void ValueChange(string value);
        public event ValueChange ValueChanged;

        public Value(string limitation,TypeOfValue type,int id)
        {
            this.Limitation = limitation;
            this.Type = type;
            this.Id = id;

        }

        public void SetValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (this.Type == TypeOfValue.numerical)
                {
                    byte numericalLimitation;
                    if (byte.TryParse(this.Limitation, out numericalLimitation))
                        this.SetAsNumerical(value, numericalLimitation);
                    else
                        throw new Exception("Значение должно быть числом в пределе от 0 до " + this.Limitation);
                }
                if (this.Type == TypeOfValue.str)
                {
                    this.SetAsString(value);
                }
            }
        }

        private void SetAsNumerical(string value, byte numericalLimitation)
        {
            byte numericalValue;
            if (byte.TryParse(value, out numericalValue))
            {
                if (numericalValue != Convert.ToByte(this.Meaning))
                {
                    if (numericalValue > numericalLimitation)
                        throw new Exception("Значение должно быть числом в пределе от 0 до " + this.Limitation);
                    else
                        this.Meaning = value;
                    if (this.ValueChanged != null)
                        this.ValueChanged(this.Meaning);
                }
            }
            else
                throw new Exception("Значение должно быть числом в пределе от 0 до " + this.Limitation);
        }

        private void SetAsString(string value)
        {
            value = value.ToUpper();
            foreach (char charValue in value)
            {
                bool isEquals = false;
                foreach (char charLimit in this.Limitation)
                {
                    if (charValue == charLimit)
                        isEquals = true;
                }
                if (!isEquals)
                {
                    string messageException = "Значение должно быть одним из символов {";
                    string LimitChars = string.Empty;
                    for (byte c = 0; c < Limitation.Length; c++)
                    {
                        LimitChars += Limitation[c];
                        if (c + 1 < Limitation.Length)
                            LimitChars += ",";
                    }
                    messageException += LimitChars + "}";
                    this.Meaning = string.Empty;
                    if (this.ValueChanged != null)
                        this.ValueChanged(this.Meaning);
                    throw new Exception(messageException);
                }
                else
                {
                    this.Meaning = value;
                    if (this.ValueChanged != null)
                        this.ValueChanged(this.Meaning);
                }
            }
        }

        public void CreateTextBox(MyWrapPanel wrapPanel)
        {
            MyTextBox textbox = new MyTextBox();
            textbox.TextChanged += textbox_TextChanged;
            wrapPanel.AddElement(textbox);
        }

        void textbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            MyTextBox textbox = sender as MyTextBox;
            if (textbox != null)
                try
                {
                    this.SetValue(textbox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK);
                    textbox.Text = string.Empty;
                }
        }
    }
}
