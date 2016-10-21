using QuizEngine.Model.Data.View;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Repository
{
    public class ValidationFreeTextQuestionsViewRepository : BaseRepository<ValidationFreeTextQuestionsView>
    {
        #region Constants
        private const string FREE_TEXT_QUESTIONS_VIEW_READ_ALL = "dbo.FreeTextQuestionsView_ReadAll";
        #endregion Constants

        #region Contructor
        public ValidationFreeTextQuestionsViewRepository(RepositoryContext context)
            : base(context)
        {

        }
        #endregion Contructor

        #region Methods
        public List<ValidationFreeTextQuestionsView> ReadFreeTextQuestions(Guid quizUID, Guid userUID)
        {
            SqlParameter[] param ={
                                      new SqlParameter("@QuizGUID", SqlDbType.UniqueIdentifier){Value = quizUID},
                                      new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier){Value = userUID}
                                  };
            return ExecuteReadCommand(FREE_TEXT_QUESTIONS_VIEW_READ_ALL, ReaderToModel, param);
        }
        protected override ValidationFreeTextQuestionsView ReaderToModel(System.Data.SqlClient.SqlDataReader reader)
        {
            return new ValidationFreeTextQuestionsView
            {
                QuizUID =reader.GetGuid(0),
                UserUID=reader.GetGuid(1),
                ArchivedQuestionUID=reader.GetGuid(2),
                ArchivedChoiceUID=reader.GetGuid(3),
                QuestionType=reader.GetInt16(4),
                QuestionText =reader.GetString(5),
                UserAnswerText= reader.GetString(6),
                Grade=reader.GetDecimal(7)
            };
        }
       
        #endregion Methods
    }
    
}
