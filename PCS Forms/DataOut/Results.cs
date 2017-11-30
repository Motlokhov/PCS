using PCS_Forms.Core;
using System.Collections.Generic;    

namespace PCS_Forms.DataOut
{
    using Database;
    using System;
    public class Results
    {
        public string Name { get; private set; }

        private List<DataResult> _dataResults;
        public Results()
        {
            this._dataResults = new List<DataResult>();
        }

        public void AddResult(DataResult dataResult)
        {
            this._dataResults.Add(dataResult);
        }

        public void SaveResults(AnalyseData analyseData)
        {
            int id =this.SaveDataTested(analyseData);
            
            this.SaveResultsTested(id);
        }

        private void SaveResultsTested(int id)
        {
            Database database = new Database();
            foreach (DataResult value in _dataResults)
                database.ExecuteScalar("INSERT INTO ResultsTested (Tested,Value,Meaning) VALUES (" + id + "," + value.Id + ",'" + value.Meaning + "')");
        }

        private int SaveDataTested(AnalyseData analyseData)
        {
            string commandstring = "INSERT INTO DataTested ";
            commandstring += "(Name,Surname,Lastname,";
            commandstring += "Psychologist,";
            commandstring += "Education,Family,Detained,Defect,Suicide,Methodology,Date) ";
            commandstring += "VALUES ('" + analyseData.Student.Name + "','";
            commandstring += analyseData.Student.Surname + "','";
            commandstring += analyseData.Student.Lastname + "',";
            commandstring += analyseData.Psychologist.Id + ",";
            commandstring += Convert.ToByte(analyseData.Student.Background.Education) + ",";
            commandstring += Convert.ToByte(analyseData.Student.Background.Family) + ",";
            commandstring += Convert.ToByte(analyseData.Student.Background.Detained) + ",";
            commandstring += Convert.ToByte(analyseData.Student.Background.Defect) + ",";
            commandstring += Convert.ToByte(analyseData.Student.Background.Suicide) + ",";
            commandstring += analyseData.Methodology.Id + ",'";
            commandstring+= analyseData.DateTesting.Date.ToShortDateString()+"')";

            Database database = new Database();
            database.ExecuteScalar(commandstring);
            database.ReadData("SELECT Id FROM DataTested ORDER BY Id DESC");
            database.Reader.Read();
            int id = Convert.ToInt32(database.Reader["Id"]);
            database.ConnectionClose();
            return id;
        }
    }
}
