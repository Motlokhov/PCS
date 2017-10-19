using System;

namespace PCS_Forms.Core
{
    using Database;
    using PCS_Forms.Controls;
    using PCS_Forms.DataOut;
    public class Parameter
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public byte Number { get; private set; }
        public RuleInterpretationParameter Rule { get; private set; }
        public byte CountValues { get; private set; }
        public TypeOfValue Type { get; private set; }
        public Value[] Values { get; private set; }
        public string Limitation { get; private set; }
       
        public Parameter(int id)
        {
            this.Id = id;
            this.ReadMyData();
            this.AddValues();
        }

        private void AddValues()
        {
            Database database = new Database();
            database.ReadData("SELECT Id,IsSumFromOtherValues FROM Value WHERE Parameter =" + this.Id);
            for (int i = 0; i < Values.Length; i++)
            {
                database.Reader.Read();
                bool isSum = Convert.ToBoolean(database.Reader["isSumFromOtherValues"]);
                int id =(int)database.Reader["Id"];
                Values[i] = new Value(id,this.Limitation, this.Type,isSum);
            }
        }

        

        private void ReadMyData()
        {
            Database database = new Database();
            database.ReadData("SELECT Number,Name,Limitation,[Rule],CountValues,Type FROM Parameter WHERE Id = " + this.Id);
            database.Reader.Read();
            this.Name=(string)database.Reader["Name"];
            this.Rule = (RuleInterpretationParameter)Convert.ToByte(database.Reader["Rule"]);
            string limitation = (string)database.Reader["Limitation"];
            this.Limitation = limitation.ToUpper();
            this.Number = Convert.ToByte(database.Reader["Number"]);
            this.CountValues = Convert.ToByte(database.Reader["CountValues"]);
            this.Type = (TypeOfValue)database.Reader["Type"];
            this.Values = new Value[this.CountValues];
            
        }

        public string Interpretation()
        {
            switch (Rule)
            {
                case RuleInterpretationParameter.none:
                    return this.InterpretateAsNone();
                case RuleInterpretationParameter.single:
                   return this.InterpretateAsSingle();
                case RuleInterpretationParameter.group:
                   return this.InterpretateAsGroup();
            }
            return null;
        }

        private string InterpretateAsGroup()
        {
            object firstGroupValue = string.Empty;
            object secondGroupValue = string.Empty;
            byte count = Convert.ToByte(this.CountValues / 2);
            for (int i = 0; i < count; i++)
            {
                
                firstGroupValue += this.Values[i].Meaning;
                secondGroupValue += this.Values[i + count].Meaning;
            }
            if (string.IsNullOrEmpty(firstGroupValue.ToString()) || string.IsNullOrEmpty(secondGroupValue.ToString()))
                throw new Exception("Не все поля заполнены");
            ReportData repdata = new ReportData();
            Database database = new Database();
            database.ReadData("SELECT FirstValue,SecondValue,Definition FROM Interpretation WHERE Parameter = " + this.Id);
            while (database.Reader.Read())
            {
                object firstvalue =database.Reader["FirstValue"];
                object secondvalue =database.Reader["SecondValue"];
                if (this.Type == TypeOfValue.numerical)
                {
                    if (Convert.ToByte(firstGroupValue) < Convert.ToByte(firstvalue) || firstGroupValue.ToString() == firstvalue.ToString())
                        if (Convert.ToByte(secondGroupValue) < Convert.ToByte(secondvalue) || secondGroupValue.ToString() == secondvalue.ToString())
                        {
                            return (string)database.Reader["Definition"];
                        }
                }
                if (this.Type == TypeOfValue.str)
                {
                    if ((string)firstvalue ==(string)firstGroupValue)
                    {
                        if ((string)secondvalue == (string)secondGroupValue)
                        {
                            return (string)database.Reader["Definition"];
                        }
                    }
                }
            }
            return null;
        }

        private string InterpretateAsSingle()
        {
            ReportData repdata = new ReportData();
            Database database = new Database();
            database.ReadData("SELECT FirstValue,SecondValue,Definition FROM Interpretation WHERE Parameter = " + this.Id);
            while (database.Reader.Read())
            {
                string val = this.GetValues();
                object interpretation = (string)database.Reader["FirstValue"] + (string)database.Reader["SecondValue"];
                byte value;
                if (byte.TryParse(val, out value))
                {
                    if (value < Convert.ToByte(interpretation) | value == Convert.ToByte(interpretation))
                    {
                        return (string)database.Reader["Definition"];
                    }
                }
                else
                {
                    if (val == (string)interpretation)
                    {
                        return (string)database.Reader["Definition"];
                    }
                }
            }
            return null;
        }

        private string InterpretateAsNone()
        {
            string val = Values[0].Meaning;
            if (string.IsNullOrEmpty(val))
                throw new Exception("Не все поля заполнены");
            else
                return val;
        }

        private string GetValues()
        {
            string val=string.Empty;
            foreach (Value value in Values)
            {
                if (string.IsNullOrEmpty(value.Meaning))
                    throw new Exception("Не все поля заполнены");
                val += value.Meaning;
            }
            return val;
        }

        public void CreateWrapPanel(MyGroupBox groupbox)
        {
            MyWrapPanel wrappanel = new MyWrapPanel();
            wrappanel.ItemHeight = 28;
            wrappanel.ItemWidth = 65;
            wrappanel.ToolTip = this.Name + " (Лимит значения: " + this.Limitation+")";
            foreach (Value value in Values)
            {
                value.CreateTextBox(wrappanel);
            }
            groupbox.AddElement(wrappanel);

        }
    }
}
