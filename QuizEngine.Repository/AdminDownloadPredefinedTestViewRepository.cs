using QuizEngine.Model.View;
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
    public class AdminDownloadPredefinedTestViewRepository: BaseRepository<AdminDownloadPredefinedTestView>
    {
        
        #region Constants
        private const string GENERATED_TESTS_READ_ALL_PREDEFINED = "dbo.GeneratedTests_ReadAllPredefined";
        #endregion Constants

        #region Contructor
        public AdminDownloadPredefinedTestViewRepository(RepositoryContext context)
            : base(context)
        {

        }
        #endregion Contructor

        #region Methods
        public List<AdminDownloadPredefinedTestView> ReadAllPredefinedTests(string sortExpression, int rowsPerPage, int pageNumber)
        {
            SqlParameter[] param =
                                    {
                                      new SqlParameter("@SortExpression", SqlDbType.NVarChar){ Value = sortExpression},
                                      new SqlParameter("@FoundrowPerPage", SqlDbType.Int){Value = rowsPerPage},
                                      new SqlParameter("@FoundPageNumber", SqlDbType.Int){Value = pageNumber}
                                    };
            return ExecuteReadCommand(GENERATED_TESTS_READ_ALL_PREDEFINED, ReaderToModel,param);
        }
        protected override AdminDownloadPredefinedTestView ReaderToModel(System.Data.SqlClient.SqlDataReader reader)
        {
            return new AdminDownloadPredefinedTestView()
            {
                QuizUID =reader.GetGuid(0),
                TypeName=reader.GetString(1),
            };
        }
       
        #endregion Methods
    }
}
