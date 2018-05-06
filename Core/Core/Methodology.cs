using System;

namespace PCS_Forms.Core
{
    using Database;
    using Controls;
    using System.Collections.Generic;
    using PCS_Forms.DataOut;
    using System.Windows.Controls;

   public class Methodology
    {
        public int Id { get; private set; }
        public Method Method { get; private set; }
        public byte CountTest { get; private set; }
        public Test[] Tests { get; private set; }
        public Psychologist Psychologist { get; private set; }
        public Student Student { get; private set; }
        public DateTime DateTesting { get; private set; }

        public static Methodology Contruct(Method method) 
        {
            var methodology = new Methodology(method);
            return methodology;
        }

       private Methodology(Method method)
       {
            this.Method = method;
            this.ReadMyData();
            this.AddTests();
       }

        public void AddTestGroupBox(StackPanel stackPanel,StackPanel activeTestStackPanel)
        {

            foreach (Test test in Tests)
            {
                var groupBox = test.AddGroupBox();
                CheckBox checkBox = AddActiveTestCheckBox(test);
                activeTestStackPanel.Children.Add(checkBox);
                test.IsActiveChanged += (value) =>
                {
                    if (value)
                    {
                        groupBox.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        groupBox.Visibility = System.Windows.Visibility.Hidden;
                    }

                };
                stackPanel.Children.Add(groupBox);
            }
        }

        private CheckBox AddActiveTestCheckBox(Test test)
        {
                CheckBox checkBox = new CheckBox();
                checkBox.IsChecked = test.IsActive;
                checkBox.Content = test.Name;
                checkBox.Margin = new System.Windows.Thickness(5, 5, 5, 5);
                checkBox.Click+= (s,e) => 
                { 
                    bool isActive = (bool)(s as CheckBox).IsChecked;
                    test.ActiveChange(isActive);
                };
                return checkBox;
        }

        public List<ReportData> Interpretation()
        {
            List<ReportData> listrepdata = new List<ReportData>();
            foreach (Test test in Tests)
            {
                if (test.IsActive)
                {
                    listrepdata.Add(test.Interpretation());
                }
            }
            if (listrepdata.Count == 0) 
            {
                return null;
            }
            return listrepdata;
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

    }
}