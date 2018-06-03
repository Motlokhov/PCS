using Core.Person;
using Core.Test;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;

namespace Report
{
   public class Report
   {
       private WordprocessingDocument _wordDoc;
       private MainDocumentPart _mainPart;
       private Document _document;
       private Body _body;
       private Paragraph _currentParagraph;
       private readonly Result _result;

       public Report(Result result)
       {
           _result = result;
           //using (var wordDocument = WordprocessingDocument.Create(@"C:\dou.docx", WordprocessingDocumentType.Document, true))
           //{
           //    var mainPart = wordDocument.AddMainDocumentPart();
           //    mainPart.Document = new Document();
           //    mainPart.Document.Body = new Body();
           //    var body = mainPart.Document.Body;
           //    var p = new Paragraph();
           //    var run = new Run();
           //    var text = new Text("Новый текст");
           //    run.Append(text);
           //    p.Append(run);
           //    body.Append(p);
           //    wordDocument.MainDocumentPart.Document.Save();
           //    wordDocument.Close();
           //}
       }

       public void SaveTo(string path)
       {
           using (_wordDoc = WordprocessingDocument.Create(@path,WordprocessingDocumentType.Document,true))
           {
               _mainPart = _wordDoc.AddMainDocumentPart();
               _document = new Document();
               _body = new Body();
               _document.Body = _body;
               _mainPart.Document = _document;
               AddFullInformation();
               _wordDoc.MainDocumentPart.Document.Save();
               _wordDoc.Close();
           }
       }

       private void NewParagraph(int aligment)
       {
           _currentParagraph = new Paragraph();
           _currentParagraph.Append(AddAligment(aligment));
       }

       private void AppendCurrentParagraph()
       {
           _body.Append(_currentParagraph);
       }

       private RunProperties AddBold()
       {
           var rProp = new RunProperties();
           var boldStyle = new Bold();
           boldStyle.Val = true;
           rProp.Append(boldStyle);
           return rProp;
       }

       private ParagraphProperties AddAligment(int aligment)
       {
           var pProp = new ParagraphProperties();
           Justification justification = new Justification() { Val = JustificationValues.Center };
           switch (aligment)
           {
               case 1: { justification.Val = JustificationValues.Center; break; }
               case 2: { justification.Val = JustificationValues.Both; break; }
               default: { justification.Val = JustificationValues.Left; break; }
           }
           pProp.Append(justification);
           return pProp;
       }

       private void AddText(string text,bool bold = false, int aligment = 0)
       {
           NewParagraph(aligment);
           var newText = new Text(text);
           
           var run = new Run();
           if (bold)
           {
               run.Append(AddBold());
           }
           run.Append(newText);
           _currentParagraph.Append(run);
           AppendCurrentParagraph();
       }

       private void AddHeader(string method, string date)
       {
           
           AddText("Результаты психологического",true,1);
           AddText("Тестирования по методике " + method, true,1);
           AddText("Дата тестирования: " + date, true,1);          
       }

       private void AddFullInformation()
       {
           AddHeader(_result.Method.ToString(),_result.Date);
           AddTestedInfo(_result.Tested);
           AddCalculatingCharacteristic(_result.Interpretation);
           AddPsycologistInfo(_result.Psy);
       }


       private void AddCalculatingCharacteristic(List<string> list)
       {
           AddText("Характеристика", true);
           foreach (var str in list)
           {
               AddText(str,false,2);
           }
       }

       private void AddTestedInfo(Tested student)
       {
           AddText("Тестируемый: " + student.FullName);
           AddText("Образование: " + student.Education);
           AddText("Состав семьи: " + student.Composition);
           AddText("Особенности: " + student.Defect);
           AddText("Приводы в полицию: " + student.Detained);
           AddText("Попытки суицида в семье: " + student.Suicide);
           AddText(string.Empty);
           AddText("Заметки: __________________________________________________________________________________"+
                            "__________________________________________________________________________________");
           AddText(string.Empty);
       }

       private void AddPsycologistInfo(Psycologist psycologist)
       {
           AddText(string.Empty);
           AddText("Психолог                                                                                    " + DateTime.Today.ToString("d"),true);
           AddText(psycologist.FullName + "                                             Подпись____________",true);
       }
    }
}
