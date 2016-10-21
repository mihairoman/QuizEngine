using QuizEngine.Model.View;
using QuizEngine.Repository.Core;
using System.Collections.Generic;
namespace QuizEngine.Repository
{
    public class QuizUserViewRepository:BaseRepository<QuizUserView>
    {
        #region Constant
        private const string QUIZ_USER_VIEW_READ_ALL = "dbo.QuizUserPreview_ReadAll";
        #endregion

        #region Constructors
        public QuizUserViewRepository(RepositoryContext context): base(context)
        { }
        #endregion

        #region Methods
        /// <summary>
        /// Reads All the QuizUserView object from the db.
        /// </summary>
        /// <returns></returns>
        public List<QuizUserView> ReadAll()
        {
            return ExecuteReadCommand(QUIZ_USER_VIEW_READ_ALL, ReaderToModel);
        }

        protected override QuizUserView ReaderToModel(System.Data.SqlClient.SqlDataReader reader)
        {
            return new QuizUserView()
            {
                QuizUID = reader.GetGuid(0),
                LevelName = reader.GetString(1),
                CategoryName = reader.GetString(2),
                QuestionNumber = reader.GetInt32(3)
            };
        }
        #endregion
    }
}
