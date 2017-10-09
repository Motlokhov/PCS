using System;
using System.Collections.Generic;
namespace PCS_Forms.Core
{
    using Controls;
    using Microsoft.Office.Interop.Word;
    using DataOut;
    using Database;
    public class AnalyseData
    {
        public Person Psychologist { get; private set; }
        public Student Student { get; private set; }
        public Methodology Methodology { get; private set; }
        public DateTime DateTesting { get; private set; }
        Report Report;
        Results Results;

        public AnalyseData(Method method)
        {
            Methodology = new Methodology(method);
            //this.Psychologist = psychologis;
            //this.Student = student;
            //this.Methodology = methodology;
            //this.DateTesting = date;
            //this.SaveResults();
            //this.WriteToWord();
            //this.Report.Dispose();
        }

        public void SetStudent(Student student)
        {
            this.Student = student;
        }

        public void SetPsychologist(Person psy)
        {
            this.Psychologist = psy;
        }

        public void SetDate(DateTime date)
        {
            this.DateTesting = date.Date;
        }

        public void StartInterpretation()
        {
            this.Report = new Report(this.Methodology.StartInterpretation());
            this.Report.WriteToWord(this);
        }

        

        
    }

    
}
namespace Core { }
