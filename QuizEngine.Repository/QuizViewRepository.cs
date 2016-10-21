using QuizEngine.Model.View;
using QuizEngine.Repository.Core;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace QuizEngine.Repository
{
    public class QuizViewRepository : BaseRepository<QuizView>
    {
        #region Constants
        private const string QUIZ_VIEW_READ_ALL = "dbo.QuizView_Read";
        private const string QUIZ_VIEW_COUNT    = "dbo.QuizView_Count";
        #endregion

        #region Constructor
        public QuizViewRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        /// <summary>
        /// Read a List of Quiz View objects from the data base.
        /// If both pageNumber and rowPerPage parameters are specified then it return only a part of the result pool.
        /// If the UserGuid parameter is specified it return only the QuizView objects belonging to the user having the id specified as parameter.
        /// The sort expression is used to return the result sorted by the followings:
        /// dbo.Users.Username , dbo.Quizes.QuizGUID , dbo.QuizUserLinks.Result , dbo.Categories.CategoryName , dbo.Levels.LevelName,
        /// QuizDate , OnlineOrDownloaded , IsTaken, dbo.Levels.Difficulty;
        /// </summary>
        /// <param name="pageNumber">Specify the page number from a table</param>
        /// <param name="rowPerPage">Specify how many rows are on a page</param>
        /// <param name="sortExpression">The property/coloum after the result will be sorted</param>
        /// <param name="UserGuid"><id of the user/param>
        /// <returns>Alist of QuizView objects</returns>
        public List<QuizView> ReadAll(int? pageNumber = null,int? rowPerPage =null, string sortExpression = null, Guid? UserGuid = null)
        {
            SqlParameter[] parameters =
            {
               new SqlParameter("@PageNumber",SqlDbType.Int){Value =  pageNumber},
               new SqlParameter("@RowPerPage",SqlDbType.Int){Value =  rowPerPage},
               new SqlParameter("@SortExpression",SqlDbType.NVarChar){Value = sortExpression},
               new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier){Value = UserGuid}
            };
            return ExecuteReadCommand(QUIZ_VIEW_READ_ALL, ReaderToModel,parameters);
        }

        public int Count(Guid? UserGuid = null)
        {
            if (UserGuid == null)
            {
                return  Convert.ToInt32(ExecuteScalarCommand(QUIZ_VIEW_COUNT));
            }
            else
            {
                SqlParameter param = new SqlParameter("@UserGUID",SqlDbType.UniqueIdentifier){Value = UserGuid};
                return Convert.ToInt32(ExecuteScalarCommand(QUIZ_VIEW_COUNT, param));
            }
        }

        protected override QuizView ReaderToModel(SqlDataReader reader)
        {
            decimal? resultIntermediate;
            if (reader.IsDBNull(2))
            {
                resultIntermediate = null;
            }
            else
            {
                resultIntermediate = reader.GetDecimal(2);
            }

            bool? isTaken;
            if (reader.IsDBNull(8))
            {
                isTaken = null;
            }
            else
            {
                isTaken = reader.GetBoolean(8);
            }
            TimeSpan? time;
            if (reader.IsDBNull(10))
            {
                time = null;
            }
            else
            {
                time = reader.GetTimeSpan(10);
            }

            return new QuizView()
            {
                UserName = reader.GetString(0),
                QuizUID = reader.GetGuid(1),
                Result = resultIntermediate,
                CategoryName = reader.GetString(3),
                LevelName = reader.GetString(4),
                UserUID = reader.GetGuid(5),
                QuizDate = reader.GetDateTime(6),
                OnlineOrDownloanded  = reader.GetBoolean(7),
                IsTaken = isTaken,
                LevelDifficulty = reader.GetInt16(9),
                Time = time
            };
        }
        #endregion
    }
}
