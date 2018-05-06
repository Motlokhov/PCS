using System;
using System.Windows;
using System.Windows.Media;
using PCS_Forms.Controls;

namespace PCS_Forms.Core
{
    using Database;
    using System.Windows.Input;
   public class Value
    {
        public int Id { get; private set; }
        public string Meaning { get; private set; }
        public string Limitation { get; private set; }
        public TypeOfValue Type { get; private set; }
        public bool IsSumFromOtherValues { get; private set; }

        public delegate void ValueChange(string value);
        public event ValueChange ValueChanged;

        public static Value Construct(int id, string limitation, TypeOfValue type, bool isSum) 
        {
            var value = new Value(id, limitation, type, isSum);
            return value;
        }
        private Value(int id,string limitation,TypeOfValue type,bool isSum)
        {
            this.Limitation = limitation;
            this.Type = type;
            this.Id = id;
            this.IsSumFromOtherValues = isSum;
            this.Meaning = string.Empty;
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
            else
                this.Meaning = string.Empty;
        }

        private void SetAsNumerical(string value, byte numericalLimitation)
        {
            byte numericalValue;
            if (byte.TryParse(value, out numericalValue))
            {
                if (value != this.Meaning)
                {
                    if (numericalValue > numericalLimitation)
                    {
                        this.Meaning = string.Empty;
                        throw new Exception("Значение должно быть числом в пределе от 0 до " + this.Limitation);
                    }
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
                    this.MessageExeptionForString();
                }
                else if (value.Length > 1)
                    this.MessageExeptionForString();
                else
                {
                    this.Meaning = value;
                    if (this.ValueChanged != null)
                        this.ValueChanged(this.Meaning);
                }
            }
        }

        public void CreateTextBox(MyStackPanel stackPanel)
        {
            MyTextBox textbox = new MyTextBox();
            if (IsSumFromOtherValues)
            {
                this.ValueChanged += textbox.ValueChanged;
                textbox.IsEnabled = false;
            }
            else
            {
                textbox.TextChanged += textbox_TextChanged;
            }
            stackPanel.AddElement(textbox);
        }

        

        private void textbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            MyTextBox textbox = sender as MyTextBox;
            if (textbox != null)
            {
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

       

        private void MessageExeptionForString()
        {
            string messageException = "Значение должно быть только одним из символов {";
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

        public void LoadDataPastTesting(int tested_id)
        {
            Database database = new Database();
            database.ReadData("SELECT Meaning FROM ResultsTested WHERE Value = " + this.Id +"AND Tested = " + tested_id);
            database.Reader.Read();
            this.Meaning = database.Reader["Meaning"].ToString();
        }
    }
}
