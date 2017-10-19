using System;
using System.Collections.Generic;
namespace PCS_Forms.Core
{
    using Controls;
    using DataOut;
    using Database;
    using System.Windows;

    public class AnalyseData
    {
        public Psychologist Psychologist { get; private set; }
        public Student Student { get; private set; }
        public Methodology Methodology { get; private set; }
        public DateTime DateTesting { get; private set; }
        public Report Report { get; private set; }
        public Results Results { get; private set; }
        public List<ReportData> ListReportData { get; private set; }
        public LoadDataAs LoadDataAs { get; private set; }

        public AnalyseData(LoadDataAs load_data_as)
        {
            this.LoadDataAs = load_data_as;
        }


        public void OpenDocument()
        {
            this.Report = new Report();
        }

        public void CreateMethodology(Method method)
        {
            this.Methodology = new Methodology(method);
            if (this.LoadDataAs == PCS_Forms.LoadDataAs.PastTesting)
                this.LoadDataPastTesting();
        }

       

        public void SetStudent(Student student)
        {
            this.Student = student;
        }

        public void SetPsychologist(Psychologist psy)
        {
            this.Psychologist = psy;
        }

        public void SetDate(DateTime date)
        {
            this.DateTesting = date.Date;
        }

        public void Interpretation()
        {

            try
            {
                this.ListReportData = new List<ReportData>(this.Methodology.Interpretation());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка!",MessageBoxButton.OK,MessageBoxImage.Error);
                this.ListReportData = null;
            }
            
        }

        public void InterpretationAndSave()
        {
            this.Interpretation();
            this.SaveResults();
        }

        public void SaveResults()
        {
            if (this.Methodology == null)
                return;
            this.Results =new Results();
            foreach (Test test in this.Methodology.Tests)
                foreach (Parameter parameter in test.Parameters)
                    foreach (Value value in parameter.Values)
                    {
                        if(string.IsNullOrEmpty(value.Meaning))
                            return;
                        Results.AddResult(new DataResult(value.Id, value.Meaning));
                    }
            this.Results.SaveResults(this);
        }

        private void LoadDataPastTesting()
        {
            foreach (Test test in Methodology.Tests)
                foreach (Parameter parameter in test.Parameters)
                    foreach (Value value in parameter.Values)
                        value.LoadDataPastTesting(this.Student.Id);
                        
        }
    }

    
}
namespace Core { }
