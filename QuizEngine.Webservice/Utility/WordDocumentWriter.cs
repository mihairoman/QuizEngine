using Novacode;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace QuizEngine.Webservice.Utility
{
    /// <summary>
    /// Class that is able to generate a Word Document of a Quiz on a stream
    /// </summary>
    public class WordDocumentWriter : QuizDocumentWriter
    {
        #region Constructor
        public WordDocumentWriter(UIContext uiContext) : base(uiContext) { }
        #endregion

        #region Methods
        /// <summary>
        /// Method that is able to generate a WORD document and put it on a stream
        /// </summary>
        /// <returns>Stream of data</returns>
        public override byte[] GenerateFile()
        {
            if (_questionList.Count > 0)
            {
                try
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (DocX document = DocX.Create(stream))
                        {
                            document.AddCustomProperty(new CustomProperty("Username", _userName));
                            document.AddCustomProperty(new CustomProperty("Creation Date", System.DateTime.Now.ToString()));
                            document.AddHeaders();
                            document.AddFooters();

                            Header header = document.Headers.odd;
                            Footer footer = document.Footers.odd;

                            footer.PageNumbers = true;
                            Paragraph headerParagraph = header.InsertParagraph();
                            headerParagraph.AppendLine("UMT SOFTWARE").Font(new FontFamily("ARIAL")).Append("©").Append(" - QUIZENGINE").Font(new FontFamily("ARIAL"));

                            WriteDocumentQuestionsAndChoices(document, false);
                            WriteDocumentQuestionsAndChoices(document, true);

                            document.Save();
                        }
                        return stream.ToArray();
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
        /// Writes in the document the questions and choices
        /// </summary>
        /// <param name="document">Word document</param>
        /// <param name="isSolutionPage">Solving/Solution page</param>

        private void WriteDocumentQuestionsAndChoices(DocX document, bool isSolutionPage)
        {
            if (isSolutionPage)
            {
                document.InsertSection();
                Paragraph paragraph1 = document.InsertParagraph();
                paragraph1.AppendLine("Solution").FontSize(18).Font(new FontFamily("Arial")).Alignment = Alignment.center;
            }

            Paragraph paragraph = document.InsertParagraph();

            int index = 1;

            foreach (var question in _questionList)
            {
                GenerateQuestionParagraph(paragraph, question, index);

                List<ArchivedChoice> choiceList = _questionAndChoices[question.ArchivedQuestionID];

                foreach (var choice in choiceList)
                {
                    switch (question.QuestionType)
                    {
                        case QuestionType.Single:
                        case QuestionType.MultiChoice:
                        case QuestionType.Weighted:
                            if (isSolutionPage && choice.IsCorrect == true)
                            {
                                paragraph.AppendLine("g").Font(new FontFamily("Webdings")).Append(string.Concat("   ", choice.AnswerText, "\n")).Font(new FontFamily("Arial")).Color(Color.Black);
                            }
                            else
                            {
                                paragraph.AppendLine("c").Font(new FontFamily("Webdings")).Append(string.Concat("   ", choice.AnswerText, "\n")).Font(new FontFamily("Arial")).Color(Color.Black);
                            }
                            break;
                        case QuestionType.TrueFalse:
                            if (isSolutionPage && choice.IsCorrect == true)
                            {
                                paragraph.AppendLine("g").Font(new FontFamily("Webdings")).Append(" TRUE\n").Font(new FontFamily("Arial")).Color(Color.Black);
                            }
                            else if (isSolutionPage && choice.IsCorrect == false)
                            {
                                paragraph.AppendLine("g").Font(new FontFamily("Webdings")).Append(" FALSE\n").Font(new FontFamily("Arial")).Color(Color.Black);
                            }
                            else
                            {
                                paragraph.AppendLine("c").Font(new FontFamily("Webdings")).Append(" TRUE\n").Font(new FontFamily("Arial")).Color(Color.Black);
                                paragraph.AppendLine("c").Font(new FontFamily("Webdings")).Append(" FALSE\n").Font(new FontFamily("Arial")).Color(Color.Black);
                            }
                            break;
                        default:
                            if (isSolutionPage)
                            {
                                paragraph.AppendLine("Pending Admin Grade!\n");
                            }
                            else
                            {
                                paragraph.AppendLine("\n");
                                paragraph.AppendLine("\n");
                                paragraph.AppendLine("\n");
                            }
                            break;
                    }
                }
                index++;
            }
        }

        private static void GenerateQuestionParagraph(Paragraph paragraph, ArchivedQuestion question, int index)
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
            string questionText = string.Concat(index, ". ", question.ArchivedQuestionText, " ");
            paragraph.AppendLine(questionText)
                     .Font(new FontFamily("Arial"))
                     .Color(Color.DarkSlateBlue)
                     .Font(new FontFamily("Aldhabi"))
                     .FontSize(12);
            paragraph.AppendLine(string.Format("{0} \n", questionDescription)).Color(Color.DarkRed).FontSize(11);
        }
        #endregion Methods
    }
}

