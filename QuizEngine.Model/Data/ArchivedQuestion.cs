using QuizEngine.Library;
using System;

namespace QuizEngine.Model.Data
{
    /// <summary>
    /// Archived Question model.
    /// </summary>
    public class ArchivedQuestion
    {       
        #region Members
        /// <summary>
        /// Members
        /// </summary>
        public Guid ArchivedQuestionID {get; set;}
        public string ArchivedQuestionText { get; set; }
        public Guid CategoryID { get; set; }
        public QuestionType QuestionType { get; set; }
        public Guid LevelID { get; set; }
        public Guid QuizID { get; set; }
        public Guid IndexOrder { get; set; }
        #endregion Members
    }
}
