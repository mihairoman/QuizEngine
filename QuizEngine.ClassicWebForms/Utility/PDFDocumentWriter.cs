
using QuizEngine.Model.Data;
using System.Collections.Generic;
using System.IO;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;
using System;
using QuizEngine.UI.Core;
using QuizEngine.Library;
namespace QuizEngine.ClassicWebForms.Utility
{
    /// <summary>
    /// Class that generates a PDF document on a stream and returns the stream of data.
    /// </summary>
    public class PDFDocumentWriter : QuizDocumentWriter
    {
        #region Constructor
        public PDFDocumentWriter(UIContext uiContext) : base(uiContext) { }
        #endregion

        #region Methods

        /// <summary>
        /// Method that is a able to generate a PDF file on a stream
        /// </summary>
        /// <returns>Returns a stream of bytes that represents the PDF file content </returns>
        public override byte[] GenerateFile()
        {
            if (_questionList.Count > 0)
            {
                try
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (PdfDocument document = new PdfDocument())
                        {
                            document.DocumentInformation.Author = _userName;
                            document.DocumentInformation.Creator = _userName;
                            document.DocumentInformation.Keywords = "pdf, demo, document information";
                            document.DocumentInformation.Subject = "Generated Quiz";
                            document.DocumentInformation.Title = "Generated Quiz";

                            WriteDocumentQuestionsAndChoices(document, false);
                            WriteDocumentQuestionsAndChoices(document, true);

                            document.SaveToStream(stream);
                            return stream.ToArray();
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Writes the document questions and choices
        /// </summary>
        /// <param name="document">Pdf Document</param>
        /// <param name="isSolutionPage">Solving or solution page</param>
        private void WriteDocumentQuestionsAndChoices(PdfDocument document, bool isSolutionPage)
        {
            PdfPageBase page = document.Pages.Add();

            float yAxisIndex = 10;

            page.Canvas.DrawString("UMT SOFTWARE © - QUIZENGINE",
                                    new PdfFont(PdfFontFamily.Helvetica, 7f),
                                    PdfBrushes.Black, 0, 0);
            yAxisIndex += 30;

            if (isSolutionPage.Equals(true))
            {
                page.Canvas.DrawString("Solution",
                                        new PdfFont(PdfFontFamily.Helvetica, 14f),
                                        PdfBrushes.Black,
                                        new Point((int)(page.GetClientSize().Width / 2 - 20), (int)yAxisIndex),
                                        new PdfStringFormat(PdfTextAlignment.Center));
                yAxisIndex += 30;
            }

            int index = 1;

            foreach (var question in _questionList)
            {
                if (yAxisIndex > page.GetClientSize().Height - 20)
                {
                    yAxisIndex = 10;
                    page = document.Pages.Add();
                }

                DrawQuestionString(page,ref yAxisIndex, index, question, question.QuestionType);

                yAxisIndex += 20;
                              

                List<ArchivedChoice> choiceList = _questionAndChoices[question.ArchivedQuestionID];

                
                foreach (var choice in choiceList)
                {

                    if (yAxisIndex > (page.GetClientSize().Height - 20))
                    {
                        yAxisIndex = 20;
                        page = document.Pages.Add();
                    }

                    switch (question.QuestionType)
                    {
                        case QuestionType.Single:
                        case QuestionType.MultiChoice:
                        case QuestionType.Weighted:
                            if (isSolutionPage && choice.IsCorrect == true)
                            {
                                page.Canvas.DrawRectangle(PdfBrushes.Black, 10, yAxisIndex, 10, 10);
                            }
                            else
                            {
                                page.Canvas.DrawRectangle(new PdfPen(PdfBrushes.Black, 0), 10, yAxisIndex, 10, 10);

                            }
                            if (new PdfTrueTypeFont(new Font("Helvetica", 10f))
                                        .MeasureString(choice.AnswerText,
                                        new PdfStringFormat(PdfTextAlignment.Justify))
                                        .Width < page.GetClientSize().Width - 20)
                            {
                                page.Canvas.DrawString(choice.AnswerText,
                                                   new PdfFont(PdfFontFamily.Helvetica, 10f),
                                                   new PdfSolidBrush(Color.Black),
                                                   30, yAxisIndex);
                            }
                            else
                            {
                                int stringIndex = 0;
                                int choiceTextStringLength = choice.AnswerText.Length;
                                while (stringIndex < choiceTextStringLength)
                                {
                                    string newChoiceTextString = "";
                                    while (new PdfTrueTypeFont(new Font("Arial", 10f, FontStyle.Bold), true)
                                                                .MeasureString(newChoiceTextString,
                                                                new PdfStringFormat(PdfTextAlignment.Justify))
                                                                .Width < page.GetClientSize().Width - 20
                                           && stringIndex < choiceTextStringLength)
                                    {
                                        if (stringIndex < choiceTextStringLength)
                                        {
                                            if (choice.AnswerText[stringIndex] != '\n')
                                            {
                                                newChoiceTextString = string.Concat(newChoiceTextString, choice.AnswerText[stringIndex]);
                                                stringIndex++;
                                            }
                                            else
                                            {
                                                stringIndex += 2;
                                                break;
                                            }
                                        }
                                    }
                                    if (stringIndex < choiceTextStringLength)
                                    {
                                        if (choice.AnswerText[stringIndex] != ' ')
                                        {
                                            int length = newChoiceTextString.Length - 1;
                                            while (newChoiceTextString[length] != ' ' && length >= 0)
                                            {
                                                length--;
                                                stringIndex--;
                                            }
                                            newChoiceTextString = newChoiceTextString.Substring(0, length);
                                        }
                                    }
                                    page.Canvas.DrawString(newChoiceTextString,
                                                   new PdfFont(PdfFontFamily.Helvetica, 10f),
                                                   new PdfSolidBrush(Color.Black),
                                                   30, yAxisIndex);
                                    yAxisIndex += 20;
                                }  
                            }
                           
                            break;
                        case QuestionType.TrueFalse:
                            if (isSolutionPage && choice.IsCorrect == true)
                            {
                                page.Canvas.DrawRectangle(PdfBrushes.Black, 10, yAxisIndex, 10, 10);
                            }
                            else
                            {
                                page.Canvas.DrawRectangle(new PdfPen(PdfBrushes.Black, 0), 10, yAxisIndex, 10, 10);
                            }
                            page.Canvas.DrawString("YES",
                                new PdfFont(PdfFontFamily.Helvetica, 10f),
                                new PdfSolidBrush(Color.Black),
                                30, yAxisIndex);
                            yAxisIndex += 20;
                            if (isSolutionPage && choice.IsCorrect == false)
                            {
                                page.Canvas.DrawRectangle(PdfBrushes.Black, 10, yAxisIndex, 10, 10);
                            }
                            else
                            {
                                page.Canvas.DrawRectangle(new PdfPen(PdfBrushes.Black, 0), 10, yAxisIndex, 10, 10);
                            }
                            page.Canvas.DrawString("NO",
                                                    new PdfFont(PdfFontFamily.Helvetica, 10f),
                                                    new PdfSolidBrush(Color.Black),
                                                    30, yAxisIndex);
                            break;
                        case QuestionType.FreeText:
                            if (isSolutionPage)
                            {
                                page.Canvas.DrawString("User answer will be graded by an admin",
                                                        new PdfFont(PdfFontFamily.Helvetica, 10f),
                                                        new PdfSolidBrush(Color.Black),
                                                        30, yAxisIndex);
                            }

                            yAxisIndex += 50;
                            break;
                    }

                    yAxisIndex = yAxisIndex + new PdfTrueTypeFont(new Font("Arial", 14f, FontStyle.Bold), true).MeasureString(choice.AnswerText, new PdfStringFormat(PdfTextAlignment.Center)).Height;

                }

                index++;
            }
        }

        private static void DrawQuestionString(PdfPageBase page,ref float yAxisIndex, int index, ArchivedQuestion question,
            QuestionType questionType)
        {
            string questionDescription = "";

            switch (question.QuestionType)
            {
                case QuestionType.MultiChoice:
                    questionDescription = "(Select correct choices)";
                    break;
                case QuestionType.Weighted:
                case QuestionType.Single:
                    questionDescription = "(Select the correct choice)";
                    break;
            }
            string questionText = string.Concat(index,
                                                ". ",
                                                question.ArchivedQuestionText,
                                                string.Format(" {0}", questionDescription));

            if (new PdfTrueTypeFont(new Font("Arial", 20f, FontStyle.Bold), true)
                                        .MeasureString(question.ArchivedQuestionText,
                                        new PdfStringFormat(PdfTextAlignment.Justify))
                                        .Width < page.GetClientSize().Width - 20)
            {
                page.Canvas.DrawString(questionText,
                                        new PdfFont(PdfFontFamily.Helvetica, 12f),
                                        new PdfSolidBrush(Color.Black),
                                        10, yAxisIndex);
                int length = questionText.Length;
                for (int iterator = 0; iterator < length; iterator++)
                {
                    if (questionText[iterator].Equals('\n'))
                    {
                        yAxisIndex += 20;
                    }
                }
            }
            else
            {

                int stringIndex = 0;
                int questionTextStringLength = questionText.Length;
                while (stringIndex < questionTextStringLength)
                {
                    string newQuestionTextString = "";
                    while (new PdfTrueTypeFont(new Font("Arial", 12f, FontStyle.Bold), true)
                                                .MeasureString(newQuestionTextString,
                                                new PdfStringFormat(PdfTextAlignment.Justify))
                                                .Width < page.GetClientSize().Width - 20
                           && stringIndex < questionTextStringLength)                           
                    {
                        if (stringIndex < questionTextStringLength)
                        {
                            if (questionText[stringIndex] != '\n')
                            {
                                newQuestionTextString = string.Concat(newQuestionTextString, questionText[stringIndex]);
                                stringIndex++;
                            }
                            else
                            {
                                stringIndex+=2;
                                break;
                            }
                        }
                    }
                    if (stringIndex < questionTextStringLength)
                    {
                        if (questionText[stringIndex] != ' ')
                        {
                            int length = newQuestionTextString.Length - 1;
                            while (newQuestionTextString[length] != ' ' && length >= 0)
                            {
                                length--;
                                stringIndex--;
                            }
                            newQuestionTextString = newQuestionTextString.Substring(0, length);
                        }
                    }
                    page.Canvas.DrawString(newQuestionTextString,
                                   new PdfFont(PdfFontFamily.Helvetica, 12f),
                                   new PdfSolidBrush(Color.Black),
                                   10, yAxisIndex);
                    yAxisIndex += 20;
                }

            }            
        }



        #endregion Methods

    }
}
