using QuizEngine.Model.View;
using QuizEngine.Repository.Core;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QuizEngine.Repository
{
    public class CategoryViewRepository : BaseRepository<CategoryView>
    {
        #region Constants
        private const string CATEGORIES_COUNTUSINGQUESTIONS = "dbo.Categories_CountUsingQuestions";
        #endregion Constants

        #region Constructors
        public CategoryViewRepository(RepositoryContext context): base(context)
        {

        }
        #endregion Constructors

        #region Methods
        public List<CategoryView> ReadAll()
        {
            return ExecuteReadCommand(CATEGORIES_COUNTUSINGQUESTIONS, ReaderToModel);
        }

        protected override CategoryView ReaderToModel(SqlDataReader reader)
        {
            return new CategoryView
            {
                CategoryUID = reader.GetGuid(0),
                CategoryName = reader.GetString(1),
                NumberOfUsingQuestions = reader.GetInt32(2)
            };
        }
        #endregion Methods
    }
}
