using System;
using QuizEngine.Library;

namespace QuizEngine.Model.View
{
    /// <summary>
    /// View for the quiz final result
    /// </summary>
    public class QuizResultView
    {
        /// <summary>
        /// Id of quiz, id of archived question, result per question
        /// </summary>
        #region Members
        public Guid QuizUID {get; set;}
        public Guid ArchivedQuestionUID { get; set; }
        public Double Result { get; set; }
        #endregion Members
    }
}
