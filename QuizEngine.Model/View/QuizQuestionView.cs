using System;
using System.Collections.Generic;
using QuizEngine.Library;
using QuizEngine.Model.Data;

namespace QuizEngine.Model.View
{
    /// <summary>
    /// Model used to populate an OnlineTestingWizardPage
    /// </summary>
    public class QuizQuestionView
    {
        #region Properties
        public Guid QuestionID { get; set; }
        public string Text { get; set; }
        public QuestionType QuestionType { get; set; }
        public List<ArchivedChoice> Choices { get; set; }
        #endregion Properties
    }
}
