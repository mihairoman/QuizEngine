using QuizEngine.Library;
using QuizEngine.Model.Data;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QuizEngine.Repository
{
    public class ArchivedQuestionRepository : BaseRepository<ArchivedQuestion>
    {
        #region Constants
        private const string ARCHIVED_QUESTION_INSERT = "dbo.ArchivedQuestions_Insert";
        private const string ARCHIVED_QUESTION_INSERT_BY_ID = "dbo.ArchivedQuestions_InsertById";
        private const string ARCHIVED_QUESTION_DELETE = "dbo.ArchivedQuestions_Delete";
        private const string ARCHIVED_QUESTION_UPDATE = "dbo.ArchivedQuestions_Update";
        private const string ARCHIVED_QUESTION_READ_BY_ID = "dbo.ArchivedQuestions_ReadByID";
        private const string ARCHIVED_QUESTION_READ_ALL = "dbo.ArchivedQuestions_ReadAll";
        private const string ARCHIVED_QUESTION_READ_ALL_BY_QUIZ_ID = "dbo.ArchivedQuestions_ReadAllByQuizID";
        #endregion 
      
        #region Constructors
        /// <summary>
        /// Instantiates archivedQuestion <see cref="QuizEngine.Repository.ArchivedQuestionRepository"/>.
        /// </summary>
        public ArchivedQuestionRepository(RepositoryContext context)
            : base(context)
        {
        }
        #endregion 

        #region Methods
        
        /// <summary>
        /// ExecuteNonQuery from ArchivedQuestion Business with Stored Procedure ArchivedQuestions_InsertById
        /// </summary>
        /// <param name="questionID"></param>
        /// <param name="quizID"></param>
        public void Insert_ById(Guid questionID, Guid quizID, Int16 indexorder)
        {
            SqlParameter[] param =
                                    {
                                       new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier){ Value = questionID},
                                       new SqlParameter("@QuizGUID", SqlDbType.UniqueIdentifier){Value = quizID},
                                       new SqlParameter("@IndexOrder", SqlDbType.SmallInt){Value = indexorder}
                                    };
            ExecuteCommand(ARCHIVED_QUESTION_INSERT_BY_ID, param);
        }
 
        /// <summary>
        /// ExecuteNonQuery from ArchivedQuestion Business with Stored Procedure ArchivedQuestion_Delete.
        /// </summary>
        /// <param name="ArchivedQuestionID">ArchivedQuestionID</param>
        public void Delete(Guid archivedQuestionID)
        {
            SqlParameter archivedQuestionIDParam = new SqlParameter("@ArchivedQuestionGUID", SqlDbType.UniqueIdentifier)
            {
                Value = archivedQuestionID
            };

            ExecuteCommand(ARCHIVED_QUESTION_DELETE, archivedQuestionIDParam);
        }

        /// <summary>
        /// Read from ArchivedQuestion Business with Stored Procedure ArchivedQuestion_Select
        /// </summary>
        /// <param name="ArchivedQuestionID">ArchivedQuestionID</param>
        /// <returns></returns>
        public ArchivedQuestion ReadByID(Guid archivedQuestionID)
        {
            SqlParameter archivedQuestionIDParam = new SqlParameter("@ArchivedQuestionGUID", SqlDbType.UniqueIdentifier)
            {
                Value = archivedQuestionID
            };
            return ExecuteReadCommand(ARCHIVED_QUESTION_READ_BY_ID, ReaderToModel, archivedQuestionIDParam).SingleOrDefault();
        }
        /// <summary>
        /// Read from ArchivedQuestion all archived questions.
        /// </summary>
        /// <returns></returns>
        public List<ArchivedQuestion> ReadAll()
        {
            return ExecuteReadCommand(ARCHIVED_QUESTION_READ_ALL, ReaderToModel);
        }

        /// <summary>
        /// Reads and returns all the ArchivedQuestions related to a specified QuizID
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public List<ArchivedQuestion> ReadAllByQuizID(Guid quizID)
        {
            SqlParameter quizGUID = new SqlParameter("@QuizGUID", SqlDbType.UniqueIdentifier) { Value = quizID};
            return ExecuteReadCommand(ARCHIVED_QUESTION_READ_ALL_BY_QUIZ_ID, ReaderToModel, quizGUID);
        }

        protected override ArchivedQuestion ReaderToModel(SqlDataReader reader)
        {
            Guid levelGUID, categoryGUID;
            if (reader.IsDBNull(3))
            {
                levelGUID = Guid.Empty;
            }
            else
                levelGUID = reader.GetGuid(3);

            if (reader.IsDBNull(4))
            {
                categoryGUID = Guid.Empty;
            }
            else
                categoryGUID = reader.GetGuid(4);
            return new ArchivedQuestion
            {
                ArchivedQuestionID = reader.GetGuid(0),
                ArchivedQuestionText = reader.GetString(1),
                QuestionType = (QuestionType)reader.GetInt16(2),
                LevelID = levelGUID,
                CategoryID = categoryGUID,
                QuizID = reader.GetGuid(5)
            };
        }
        #endregion Methods

    }
}
