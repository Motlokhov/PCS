using PCS_Forms.Core;
using System.Collections.Generic;    

namespace PCS_Forms.DataOut
{
    using Database;
    public enum WhereAreDataFrom { App, DataBase }
    class Results
    {
        public bool AreResultsSave { get; private set; }
        List<DataResult> DataResults;
        WhereAreDataFrom DataFrom;

        public Results(WhereAreDataFrom dataFrom)
        {
            this.DataFrom = dataFrom;
            if (this.DataFrom == WhereAreDataFrom.App)
                AreResultsSave = false;
            if (this.DataFrom == WhereAreDataFrom.DataBase)
                AreResultsSave = true;
        }

        public void AddResult(DataResult dataresult)
        {
            this.DataResults.Add(dataresult);
        }

        public void SaveResults(AnalyseData analyseData)
        {
            int id =this.SaveDataTested(analyseData);
            this.SaveResultsTested(id);
        }

        private void SaveResultsTested(int id)
        {
            Database database = new Database();
            foreach (DataResult data in DataResults)
                database.ExecuteScalar("INSERT INTO ResultsTested (Tested,Value,Meaning) VALUES (" + id + "," + data.Id + "," + data.Meaning + ")");
        }

        private int SaveDataTested(AnalyseData analyseData)
        {
            string commandstring = "INSERT INTO DataTested ";
            commandstring += "(TestedName,TestedSurname,TestedLastname,";
            commandstring += "PsyName,PsySurname,PsyLastname,";
            commandstring += "Education,Family,Detained,Defect,Suicide) ";
            commandstring += "VALUES (" + analyseData.Student.Name + ",";
            commandstring += analyseData.Student.Surname + ",";
            commandstring += analyseData.Student.Lastname + ",";
            commandstring += analyseData.Psychologist.Name + ",";
            commandstring += analyseData.Psychologist.Surname + ",";
            commandstring += analyseData.Psychologist.Lastname + ",";
            commandstring += (byte)analyseData.Student.Background.Education + ",";
            commandstring += (byte)analyseData.Student.Background.Family + ",";
            commandstring += (byte)analyseData.Student.Background.Detained + ",";
            commandstring += (byte)analyseData.Student.Background.Defect + ",";
            commandstring += (byte)analyseData.Student.Background.Suicide + ")";

            Database database = new Database();
            int id =(int)database.ExecuteScalar(commandstring);
            return id;
        }
    }
}
