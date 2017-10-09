using System;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop;
using System.Collections.Generic;
using PCS_Forms.Core;
namespace PCS_Forms.DataOut
{
    class Report:IDisposable
    {
        Application WordApp;
        Document WordDoc;
        Range Range;
        List<ReportData> ListReportData;

        public Report(List<ReportData> listreportdata)
        {
            this.WordApp =new Application();
            this.WordDoc = this.WordApp.Documents.Add(Type.Missing);
            this.ListReportData = new List<ReportData>(listreportdata);
        }

        public void Dispose()
        {
            this.WordApp = null;
            this.WordDoc = null;
            this.Range = null;
        }

        private void AddParagraph(Range range,WdParagraphAlignment alignment)
        {
            this.WordDoc.Paragraphs.Add(range);
            this.WordDoc.Paragraphs.Last.Alignment = alignment;
            
        }

        public void AddRange(string text, int bold = 0, WdParagraphAlignment alignment = WdParagraphAlignment.wdAlignParagraphCenter)
        {
            if (Range == null)
                Range = WordDoc.Range(0, 0);
            else
                this.Range.Start = Range.End;   
            this.Range.Text = text;
            this.Range.Bold = bold;
            this.AddParagraph(Range,alignment);
        }

        public void WordAddVisible(bool visible)
        {
            this.WordApp.Visible = visible;
        }

        public void AddDiagram(ReportData repData)
        {
            Microsoft.Office.Interop.Word.Chart wdChart = this.WordDoc.Shapes.AddChart2(227, Microsoft.Office.Core.XlChartType.xlLine).Chart;
            Microsoft.Office.Interop.Word.ChartData chartData = wdChart.ChartData;
            WrapFormat wrapformat = (WrapFormat)this.WordDoc.Shapes[1].WrapFormat;
            this.WordDoc.Shapes[1].Top = 10;
            this.WordDoc.Shapes[1].TopRelative = 600;

            wrapformat.Type = WdWrapType.wdWrapTopBottom;
            Microsoft.Office.Interop.Excel.Workbook dataWorkBook = (Microsoft.Office.Interop.Excel.Workbook)chartData.Workbook;
            Microsoft.Office.Interop.Excel.Worksheet dataSheet = (Microsoft.Office.Interop.Excel.Worksheet)dataWorkBook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range tRange = (Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("A1", "B" + repData.Data.Count + 1);
            Microsoft.Office.Interop.Excel.ListObject tbl1 = dataSheet.ListObjects[1];
            tbl1.Resize(tRange);         
            for (byte i = 0; i < repData.Data.Count; i++)
            {
                ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("A" + (i+2))).FormulaR1C1 = "Квадрат № " + (i + 1);
                ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("B" + (i+2))).FormulaR1C1 = repData.Data[i];
            }
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("B1")).FormulaR1C1= "Время";
        }

        private string GetCharExcel(byte number)
        {
            switch (number)
            {
                case 1: return "A";
                case 2: return "B";
                case 3: return "C";
                case 4: return "D";
                case 5: return "E";
                default: return "A";

            }
        }



        public void WriteToWord(AnalyseData analyse)
        {
            this.AddFullInformation
                (
                analyse.Methodology.Method.ToString(),
                    analyse.DateTesting,
                    analyse.Student.ReturnFullName(),
                    new string[] 
                        {
                           "Образование: "+EnumUtils.ValueOf(analyse.Student.Background.Education),
                           "Состав семьи: "+ EnumUtils.ValueOf(analyse.Student.Background.Family),
                           "Особенности: " + EnumUtils.ValueOf(analyse.Student.Background.Defect),
                           "Приводы в полицию: " + EnumUtils.ValueOf(analyse.Student.Background.Detained),
                           "Попытки суицида в семье: " + EnumUtils.ValueOf(analyse.Student.Background.Suicide)
                        },
                        this.ListReportData,
                        analyse.Psychologist.ReturnFullName()
                 );
            this.WordAddVisible(true);
        }

        private void AddHeaderWord(string method, DateTime date)
        {
            this.AddRange("Результаты психологического", 1);
            this.AddRange("тестирования по методике " + method, 1);
            this.AddRange("Дата тестирования: " + date.ToString("d"));
            this.AddRange("");
        }

        private void AddFullInformation(string method, DateTime date, string subject, string[] characteristic, List<ReportData> list_report_data, string psychologist)
        {
            this.AddHeaderWord(method, date);
            this.AddSubject(subject);
            this.AddPrimaryCharacteristic(characteristic);
            this.AddCalculatingCharacteristic(list_report_data);
            this.AddWhoWasConducted(psychologist);
        }

        private void AddSubject(string subject)
        {
            this.AddRange(subject, 0, WdParagraphAlignment.wdAlignParagraphLeft);
        }

        private void AddCalculatingCharacteristic(List<ReportData> list_report_data)
        {
            foreach (ReportData report_data in list_report_data)
            {
                if (report_data.ReportType == ReportType.asString)
                {
                    foreach (string data in report_data.Data)
                        if (data != string.Empty)
                            this.AddRange(data, 0, WdParagraphAlignment.wdAlignParagraphJustify);
                }
                if(report_data.ReportType == ReportType.asChart)
                    this.AddDiagram(report_data);
            }

        }

        private void AddPrimaryCharacteristic(string[] characteristic)
        {
            for (int s = 0; s < characteristic.Length; s++)
            {
                this.AddRange(characteristic[s], 0, WdParagraphAlignment.wdAlignParagraphLeft);
                if (s > 2)
                    this.AddRange("_____________________________________________________________");
            }
            this.AddRange("");
        }

        private void AddWhoWasConducted(string psychologist)
        {
            this.AddRange("");
            this.AddRange("Психолог                                                                 " + DateTime.Today.ToString("d"), 0, WdParagraphAlignment.wdAlignParagraphJustify);
            this.AddRange(psychologist + "                                Подпись____________", 0, WdParagraphAlignment.wdAlignParagraphJustify);

        }
    }
}
