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
        private Microsoft.Office.Interop.Word.Application _wordApp;
        private Document _wordDoc;
        private Range _range;

        public Report(AnalyseData analyse)
        {
            this._wordApp = new Application();
            this._wordDoc = this._wordApp.Documents.Add(Type.Missing);
            this.WriteToWord(analyse);

        }

        public Report()
        {
            this._wordApp = new Application();
            string path = @Directory.GetCurrentDirectory() + @"\Files\Dictionary.docx";
            this._wordDoc = this._wordApp.Documents.Open(path);
            this.WordAddVisible(true);
        }

        public void Dispose()
        {
            this._wordApp = null;
            this._wordDoc = null;
            this._range = null;
            
        }

        private void AddParagraph(Range range,WdParagraphAlignment alignment)
        {
            this._wordDoc.Paragraphs.Add(range);
            this._wordDoc.Paragraphs.Last.Alignment = alignment;
            
        }

        public void AddRange(string text, int bold = 0, WdParagraphAlignment alignment = WdParagraphAlignment.wdAlignParagraphCenter)
        {
            if (_range == null)
                _range = _wordDoc.Range(0, 0);
            else
                this._range.Start = _range.End;   
            this._range.Text = text;
            this._range.Bold = bold;
            this.AddParagraph(_range,alignment);
        }

        public void WordAddVisible(bool visible)
        {
            this._wordApp.Visible = visible;
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
