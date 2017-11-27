using System;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop;
using System.Collections.Generic;
using PCS_Forms.Core;
using System.IO;
namespace PCS_Forms.DataOut
{
   public class Report:IDisposable
    {
       Microsoft.Office.Interop.Word.Application WordApp;
        Document WordDoc;
        Range Range;

        public Report(AnalyseData analyse)
        {
            this.WordApp =new Application();
            this.WordDoc = this.WordApp.Documents.Add(Type.Missing);
            this.WordApp.DocumentBeforeClose+=WordApp_DocumentBeforeClose;
            this.WriteToWord(analyse);
            
        }

        public Report()
        {
            this.WordApp = new Application();
            string path = @Directory.GetCurrentDirectory() + @"\Files\Dictionary.docx";
            this.WordDoc = this.WordApp.Documents.Open(path);
            this.WordAddVisible(true);
        }

        private void WordApp_DocumentBeforeClose(Document Doc, ref bool Cancel)
        {
            this.Dispose();
            
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

        //public Microsoft.Office.Interop.Excel.Workbook AddDiagram(ReportData repData)
        //{
            //Microsoft.Office.Interop.Word.Chart wdChart = this.WordDoc.Shapes.AddChart2(227, Microsoft.Office.Core.XlChartType.xlLine).Chart;
            //Microsoft.Office.Interop.Word.ChartData chartData = wdChart.ChartData;
            //WrapFormat wrapformat = (WrapFormat)this.WordDoc.Shapes[1].WrapFormat;
            //wrapformat.Type = WdWrapType.wdWrapTopBottom;
            //this.WordDoc.Shapes[1].Top = 10;
            //this.WordDoc.Shapes[1].TopRelative = 1000;

            
            //Microsoft.Office.Interop.Excel.Workbook dataWorkBook = (Microsoft.Office.Interop.Excel.Workbook)chartData.Workbook;
            //Microsoft.Office.Interop.Excel.Worksheet dataSheet = (Microsoft.Office.Interop.Excel.Worksheet)dataWorkBook.Worksheets[1];
            //Microsoft.Office.Interop.Excel.Range tRange = (Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("A1", "B7");
            //Microsoft.Office.Interop.Excel.ListObject tbl1 = dataSheet.ListObjects[1];
            //tbl1.Resize(tRange);   
            //for (byte i = 0; i < repData.Data.Count; i++)
            //{
            //    ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("A" + (i+2))).FormulaR1C1 = "Квадрат № " + (i + 1);
            //    ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("B" + (i+2))).FormulaR1C1 = repData.Data[i];
            //}
            //((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("B1")).FormulaR1C1= "Время";

            //return dataWorkBook;
        //}

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
            this.AddFullInformation(analyse);
            this.WordAddVisible(true);
            
        }

        private void AddHeaderWord(string method, DateTime date)
        {
            this.AddRange("Результаты психологического", 1);
            this.AddRange("тестирования по методике " + method, 1);
            this.AddRange("Дата тестирования: " + date.ToString("d"));
            this.AddRange(string.Empty);
        }

        private void AddFullInformation(AnalyseData analyse)
        {
            this.AddHeaderWord(analyse.Methodology.Method.ToString(), analyse.DateTesting);
            this.AddTestedName(analyse.Student);
            this.AddPrimaryCharacteristic(analyse.Student);
            this.AddCalculatingCharacteristic(analyse.ListReportData);
            this.AddWhoWasConducted(analyse.Psychologist);
        }

        private void AddTestedName(Student student)
        {
            this.AddRange(student.ReturnFullName(), 0, WdParagraphAlignment.wdAlignParagraphLeft);
        }

        private void AddCalculatingCharacteristic(List<ReportData> list_report_data)
        {
            foreach (ReportData report_data in list_report_data)
            {
                if (report_data.Type == ReportType.asString)
                {
                    foreach (string data in report_data.Data)
                        if (data != string.Empty)
                            this.AddRange(data, 0, WdParagraphAlignment.wdAlignParagraphJustify);
                }
                if (report_data.Type == ReportType.asChart)
                {
                    string values = string.Empty;
                    foreach (string value in report_data.Data)
                        values += value + " ";
                    if (!string.IsNullOrEmpty(values))
                        this.AddRange(values, 0, WdParagraphAlignment.wdAlignParagraphJustify);
                    // Возникает исключение hresult при вставке графика
                    //try
                    //{
                    //    this.workbook = AddDiagram(report_data);
                        
                    //}
                    //catch
                    //{
                    //    this.Dispose();
                    //}
                }
            }
            this.AddRange(string.Empty);
        }

        private void AddPrimaryCharacteristic(Student student)
        {
            this.AddRange("Образование: " + student.ValueOfEducation(), 0, WdParagraphAlignment.wdAlignParagraphLeft);
            this.AddRange("Состав семьи: " + student.ValueOfFamily(), 0, WdParagraphAlignment.wdAlignParagraphLeft);
            this.AddRange("Особенности: " + student.ValueOfDefect(), 0, WdParagraphAlignment.wdAlignParagraphLeft);
            this.AddRange("Приводы в полицию: " + student.ValueOfDetained(), 0, WdParagraphAlignment.wdAlignParagraphLeft);
            this.AddRange("Попытки суицида в семье: " + student.ValueOfSuicide(), 0, WdParagraphAlignment.wdAlignParagraphLeft);
            this.AddRange(string.Empty);
            this.AddRange("Заметки:", 0, WdParagraphAlignment.wdAlignParagraphLeft);
            this.AddRange("_______________________________________________________");
            this.AddRange("_______________________________________________________");
            this.AddRange("_______________________________________________________");
            this.AddRange(string.Empty);
            
        }

        private void AddWhoWasConducted(Psychologist psychologist)
        {
            this.AddRange("");
            this.AddRange("Психолог                                                                 " + DateTime.Today.ToString("d"), 0, WdParagraphAlignment.wdAlignParagraphJustify);
            this.AddRange(psychologist.ReturnFullName() + "                                Подпись____________", 0, WdParagraphAlignment.wdAlignParagraphJustify);

        }
    }
}
