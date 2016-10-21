using QuizEngine.Model.View;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuizEngine.AssemblyResources;

namespace QuizEngine.Repository
{
    [Obsolete("Please use the QuizView entity")]
    public class QuizzesPerUserViewRepository: BaseRepository<QuizesPerUserView>
    {
        #region Constants
        private const string QUIZBYUSER_READALLVIEW = "dbo.QuizesPerUserView_ReadAll";
        #endregion

        #region Constructor
        public QuizzesPerUserViewRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public List<QuizesPerUserView> ReadAllQuizesPerUserView(Guid userUID)
        {
            SqlParameter userParam = new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier)
            {
                Value = userUID
            };
            return ExecuteReadCommand(QUIZBYUSER_READALLVIEW, ReaderToModel, userParam);
        }

        protected override QuizesPerUserView ReaderToModel(SqlDataReader reader)
        {
            string taken;
            if (reader.IsDBNull(6))
            {
                taken = "";
            }
            else
                taken = reader.GetBoolean(6) ? RepositoryResources.Taken : String.Format(RepositoryResources.NotTakenLink, reader.GetGuid(0));
            decimal result;
            if (reader.IsDBNull(5))
            {
                result = -1;
            }
            else
                result = reader.GetDecimal(5);

            string value;
            if (result == -1)
                if (taken.Equals(RepositoryResources.Taken))
                    value = RepositoryResources.NotYetCorrected;
                else
                    value = "";
            else
                value = result.ToString();
                     
            return new QuizesPerUserView()
            {
                QuizUID = reader.GetGuid(0),
                QuizDate = reader.GetDateTime(1),
                CategoryName = reader.GetString(2),
                LevelName = reader.GetString(3),
                OnlineOrDownloaded = reader.GetBoolean(4) ? RepositoryResources.Online : RepositoryResources.Downloaded,
                TakenOrNot = taken,
                Result = value
            };
        }
        #endregion Methods
    }
}
