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
        public ReportType ReportType { get; private set; }


        public Test(int id)
        {
            this.Id = id;
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
                MyWrapPanel WrapPanel = new MyWrapPanel();
                //this.GroupBox.AddElement(WrapPanel);
                Parameters[i] = new Parameter((int)database.Reader["Id"]);
                i++;
            }
            database.ConnectionClose();
        }

        private void ReadMyData()
        {
            Database database = new Database();
            database.ReadData("SELECT CountValues,Range,Name,ReportType FROM TestTable WHERE Id=" + this.Id);
            database.Reader.Read();
            this.Range =Convert.ToByte(database.Reader["Range"]);
            this.ReportType = (ReportType)database.Reader["ReportType"];
            this.CountValues = Convert.ToByte(database.Reader["CountValues"]);
            this.Name = (string)database.Reader["Name"];
            this.Parameters = new Parameter[this.CountValues];
            
            database.ConnectionClose();
        }

        public void AddGroupBox(MyTabitem tabitem)
        {
            MyGroupBox groupbox = new MyGroupBox();
            groupbox.Header = this.Name;
            foreach (Parameter parameter in Parameters)
                parameter.CreateWrapPanel(groupbox);
            tabitem.AddGroupBox(groupbox);
        }

        public ReportData Interpretation()
        {
            ReportData repdata = new ReportData();
            foreach (Parameter parameter in Parameters)
                repdata.AddData(parameter.Interpretation());
            repdata.SetReportType(this.ReportType);
            return repdata;
        }
    }
}
