using System.Collections.Generic;

namespace PCS_Forms.DataOut
{
    public class ReportData
    {
        public string TestName { get; private set; }
        public List<string> Data { get; private set; }
        public ReportType ReportType { get; private set; }

        public ReportData()
        {
            this.Data = new List<string>();
        }

        public ReportData( string testName)
        {
            this.Data = new List<string>();
            this.TestName = testName;
        }
        public void AddData(string data)
        {
            this.Data.Add(data);
        }

        public void SetReportType(ReportType type)
        {
            this.ReportType = type;
        }
    }
}
