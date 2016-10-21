using QuizEngine.Model.Data;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QuizEngine.Repository
{
    /// <summary>
    /// Class CategoryRepository
    /// </summary>
    public class CategoryRepository : BaseRepository<Category>
    {
        #region Constants
        private const string CATEGORIES_READBYID = "dbo.Categories_ReadByID";
        private const string CATEGORIES_READALL = "dbo.Categories_ReadAll";
        private const string CATEGORIES_INSERT = "dbo.Categories_Insert";
        private const string CATEGORIES_UPDATE = "dbo.Categories_Update";
        private const string CATEGORIES_CANBEDELETED = "dbo.Categories_CanBeDeleted";
        private const string CATEGORIES_DELETE = "dbo.Categories_Delete";
        #endregion Constants

        #region Constructors
        /// <summary>
        /// Instantiates a <see cref="QuizEngine.Repository.CategoryRepository"/>.
        /// </summary>
        public CategoryRepository(RepositoryContext context)
            : base(context)
        {

        }
        #endregion Constructors

        #region Methods
        public Category ReadByID(Guid categoryUID)
        {
            SqlParameter categoryUIDParam = new SqlParameter("@CategoryGUID", SqlDbType.UniqueIdentifier)
            {
                Value = categoryUID
            };
            return ExecuteReadCommand(CATEGORIES_READBYID, ReaderToModel, categoryUIDParam).SingleOrDefault();
        }

        public List<Category> ReadAll()
        {
            return ExecuteReadCommand(CATEGORIES_READALL, ReaderToModel);
        }

        public void Insert(Category category)
        {
            SqlParameter[] param = {
                new SqlParameter("@CategoryGUID", SqlDbType.UniqueIdentifier){Value = category.CategoryUID},
                new SqlParameter("@CategoryName", SqlDbType.NVarChar, 50){Value = category.CategoryName}
            };
            ExecuteCommand(CATEGORIES_INSERT, param);
        }

        public void Update(Category category)
        {
            SqlParameter[] param = {
                new SqlParameter("@CategoryGUID", SqlDbType.UniqueIdentifier){Value = category.CategoryUID},
                new SqlParameter("@CategoryName", SqlDbType.NVarChar, 50){Value = category.CategoryName}
            };
            ExecuteCommand(CATEGORIES_UPDATE, param);
        }

        public void Delete(Category category)
        {
            SqlParameter[] param = {
                new SqlParameter("@CategoryGUID", SqlDbType.UniqueIdentifier){Value = category.CategoryUID}
            };
            if (ExecuteCheckCommand(CATEGORIES_CANBEDELETED, param))
            {
                param = new SqlParameter[1];
                param[0] = new SqlParameter("@CategoryGUID", SqlDbType.UniqueIdentifier) { Value = category.CategoryUID };
                ExecuteCommand(CATEGORIES_DELETE, param);
            }
            else
                throw new Exception("This category cannot be deleted.");
        }

        protected override Category ReaderToModel(SqlDataReader reader)
        {
            return new Category
            {
                CategoryUID = reader.GetGuid(0),
                CategoryName = reader.GetString(1)
            };
        }
        #endregion Methods
    }
}
