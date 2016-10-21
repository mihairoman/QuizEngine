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
    public class AdminDownloadGeneratedTestRepository : BaseRepository<AdminDownloadGeneratedTestView>
    {
        #region Constants
        private const string GENERATED_TESTS_READ_ALL_RANDOM = "dbo.GeneratedTests_ReadAllRandom";
        #endregion Constants

        #region Contructor
        public AdminDownloadGeneratedTestRepository(RepositoryContext context)
            : base(context)
        {

        }
        #endregion Contructor

        #region Methods
        public List<AdminDownloadGeneratedTestView> ReadAllGeneratedTests(string sortExpression, int rowsPerPage, int pageNumber)
        {
            SqlParameter[] param =
                                    {
                                      new SqlParameter("@SortExpression", SqlDbType.NVarChar){ Value = sortExpression},
                                      new SqlParameter("@FoundrowPerPage", SqlDbType.Int){Value = rowsPerPage},
                                      new SqlParameter("@FoundPageNumber", SqlDbType.Int){Value = pageNumber}
                                    };
            return ExecuteReadCommand(GENERATED_TESTS_READ_ALL_RANDOM, ReaderToModel,param);
        }
        protected override AdminDownloadGeneratedTestView ReaderToModel(System.Data.SqlClient.SqlDataReader reader)
        {
            return new AdminDownloadGeneratedTestView
            {
                QuizUID =reader.GetGuid(0),
                LevelUID=reader.GetGuid(1),
                CategoryUID=reader.GetGuid(2),
                LevelName=reader.GetString(3),
                LevelDifficulty=reader.GetInt16(4),
                CategoryName =reader.GetString(5),
            };
        }
       
        #endregion Methods
    }
}
