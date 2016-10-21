using System;

namespace QuizEngine.Model.Data
{   
    /// <summary>
    /// Class QuestionsTags
    /// </summary>
    public class QuestionTag
    {
        #region Members
        private Guid _questionUID;
        private Guid _tagUID;
        #endregion Members

        #region Constructors
        public QuestionTag()
        {

        }
        /// <summary>
        /// Instantiates a <see cref="QuizEngine.Model.Data.QuestionTag"/>.
        /// </summary>
        /// <param name="questionUID"></param>
        /// <param name="tagUID"></param>
        public QuestionTag(Guid questionUID, Guid tagUID)
        {
            _questionUID = questionUID;
            _tagUID = tagUID;
        }
        #endregion Constructors

        #region Properties
        public Guid QuestionUID
        {
            get { return _questionUID; }
            set { _questionUID = value; }
        }

        public Guid TagUID
        {
            get { return _tagUID; }
            set { _tagUID = value; }
        }
        #endregion Properties

    }
}
