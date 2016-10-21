using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Model.Data
{
    /// <summary>
    /// Model for the answer.
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Properties of the Answer Model.
        /// </summary>
        #region Properties
        public Guid QuizUID { get; set; }
        public Guid ArchivedQuestionUID { get; set; }
        public Double Grade { get; set; }
        public String AnswerText { get; set; }
        public Guid UserUID { get; set; }
        public Guid ArchivedChoiceUID { get; set; }
        #endregion Properties
    }
}
