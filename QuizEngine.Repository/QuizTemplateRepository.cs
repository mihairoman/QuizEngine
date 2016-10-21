using QuizEngine.Model.Data;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace QuizEngine.Repository
{
    public class QuizTemplateRepository:BaseRepository<QuizTemplates>
    {
        #region Constants
        private const string QUIZ_TEMPLATE_INSERT = "dbo.QuizTemplates_Insert";
        private const string QUIZ_TEMPLATE_READ_BY_ID = "dbo.QuizTemplates_ReadById";
        private const string QUIZ_TEMPLATE_READ_ALL = "dbo.QuizTemplates_ReadAll";
        private const string QUIZ_TEMPLATE_COUNT = "dbo.QuizTemplate_Count";
        private const string GENERATED_TESTS_READ_ALL_PREDEFINED = "dbo.GeneratedTests_ReadAllPredefined";
        #endregion

        #region Constructors
        public QuizTemplateRepository(RepositoryContext context)
            : base(context) { }
        #endregion

        #region Methods
        /// <summary>
        /// Insert a SpecialQuiz object in the database.
        /// </summary>
        /// <param name="quizTemplate"></param>
        public void Insert(QuizTemplates quizTemplate)
        {
            SqlParameter specialQuizUID = new SqlParameter("@QuizTemplateGUID", System.Data.SqlDbType.UniqueIdentifier)
            {
                Value = quizTemplate.QuizTemplateUID
            };

            SqlParameter typeName = new SqlParameter("@TypeName", System.Data.SqlDbType.NVarChar)
            {
                Value = quizTemplate.TypeName
            };

            SqlParameter time = new SqlParameter("@Time", System.Data.SqlDbType.Time)
            {
                Value = quizTemplate.Time
            };


            ExecuteCommand(QUIZ_TEMPLATE_INSERT, specialQuizUID, typeName, time);

        }

        /// <summary>
        /// Return All 
        /// </summary>
        /// <param name="specialQuizUID"></param>
        /// <returns></returns>
        public QuizTemplates ReadById(Guid specialQuizUID)
        {
            SqlParameter specialQuizGUID = new SqlParameter("@QuizTemplateGUID", System.Data.SqlDbType.UniqueIdentifier)
            {
                Value = specialQuizUID
            };

            return ExecuteReadCommand(QUIZ_TEMPLATE_READ_BY_ID,ReaderToModel,specialQuizGUID).SingleOrDefault();
        }

        public List<QuizTemplates> ReadAll()
        {
            return ExecuteReadCommand(QUIZ_TEMPLATE_READ_ALL,ReaderToModel);
        }

        public List<QuizTemplates> ReadAllPredefinedTests()
        {
            return ExecuteReadCommand(GENERATED_TESTS_READ_ALL_PREDEFINED, ReaderToModel);
        }
        public int Count(Guid _userGuid)
        {
            SqlParameter userGuid = new SqlParameter("@userGUID", System.Data.SqlDbType.UniqueIdentifier) { Value = _userGuid};
            return  Convert.ToInt32(ExecuteScalarCommand(QUIZ_TEMPLATE_COUNT,userGuid));
        }

        protected override QuizTemplates ReaderToModel(SqlDataReader reader)
        {
            TimeSpan? time;
            if (reader.IsDBNull(2))
            {
                time = null;
            }
            else
            {
                time = reader.GetTimeSpan(2);
            }
            return new QuizTemplates()
            {
                QuizTemplateUID = reader.GetGuid(0),
                TypeName = reader.GetString(1),
                Time = time
            };
        }
        #endregion
    }
}
