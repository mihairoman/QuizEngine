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
    public class QuestionRepository : BaseRepository<Question>
    {
        #region Constants
        private const string QUESTIONS_READBYID = "dbo.Questions_ReadByID";
        private const string QUESTIONS_READALL = "dbo.Questions_ReadAll";
        private const string QUESTIONS_INSERT = "dbo.Questions_Insert";
        private const string QUESTIONS_UPDATE = "dbo.Questions_Update";
        private const string QUESTIONS_DELETE = "dbo.Questions_Delete";
        private const string QUESTION_READ_BY_QUIZ_TEMPLATE_ID = "dbo.Questions_ReadByQuizTemplateID";
        #endregion
        
        #region Constructors
        public QuestionRepository(RepositoryContext context)
            : base(context)
        {
        }
        #endregion Constructors

        #region Methods
        public Question ReadByID(Guid questionID)
        {
            SqlParameter[] param = {
               new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier){Value = questionID}
            };

            return ExecuteReadCommand(QUESTIONS_READBYID, ReaderToModel, param).SingleOrDefault();
        }

        public List<Question> ReadAll()
        {
            return ExecuteReadCommand(QUESTIONS_READALL, ReaderToModel);
        }

        public void Insert(Question Question)
        {
            SqlParameter[] param = {
                new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier){Value = Question.QuestionGUID},
                new SqlParameter("@Text", SqlDbType.NVarChar, -1){Value = Question.Text},
                new SqlParameter("@QuestionType", SqlDbType.SmallInt){Value = (Int16)Question.QuestionType},
                new SqlParameter("@CategoryGUID", SqlDbType.UniqueIdentifier){Value = Question.CategoryGUID},
                new SqlParameter("@LevelGUID", SqlDbType.UniqueIdentifier){Value = Question.LevelGUID},
            };

            ExecuteCommand(QUESTIONS_INSERT, param);
        }

        public void Update(Question Question)
        {
            SqlParameter[] param = {
              new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier){Value = Question.QuestionGUID},
              new SqlParameter("@Text", SqlDbType.NVarChar, -1){Value = Question.Text},
              new SqlParameter("@QuestionType", SqlDbType.SmallInt){Value = (Int16)Question.QuestionType},
              new SqlParameter("@CategoryGUID", SqlDbType.UniqueIdentifier){Value = Question.CategoryGUID},
              new SqlParameter("@LevelGUID", SqlDbType.UniqueIdentifier){Value = Question.LevelGUID}
            };

            ExecuteCommand(QUESTIONS_UPDATE, param);
        }

        public void Delete(Question Question)
        {
            SqlParameter[] param = {
                new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier){Value = Question.QuestionGUID}
            };

            ExecuteCommand(QUESTIONS_DELETE, param);
        }

        public List<Question> ReadByQuizTemplateId(Guid quizTemplateUID)
        {
            SqlParameter quizTemplateGUID = new SqlParameter("@SpecialQuizGUID",System.Data.SqlDbType.UniqueIdentifier)
            {
                Value = quizTemplateUID
            };
            return ExecuteReadCommand(QUESTION_READ_BY_QUIZ_TEMPLATE_ID,ReaderToModel,quizTemplateGUID);
        }

        protected override Question ReaderToModel(SqlDataReader reader)
        {
            return new Question
            {
                QuestionGUID = reader.GetGuid(0),
                Text = reader.GetString(1),
                QuestionType = (QuestionType)reader.GetInt16(2),
                CategoryGUID = reader.GetGuid(3),
                LevelGUID = reader.GetGuid(4)
            };
        }
        #endregion Methods
    }
}
