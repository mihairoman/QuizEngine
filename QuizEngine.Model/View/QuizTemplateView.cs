using System;
namespace QuizEngine.Model.Data.View
{
    public class QuizTemplateView
    {
        #region Properties
        public Guid   QuizTemplateUID { get; set; }
        public string QuizTemplateName { get; set; }
        public int    QuestionNumber { get; set; }
        #endregion Properties
    }
}
