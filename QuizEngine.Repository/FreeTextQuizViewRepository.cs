using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using QuizEngine.Model.View;
using QuizEngine.Repository.Core;


namespace QuizEngine.Repository
{
    public class FreeTextQuizViewRepository : BaseRepository<FreeTextQuizView>
    {
        
        #region Constants
        private const string FREE_TEXT_QUIZ_VIEW_READ_ALL = "dbo.FreeTextQuizView_ReadAll";
        private const string FREE_TEXT_QUIZ_VIEW_COUNT_ALL_FILTERED_QUIZZES = "dbo.FreeTextQuizView_CountAllFilteredQuizzes";
        #endregion

        #region Constructor
        public FreeTextQuizViewRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public List<FreeTextQuizView> ReadAll(string sortExpression, int rowsPerPage, int pageNumber)
        {
           
            SqlParameter[] param =
                                    {
                                      new SqlParameter("@SortExpression", SqlDbType.NVarChar){ Value = sortExpression},
                                      new SqlParameter("@FoundrowPerPage", SqlDbType.Int){Value = rowsPerPage},
                                      new SqlParameter("@FoundPageNumber", SqlDbType.Int){Value = pageNumber}
                                    };
            return ExecuteReadCommand(FREE_TEXT_QUIZ_VIEW_READ_ALL, ReaderToModel, param);
        }
         public int CountAllFilteredQuizzes()
        {
             return (int)ExecuteScalarCommand(FREE_TEXT_QUIZ_VIEW_COUNT_ALL_FILTERED_QUIZZES);
        }
        
        protected override FreeTextQuizView ReaderToModel(SqlDataReader reader)
        {
           
            return new FreeTextQuizView()
            {   
                UserUID = reader.GetGuid(0),
                QuizUID = reader.GetGuid(1),
                UserName = reader.GetString(2),
                QuizDate = reader.GetDateTime(3),
                CategoryName = reader.GetString(4),
                LevelName = reader.GetString(5)

         
            };
        }
        #endregion
    }
}
