using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Model.Data.View
{
    public class ValidationFreeTextQuestionsView
    {
        #region Properties
        public Guid QuizUID { get; set; }
        public Guid UserUID { get; set; }
        public Guid ArchivedQuestionUID { get; set; }
        public Guid ArchivedChoiceUID { get; set; }
        public Int16 QuestionType { get; set; }
        public string QuestionText { get; set; }
        public string UserAnswerText { get; set; }
        public decimal? Grade { get; set; }
        #endregion
    }
}
