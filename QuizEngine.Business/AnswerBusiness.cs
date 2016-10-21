using QuizEngine.Repository;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using QuizEngine.Infrastructure;
using QuizEngine.Business.Core;
namespace QuizEngine.Business
{
    /// <summary>
    /// Business class for answers.
    /// </summary>
    public class AnswerBusiness : BusinessObject
    {
        #region Constructors
       /// <summary>
       /// Create a new Answer Business
       /// </summary>
        /// <param name="businessContext">Business Context</param>
        public AnswerBusiness(BusinessContext businessContext)
            : base(businessContext)
        { }
        #endregion Constructors

        #region Methods
       /// <summary>
       /// Calls the Repository context to read an answer model by its uids
       /// </summary>
       /// <param name="quizUID">id of the quiz</param>
       /// <param name="questionUID">id of the archived question</param>
       /// <param name="userUID">id fo the user</param>
       /// <param name="archivedChoiceUID">id of the archived choice</param>
       /// <returns></returns>
        public Answer ReadById(Guid quizUID, Guid questionUID, Guid userUID, Guid archivedChoiceUID)
        {
            return _context.Repository.Objects.Answer.ReadByID(quizUID, questionUID, userUID, archivedChoiceUID);
        }
        /// <summary>
        /// Calls the repository context to read all answer models
        /// </summary>
        /// <returns></returns>
        public List<Answer> ReadAll()
        {
            return _context.Repository.Objects.Answer.ReadAll();
        }

        /// <summary>
        /// Calls the repository context to insert a new answer
        /// </summary>
        /// <param name="answer">an answer model</param>
        public void Insert(Answer answer)
        {
            _context.Repository.Objects.Answer.Insert(answer);
        }

        /// <summary>
        /// Calls the repository context to update an answer
        /// </summary>
        /// <param name="answer">Answer model</param>
        public void Update(Answer answer)
        {
            _context.Repository.Objects.Answer.Update(answer);
        }
        /// <summary>
            /// Calls the method in the <see cref="DatabaseConnection.Repository.AnswerRepository">Answer Repository</see> for updating an answer.
            /// </summary>
            /// <param name="answer">Receives an <see cref="DatabaseConnection.Model.Answer">Answer model</see> as signature.</param>
            public void UpdateGrade(Answer answer)
            {
                _context.Repository.Objects.Answer.UpdateGrade(answer);
            }
        /// <summary>
        /// Calls the repository context to delete the text of an answer
        /// </summary>
        /// <param name="quizUID">quiz uid</param>
        /// <param name="questionUID"> question uid</param>
        /// <param name="userUID">user uid</param>
        /// <param name="archivedChoiceUID">choice uid</param>
        public void DeleteText(Guid quizUID, Guid questionUID, Guid userUID, Guid archivedChoiceUID)
        {
            _context.Repository.Objects.Answer.DeleteText(quizUID, questionUID, userUID, archivedChoiceUID);
        }

        /// <summary>
        ///  Calls the repository context to delete the grade of an answer
        /// </summary>
        /// <param name="quizUID">quiz uid</param>
        /// <param name="questionUID">question uid</param>
        /// <param name="userUID">user uid</param>
        /// <param name="archivedChoiceUID">choice uid</param>
        public void DeleteGrade(Guid quizUID, Guid questionUID, Guid userUID, Guid archivedChoiceUID)
        {
            _context.Repository.Objects.Answer.DeleteGrade(quizUID, questionUID, userUID, archivedChoiceUID);
        }
        /// <summary>
        /// Calls the repository context to read all answers of a quiz
        /// </summary>
        /// <param name="quizUID">the id of the quiz</param>
        /// <returns></returns>
        public List<Answer> ReadByQuizId(Guid quizUID)
        {
            return _context.Repository.Objects.Answer.ReadByQuizId(quizUID);
        }


        /// <summary>
        /// Read all the answers of a quiz, taken by a specific user.
        /// </summary>
        /// <param name="quizUID"></param>
        /// <param name="userUID"></param>
        /// <returns></returns>
        public List<Answer> ReadByQuizUserId(Guid quizUID, Guid userUID)
        {
            return _context.Repository.Objects.Answer.ReadByQuizUserId(quizUID, userUID);
        }

        #endregion Methods
    }
}

