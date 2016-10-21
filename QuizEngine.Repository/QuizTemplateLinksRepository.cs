using QuizEngine.Model.Data;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QuizEngine.Repository
{
    public class QuizTemplateLinksRepository:BaseRepository<QuizTemplateLinks>
    {
        #region Constants
        private const string QUIZ_TEMPLATE_LINKS_INSERT     = "dbo.TemplateQuestionLinks_Insert";
        private const string QUIZ_TEMPLATE_LINKS_READ_ALL   = "dbo.TemplateQuestionLinks_ReadAll";
        private const string QUIZ_TEMPLATE_LINKS_READ_BY_ID = "dbo.TemplateQuestionLinks_ReadByQuizId";
        #endregion

        #region Constructors
        public QuizTemplateLinksRepository(RepositoryContext context): base(context)
        { }
        #endregion

        #region Metods
        /// <summary>
        /// Insert a SpecialQuizQuestionLinks object in the data base.
        /// </summary>
        /// <param name="specialQuizQuestionLinks"></param>
        public void Insert(QuizTemplateLinks specialQuizQuestionLinks)
        {
            SqlParameter quizUID = new SqlParameter("@TemplateQuizGUID", System.Data.SqlDbType.UniqueIdentifier)
            {
                Value = specialQuizQuestionLinks.QuizTemplateUID
            };

            SqlParameter questionUID = new SqlParameter("@QuestionGUID", System.Data.SqlDbType.UniqueIdentifier)
            {
                Value = specialQuizQuestionLinks.QuestionUID
            };

            ExecuteCommand(QUIZ_TEMPLATE_LINKS_INSERT,quizUID,questionUID);
        }

        /// <summary>
        /// Read All the SpecialQuizQuestionLinks from the data base.
        /// </summary>
        /// <returns></returns>
        public List<QuizTemplateLinks> ReadAll()
        {
            return ExecuteReadCommand(QUIZ_TEMPLATE_LINKS_READ_ALL, ReaderToModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quizUID"></param>
        /// <returns></returns>
        public List<QuizTemplateLinks> ReadByQuizId(Guid quizUID)
        {
            SqlParameter specialQuizGUID = new SqlParameter("@TemplateQuizGUID", System.Data.SqlDbType.UniqueIdentifier)
            {
                Value = quizUID
            };
            return ExecuteReadCommand(QUIZ_TEMPLATE_LINKS_READ_BY_ID,ReaderToModel,specialQuizGUID);
        }

        protected override QuizTemplateLinks ReaderToModel(SqlDataReader reader)
        {
            return new QuizTemplateLinks()
            {
                QuizTemplateUID = reader.GetGuid(0),
                QuestionUID = reader.GetGuid(1)
            };
        }

        #endregion
    }
}
