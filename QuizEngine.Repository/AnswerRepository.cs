using QuizEngine.Model.Data;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QuizEngine.Repository
{
    /// <summary>
    /// Repository for answer models
    /// </summary>
    public class AnswerRepository : BaseRepository<Answer>
    {
        /// <summary>
        /// Stored procedures names
        /// </summary>
        #region Constants
        private const string ANSWER_READ_BY_ID = "dbo.Answers_ReadByID";
        private const string ANSWER_READ_ALL = "dbo.Answers_ReadAll";
        private const string ANSWER_INSERT = "dbo.Answers_Insert";
        private const string ANSWER_UPDATE = "dbo.Answers_Update";
        private const string ANSWER_DELETE_TEXT = "dbo.Answers_DeleteAnswerText";
        private const string ANSWER_DELETE_GRADE = "dbo.Answers_DeleteGrade";
        private const string ANSWER_READ_BY_QUIZ_ID = "dbo.Answers_ReadByQuizID";
        private const string ANSWER_UPDATE_GRADE = "dbo.Answers_UpdateGrade";
        private const string ANSWER_READ_BY_ARCHIVED_QUESTION_ID = "dbo.Answers_ReadByArchivedQuestionID";
        private const string ANSWER_READ_BY_ARCHIVED_CHOICE_ID = "dbo.Answers_ReadByArchivedChoiceID";
        private const string ANSWER_READ_BY_QUIZ_USER_ID = "dbo.Answer_ReadByQuizUser";
        #endregion Constants

        #region Constructors
        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="context">RepositoryContext</param>
        public AnswerRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods

        /// <summary>
        /// Retries an answer model
        /// </summary>
        /// <param name="quizUID">id of quiz</param>
        /// <param name="archivedQuestionUID">id of archived question</param>
        /// <param name="userUID">id of the user</param>
        /// <param name="archivedChoiceUID">id of the choice</param>
        /// <returns></returns>
        public Answer ReadByID(Guid quizUID, Guid archivedQuestionUID, Guid userUID, Guid archivedChoiceUID)
        {
            SqlParameter[] param = {
                                     new SqlParameter("@QuizGUID", SqlDbType.UniqueIdentifier){Value = quizUID},
                                     new SqlParameter("@ArchivedQuestionGUID", SqlDbType.UniqueIdentifier){Value = archivedQuestionUID},
                                     new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier){Value = userUID},
                                     new SqlParameter("@ArchivedChoiceGUID", SqlDbType.UniqueIdentifier){Value = archivedChoiceUID}
                                   };
            return ExecuteReadCommand(ANSWER_READ_BY_ID, ReaderToModel, param).FirstOrDefault();
        }
        /// <summary>
        /// Returns a list containing all the user answers.
        /// </summary>
        /// <returns>list of answer model</returns>
        public List<Answer> ReadAll()
        {
            return ExecuteReadCommand(ANSWER_READ_ALL, ReaderToModel);
        }

        /// <summary>
        /// Inserts an answer into the database
        /// </summary>
        /// <param name="answer">Answer model</param>
        public void Insert(Answer answer)
        {
            SqlParameter[] param = {
                                     new SqlParameter("@QuizGUID", SqlDbType.UniqueIdentifier){Value = answer.QuizUID},
                                     new SqlParameter("@ArchivedQuestionGUID", SqlDbType.UniqueIdentifier){Value = answer.ArchivedQuestionUID},
                                     new SqlParameter("@Grade", SqlDbType.Decimal){Value = answer.Grade},
                                     new SqlParameter("@AnswerText", SqlDbType.NVarChar){Value = answer.AnswerText},
                                     new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier){Value = answer.UserUID},
                                     new SqlParameter("@ArchivedChoiceGUID", SqlDbType.UniqueIdentifier){Value = answer.ArchivedChoiceUID}
                    };
            ExecuteCommand(ANSWER_INSERT, param);           
        }

        /// <summary>
        /// Updates an answer
        /// </summary>
        /// <param name="answer">Answer model</param>
        public void Update(Answer answer)
        {
            SqlParameter[] param = {
                                     new SqlParameter("@QuizGUID", SqlDbType.UniqueIdentifier){Value = answer.QuizUID},
                                     new SqlParameter("@ArchivedQuestionGUID", SqlDbType.UniqueIdentifier){Value = answer.ArchivedQuestionUID},
                                     new SqlParameter("@Grade", SqlDbType.Decimal){Value = answer.Grade},
                                     new SqlParameter("@AnswerText", SqlDbType.NVarChar){Value = answer.AnswerText},
                                     new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier){Value = answer.UserUID},
                                     new SqlParameter("@ArchivedChoiceGUID", SqlDbType.UniqueIdentifier){Value = answer.ArchivedChoiceUID}
                    };           
            ExecuteCommand(ANSWER_UPDATE, param);
        }
        /// <summary>
        /// Updates a certain answer from the database.
        /// </summary>
        /// <param name="answer"><see cref="DatabaseConnection.Model.Answer">Answer model</see> to be updated.</param>
        public void UpdateGrade(Answer answer)
        {
            SqlParameter[] param = {
                                     new SqlParameter("@QuizGUID", SqlDbType.UniqueIdentifier){Value = answer.QuizUID},
                                     new SqlParameter("@ArchivedQuestionGUID", SqlDbType.UniqueIdentifier){Value = answer.ArchivedQuestionUID},
                                     new SqlParameter("@Grade", SqlDbType.Decimal){Value = answer.Grade},
                                     new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier){Value = answer.UserUID},
                                     new SqlParameter("@ArchivedChoiceGUID", SqlDbType.UniqueIdentifier){Value = answer.ArchivedChoiceUID}
                    };
            ExecuteCommand(ANSWER_UPDATE_GRADE, param);
        }
        /// <summary>
        /// Deletes the text of an answer
        /// </summary>
        /// <param name="quizUID">id of quiz</param>
        /// <param name="archivedQuestionUID">id of archived question</param>
        /// <param name="userUID">id of the user</param>
        /// <param name="archivedChoiceUID">id of the archived choice</param>
        public void DeleteText(Guid quizUID, Guid archivedQuestionUID, Guid userUID, Guid archivedChoiceUID)
        {
            SqlParameter[] param = {
                                     new SqlParameter("@QuizGUID", SqlDbType.UniqueIdentifier){Value = quizUID},
                                     new SqlParameter("@ArchivedQuestionGUID", SqlDbType.UniqueIdentifier){Value = archivedQuestionUID},
                                     new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier){Value = userUID},
                                     new SqlParameter("@ArchivedChoiceGUID", SqlDbType.UniqueIdentifier){Value = archivedChoiceUID}
                    };
            ExecuteCommand(ANSWER_DELETE_GRADE, param);
        }

        /// <summary>
        /// Deletes the grade of an answer
        /// </summary>
        /// <param name="quizUID">id of quiz</param>
        /// <param name="archivedQuestionUID">id of archived question</param>
        /// <param name="userUID">id of the user</param>
        /// <param name="archivedChoiceUID">id of the archived choice</param>
        public void DeleteGrade(Guid quizUID, Guid archivedQuestionUID, Guid userUID, Guid archivedChoiceUID)
        {
                SqlParameter[] param = {
                                     new SqlParameter("@QuizGUID", SqlDbType.UniqueIdentifier){Value = quizUID},
                                     new SqlParameter("@ArchivedQuestionGUID", SqlDbType.UniqueIdentifier){Value = archivedQuestionUID},
                                     new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier){Value = userUID},
                                     new SqlParameter("@ArchivedChoiceGUID", SqlDbType.UniqueIdentifier){Value = archivedChoiceUID}
                    };
                ExecuteCommand(ANSWER_DELETE_GRADE, param);
        }
        /// <summary>
        /// Read by quiz uid
        /// </summary>
        /// <param name="quizUID">id of quiz</param>
        /// <returns></returns>
        public List<Answer> ReadByQuizId(Guid quizUID)
        {
            SqlParameter quizGUIDParam = new SqlParameter("@QuizGUID", SqlDbType.UniqueIdentifier);
            quizGUIDParam.Value = quizUID;
            return ExecuteReadCommand(ANSWER_READ_BY_QUIZ_ID, ReaderToModel, quizGUIDParam);
        }
        /// <summary>
        /// Creates a model from an sql data reader object.
        /// </summary>
        /// <param name="reader">SqlDataReader</param>
        /// <returns></returns>
        protected override Answer ReaderToModel(SqlDataReader reader)
        {
            return new Answer
            {
                QuizUID = reader.GetGuid(0),
                ArchivedQuestionUID = reader.GetGuid(1),
                Grade = Convert.ToDouble(reader.GetDecimal(2)),
                AnswerText = reader.GetString(3),
                UserUID = reader.GetGuid(4),
                ArchivedChoiceUID = reader.GetGuid(5)
            };
        }
        /// <summary>
        /// Read by archived question uid
        /// </summary>
        /// <param name="archivedQuestionUID">id of archived question</param>
        /// <returns></returns>
        public List<Answer> ReadByArchivedQuestionId(Guid archivedQuestionUID)
        {
            SqlParameter archivedQuestionGUIDParam = new SqlParameter("@ArchivedQuestionIdGUID", SqlDbType.UniqueIdentifier) { Value = archivedQuestionUID };
            return ExecuteReadCommand(ANSWER_READ_BY_ARCHIVED_QUESTION_ID, ReaderToModel, archivedQuestionGUIDParam);
        }
        /// <summary>
        /// Read by archived choice uid
        /// </summary>
        /// <param name="archivedChoiceUID"><id of archived choice/param>
        /// <returns></returns>
        public List<Answer> ReadByArchivedChoiceId(Guid archivedChoiceUID)
        {
            SqlParameter archivedChoiceGUIDParam = new SqlParameter("@ArchivedChoiceGUID", SqlDbType.UniqueIdentifier) { Value = archivedChoiceUID };
            return ExecuteReadCommand(ANSWER_READ_BY_ARCHIVED_CHOICE_ID, ReaderToModel, archivedChoiceGUIDParam);
        }


        /// <summary>
        /// Read all the answers of a quiz, taken by a specific user.
        /// </summary>
        /// <param name="quizUID"></param>
        /// <param name="userUID"></param>
        /// <returns></returns>
        public List<Answer> ReadByQuizUserId(Guid quizUID, Guid userUID)
        {
            SqlParameter[] parameters = {
            new SqlParameter("@QuizGUID",SqlDbType.UniqueIdentifier){Value = quizUID},
            new SqlParameter("@UserGUID",SqlDbType.UniqueIdentifier){Value = userUID}
            };
            return ExecuteReadCommand(ANSWER_READ_BY_QUIZ_USER_ID, ReaderToModel, parameters);
        }

        #endregion Methods
    }
}
