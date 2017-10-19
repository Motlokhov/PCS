using System;

namespace PCS_Forms.Core
{
    using Database;
    using Controls;
    using System.Collections.Generic;
    using PCS_Forms.DataOut;

   public class Methodology
    {
        public int Id { get; private set; }
        public Method Method { get; private set; }
        public byte CountTest { get; private set; }
        public Test[] Tests { get; private set; }


        public Methodology(Method method)
        {
            this.Method = method;
            this.ReadMyData();
            this.AddTests();
        }

        private void ReadMyData()
        {
            Database database = new Database();
            database.ReadData("SELECT Id,CountTest FROM Methodology WHERE Method = '" + this.Method + "'");
            database.Reader.Read();
            this.Id = (int)database.Reader["Id"];
            this.CountTest = Convert.ToByte(database.Reader["CountTest"]);
            this.Tests = new Test[this.CountTest];
            database.ConnectionClose();
        }


        private void AddTests()
        {

            Database database = new Database();
            database.ReadData("SELECT Id FROM TestTable WHERE Methodology = " + this.Id);
            int i = 0;
            while (database.Reader.Read())
            {
                Tests[i] = new Test((int)database.Reader["Id"]);
                i++;
            }
            database.ConnectionClose();
        }

        public MyTabitem AddTabItem()
        {
            MyTabitem Tabitem = new MyTabitem();
            foreach (Test test in Tests)
                test.AddGroupBox(Tabitem);
            return Tabitem;
        }

        public List<ReportData> StartInterpretation()
        {
            List<ReportData> listrepdata = new List<ReportData>();
            foreach (Test test in Tests)
            {
                listrepdata.Add(test.Interpretation());
            }
            return listrepdata;
        }
    }
}