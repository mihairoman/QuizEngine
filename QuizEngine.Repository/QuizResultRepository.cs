using QuizEngine.Model.View;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuizEngine.Repository
{
    /// <summary>
    /// Class that collect all the question results of a quiz
    /// </summary>
    public class QuizResultRepository : BaseRepository<QuizResultView>
    {
        #region Constants
        /// <summary>
        /// Name of the stored procedure that collect the results of the questions of a quiz
        /// </summary>
        private const string QUIZ_RESULT_VIEW_READ_RESULT = "QuizResultView_ReadResult";
        #endregion Constants

        #region Constructors
        public QuizResultRepository(RepositoryContext context) : base(context) { }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Method that collects all the results of questions base on a user and quiz id
        /// </summary>
        /// <param name="quizUID">the id of the quiz</param>
        /// <param name="userUID">the id of the user</param>
        /// <returns></returns>
        public List<QuizResultView> ReadResultsPerQuestion(Guid quizUID, Guid userUID)
        {
            SqlParameter[] param = {
                new SqlParameter("@QuizGUID", SqlDbType.UniqueIdentifier){Value = quizUID},
                new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier){Value = userUID}
            };

            List<QuizResultView> quizViewResultList = ExecuteReadCommand(QUIZ_RESULT_VIEW_READ_RESULT, ReaderToModel, param);
            if (quizViewResultList.Count > 0)
            {
                return quizViewResultList;
            }
            return default(List<QuizResultView>);
        }

        /// <summary>
        /// Transforms a dataset into a view
        /// </summary>
        /// <param name="reader">SqlDataReader</param>
        /// <returns></returns>
        protected override QuizResultView ReaderToModel(SqlDataReader reader)
        {
            if (reader != null)
            {
                return new QuizResultView
                {
                    QuizUID = reader.GetGuid(0),
                    ArchivedQuestionUID = reader.GetGuid(1),
                    Result = Convert.ToDouble(reader.GetDecimal(2))
                };
            }
            return null;
        }
        #endregion Methods
    }
}
