using QuizEngine.Model.Data;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;

namespace QuizEngine.UI
{
    /// <summary>
    /// Business class for answers.
    /// </summary>
    public class AnswerUI : UIObject
    {
        #region Constructors
        public AnswerUI(UIContext context) : base(context) { }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Reads an <see cref="QuizEngine.Model.Answer">Answer model</see> by its ids.
        /// </summary>
        /// <param name="QuizUID">The id of the quiz</param>
        /// <param name="QuestionUID">The id of the question</param>
        /// <param name="ArchivedChoiceUID"The id of the archived choice</param>
        /// <returns>Returns an <see cref="QuizEngine.Model.Answer">Answer model</see></returns>
        public Answer ReadById(Guid quizUID, Guid questionUID, Guid userUID, Guid archivedChoiceUID)
        {
            return _context.Business.Objects.Answer.ReadById(quizUID, questionUID, userUID, archivedChoiceUID);
        }

        /// <summary>
        /// Return all users'answers.
        /// </summary>
        /// <returns>Returns a list of <see cref="DatabaseConnection.Model.Answer">Answer models</see>.</returns>
        public List<Answer> ReadAll()
        {
            return _context.Business.Objects.Answer.ReadAll();
        }

        /// <summary>
        /// Calls the method in the <see cref="QuizEngine.Repository.AnswerRepository">Answer Repository</see> for adding new answer.
        /// </summary>
        /// <param name="answer">Receives an <see cref="QuizEngine.Model.Answer">Answer model</see> as signature.</param>
        public void Insert(Answer answer)
        {
            _context.Business.Objects.Answer.Insert(answer);
        }

        /// <summary>
        /// Calls the method in the <see cref="QuizEngine.Repository.AnswerRepository">Answer Repository</see> for updating an answer.
        /// </summary>
        /// <param name="answer">Receives an <see cref="QuizEngine.Model.Answer">Answer model</see> as signature.</param>
        public void Update(Answer answer)
        {
            _context.Business.Objects.Answer.Update(answer);
        }

        /// <summary>
        /// Calls the method in the <see cref="QuizEngine.Repository.AnswerRepository">Answer Repository</see> for deleting the text of an answer.
        /// </summary>
        /// <param name="QuizUID">The id of the quiz.</param>
        /// <param name="QuestionUID">The id fo the question.</param>
        /// <param name="ArchivedChoiceUID">The id of the archived choise.</param>
        public void DeleteText(Guid quizUID, Guid questionUID, Guid userGuid, Guid archivedChoiceUID)
        {
            _context.Business.Objects.Answer.DeleteText(quizUID, questionUID, userGuid, archivedChoiceUID);
        }

        /// <summary>
        /// Calls the method in the Answer Repository for deleting the grade of an answer.
        /// </summary>
        /// <param name="QuizUID">The id of the quiz.</param>
        /// <param name="QuestionUID">The id fo the question.</param>
        /// <param name="ArchivedChoiceUID">The id of the archived choise.</param>
        public void DeleteGrade(Guid quizUID, Guid questionUID, Guid userGuid, Guid archivedChoiceUID)
        {
            _context.Business.Objects.Answer.DeleteGrade(quizUID, questionUID, userGuid, archivedChoiceUID);
        }
        /// <summary>
        /// Calls the repository context to read all answers of a quiz
        /// </summary>
        /// <param name="quizUID">the id of the quiz</param>
        /// <returns></returns>
        public List<Answer> ReadByQuizId(Guid quizUID)
        {
            return _context.Business.Objects.Answer.ReadByQuizId(quizUID);
        }
     
        /// <summary>
        /// Calls the method in the <see cref="DatabaseConnection.Repository.AnswerRepository">Answer Repository</see> for updating an answer.
        /// </summary>
        /// <param name="answer">Receives an <see cref="DatabaseConnection.Model.Answer">Answer model</see> as signature.</param>
        public void UpdateGrade(Answer answer)
        {
            _context.Business.Objects.Answer.UpdateGrade(answer);
        }

        public List<Answer> ReadByQuizUserId(Guid quizID, Guid userID)
        {
            return _context.Business.Objects.Answer.ReadByQuizUserId(quizID, userID);
        }
        #endregion Methods
    }
}

