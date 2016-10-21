using QuizEngine.Model.Data.View;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace QuizEngine.Repository
{
    public class QuizTemplateViewRepository:BaseRepository<QuizTemplateView>
    {
        #region Constant
        private const string QUIZ_TEMPLATE_VIEW_READ_ALL = "dbo.QuizTemplateView_Read";
        #endregion

        #region Constructors
        public QuizTemplateViewRepository(RepositoryContext context):base(context)
        { }
        #endregion

        #region Methods
        public List<QuizTemplateView> Read(Guid userID,int? pageNumber = null, int? rowPerPage = null, string sortExpression =null)
        {

            SqlParameter[] parameters = {
            new SqlParameter("@userGUID", System.Data.SqlDbType.UniqueIdentifier){Value = userID},
            new SqlParameter("@PageNumber", System.Data.SqlDbType.Int){Value = pageNumber},
            new SqlParameter("@RowPerPage", System.Data.SqlDbType.Int){Value = rowPerPage},
            new SqlParameter("@SortExpression", System.Data.SqlDbType.NVarChar){Value = sortExpression}
            
            };
            return ExecuteReadCommand(QUIZ_TEMPLATE_VIEW_READ_ALL,ReaderToModel,parameters);
        }

        protected override QuizTemplateView ReaderToModel(System.Data.SqlClient.SqlDataReader reader)
        {
            return new QuizTemplateView()
            {
                QuizTemplateUID = reader.GetGuid(0),
                QuizTemplateName = reader.GetString(1),
                QuestionNumber = reader.GetInt32(2)
            };
        }
        #endregion
    }
}
