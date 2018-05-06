using System;

namespace PCS_Forms.Core
{
    using System.Collections.Generic;
    using Database;
    using PCS_Forms.Controls;
    using PCS_Forms.DataOut;
    public class Test
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public byte CountValues { get; private set; }
        public byte Range { get; private set; }
        public Parameter[] Parameters { get; private set; }
        public List<Value> AllValues { get; private set; }
        public ReportType ReportType { get; private set; }
        public bool MayValueToBeRepeated { get; private set; }
        public bool IsActive { get; private set; }
        
        private bool HasSumValue = false;

        public delegate void IsActiveChange (bool value);
        public event IsActiveChange IsActiveChanged;



        public Test(int id)
        {
            this.Id = id;
            this.IsActive = true;
            this.ReadMyData();
            this.AddParameters();
        }

        private void AddParameters()
        {
            Database database = new Database();
            database.ReadData("SELECT Id FROM Parameter WHERE TestTable = "+ this.Id);
            int i = 0;
            while (database.Reader.Read())
            {
                MyStackPanel stackPanel = new MyStackPanel();
                Parameters[i] = Parameter.Construct((int)database.Reader["Id"]);
                i++;
            }
            database.ConnectionClose();
            this.SetLinkToValues();
        }

        private void SetLinkToValues()
        {
            AllValues =new List<Value>();
            foreach (Parameter parameter in Parameters)
            {
                foreach (Value value in parameter.Values)
                {
                    AllValues.Add(value);
                    if (!value.IsSumFromOtherValues)
                    {
                        value.ValueChanged += Test_ValueChanged;
                        HasSumValue = false;
                    }
                    else
                        HasSumValue = true;
                }
            }
        }

      private void Test_ValueChanged(string value)
        {
            if (HasSumValue)
            {
                byte meaning = byte.MinValue;
                foreach (Value val in AllValues)
                    if (!val.IsSumFromOtherValues)
                        if (!string.IsNullOrEmpty(val.Meaning))
                            meaning += Convert.ToByte(val.Meaning);
                foreach (Value val in AllValues)
                    if (val.IsSumFromOtherValues)
                        val.SetValue(meaning.ToString());
            }
        }

        private void ReadMyData()
        {
            Database database = new Database();
            database.ReadData("SELECT CountValues,Range,Name,ReportType,MayValueToBeRepeated FROM TestTable WHERE Id=" + this.Id);
            database.Reader.Read();
            this.Range =Convert.ToByte(database.Reader["Range"]);
            this.ReportType = (ReportType)database.Reader["ReportType"];
            this.CountValues = Convert.ToByte(database.Reader["CountValues"]);
            this.Name = (string)database.Reader["Name"];
            this.MayValueToBeRepeated = Convert.ToBoolean(database.Reader["MayValueToBeRepeated"]);
            this.Parameters = new Parameter[this.CountValues];
            
            database.ConnectionClose();
        }

        public MyGroupBox AddGroupBox()
        {   
            MyGroupBox groupbox = new MyGroupBox();
            groupbox.Header = this.Name;
            foreach (Parameter parameter in Parameters)
                parameter.CreateStackPanel(groupbox);
            return groupbox;
        }

        public ReportData Interpretation()
        {
            ReportData repdata = new ReportData(this.Name);
            repdata.SetReportType(this.ReportType);

            if(!this.MayValueToBeRepeated)
            {
                for (int i = 0; i < AllValues.Count; i++)
                {
                    string value= AllValues[i].Meaning;
                    for (int j = i + 1; j < AllValues.Count; j++)
                    {
                        if (value == AllValues[j].Meaning)
                        {
                            repdata.AddData("Данные по тесту недостоверны. Данные имеют повторяющиеся значения!");
                            return repdata;
                        }
                    }
                }
            }
            //foreach (Parameter parameter in Parameters)
            //    repdata.AddData(parameter.Interpretation());
            return repdata;
        }

        public void ActiveChange(bool isActive)
        {
            IsActive = isActive;
            IsActiveChanged(isActive);
        }
    }
}
