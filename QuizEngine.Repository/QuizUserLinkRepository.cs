using QuizEngine.Model.Data;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QuizEngine.Repository
{
    public  class QuizUserLinkRepository: BaseRepository<QuizUserLink>
    {
        #region Constants
        private const string QUIZ_USER_LINK_INSERT                   = "dbo.QuizUserLink_Insert";
        private const string QUIZ_USER_LINK_UPDATE                   = "dbo.QuizUserLinks_Update";
        private const string QUIZ_USER_LINK_UPDATE_FINAL_RESULT      = "dbo.QuizUserLinks_UpdateFinalResult";
        private const string QUIZ_USER_LINK_READ_ALL                 = "dbo.QuizUserLinks_ReadAll";
        private const string QUIZ_USER_LINK_READ_BY_QUIZ_ID          = "dbo.QuizUserLinks_ReadByQuizId";
        private const string QUIZ_USER_LINK_READ_BY_USER_ID          = "dbo.QuizUserLinks_ReadByUserId";
        private const string QUIZ_USER_LINK_INSERT_FOR_GENERATING_NEW_LINK = "dbo.QuizUserLink_InsertForGeneratingNewLink";
        #endregion

        #region Constructors
        public QuizUserLinkRepository(RepositoryContext context)
            : base(context)
        { }
        #endregion

        #region Methods
        /// <summary>
        /// Insert a QuizUserLink object into database.
        /// </summary>
        /// <param name="quizUserLink"></param>
        public void Insert(QuizUserLink quizUserLink)
        {
            SqlParameter quizGuid = new SqlParameter("@QuizGUID", System.Data.SqlDbType.UniqueIdentifier);
            quizGuid.Value = quizUserLink.QuizUID;

            SqlParameter userGuid = new SqlParameter("@UserGUID", System.Data.SqlDbType.UniqueIdentifier);
            userGuid.Value = quizUserLink.UserUID;

            SqlParameter result = new SqlParameter("@Result", System.Data.SqlDbType.Decimal);
            if (quizUserLink.Result == null)
            {
                result.Value = DBNull.Value;
            }
            else
            {
                result.Value = quizUserLink.Result;
            }
            result.Value = quizUserLink.Result;

            SqlParameter dateTime = new SqlParameter("@QuizDate", System.Data.SqlDbType.DateTime); //ask somebody if it's correct
            dateTime.Value = quizUserLink.QuizDate;

            SqlParameter onlineOrDownloaded = new SqlParameter("@OnlineOrDownloaded", System.Data.SqlDbType.Bit);
            onlineOrDownloaded.Value = quizUserLink.OnlineOrDownloaded;

            SqlParameter isTaken = new SqlParameter("@IsTaken",System.Data.SqlDbType.Bit);
            if (quizUserLink.IsTaken == null)
            {
                isTaken.Value = DBNull.Value;
            }
            else
            {
                isTaken.Value = quizUserLink.IsTaken;
            }

            ExecuteCommand(QUIZ_USER_LINK_INSERT, quizGuid, userGuid, result, dateTime, onlineOrDownloaded,isTaken);
          
        }


        /// <summary>
        /// Insert a QuizUserLink object into database.
        /// </summary>
        /// <param name="quizUserLink"></param>
        public void InsertForGeneratingNewLink(QuizUserLink quizUserLink)
        {
            SqlParameter quizGuid = new SqlParameter("@QuizGUID", System.Data.SqlDbType.UniqueIdentifier);
            quizGuid.Value = quizUserLink.QuizUID;

            SqlParameter userGuid = new SqlParameter("@UserGUID", System.Data.SqlDbType.UniqueIdentifier);
            userGuid.Value = quizUserLink.UserUID;

            SqlParameter result = new SqlParameter("@Result", System.Data.SqlDbType.Decimal);
            if (quizUserLink.Result == null)
            {
                result.Value = DBNull.Value;
            }
            else
            {
                result.Value = quizUserLink.Result;
            }
            result.Value = quizUserLink.Result;

            SqlParameter dateTime = new SqlParameter("@QuizDate", System.Data.SqlDbType.DateTime); //ask somebody if it's correct
            dateTime.Value = quizUserLink.QuizDate;

            SqlParameter onlineOrDownloaded = new SqlParameter("@OnlineOrDownloaded", System.Data.SqlDbType.Bit);
            onlineOrDownloaded.Value = quizUserLink.OnlineOrDownloaded;

            SqlParameter isTaken = new SqlParameter("@IsTaken", System.Data.SqlDbType.Bit);
            if (quizUserLink.IsTaken == null)
            {
                isTaken.Value = DBNull.Value;
            }
            else
            {
                isTaken.Value = quizUserLink.IsTaken;
            }

            ExecuteCommand(QUIZ_USER_LINK_INSERT_FOR_GENERATING_NEW_LINK, quizGuid, userGuid, result, dateTime, onlineOrDownloaded, isTaken);

        }

        /// <summary>
        /// Update the QuizUserLink object recived as parameter. 
        /// </summary>
        /// <param name="quizUserLink"></param>
        public void Update(QuizUserLink quizUserLink)
        {
            SqlParameter quizGuid = new SqlParameter("@QuizGUID", System.Data.SqlDbType.UniqueIdentifier);
            quizGuid.Value = quizUserLink.QuizUID;

            SqlParameter userGuid = new SqlParameter("@UserGUID", System.Data.SqlDbType.UniqueIdentifier);
            userGuid.Value = quizUserLink.UserUID;

            SqlParameter result = new SqlParameter("@Result", System.Data.SqlDbType.Decimal);
            if (quizUserLink.Result == null)
            {
                result.Value = DBNull.Value;
            }
            else
            {
                result.Value = quizUserLink.Result;
            }
            result.Value = quizUserLink.Result;

            SqlParameter dateTime = new SqlParameter("@QuizDate", System.Data.SqlDbType.DateTime); //ask somebody if it's correct
            dateTime.Value = quizUserLink.QuizDate;

            SqlParameter onlineOrDownloanded = new SqlParameter("@OnlineOrDownloaded", System.Data.SqlDbType.Bit);
            onlineOrDownloanded.Value = quizUserLink.OnlineOrDownloaded;

            SqlParameter isTaken = new SqlParameter("@IsTaken", System.Data.SqlDbType.Bit);
            if (quizUserLink.IsTaken == null)
            {
                isTaken.Value = DBNull.Value;
            }
            else
            {
                isTaken.Value = quizUserLink.IsTaken;
            }
            
            ExecuteCommand(QUIZ_USER_LINK_UPDATE, quizGuid, userGuid, result, dateTime, onlineOrDownloanded,isTaken);
        }
        /// <summary>
        /// UpdateGrade the QuizUserLink object recived as parameter. 
        /// </summary>
        /// <param name="quizUserLink"></param>
        public void UpdateFinalResult(QuizUserLink quizUserLink)
        {
            SqlParameter quizGuid = new SqlParameter("@QuizGUID", System.Data.SqlDbType.UniqueIdentifier);
            quizGuid.Value = quizUserLink.QuizUID;

            SqlParameter userGuid = new SqlParameter("@UserGUID", System.Data.SqlDbType.UniqueIdentifier);
            userGuid.Value = quizUserLink.UserUID;

            SqlParameter result = new SqlParameter("@Result", System.Data.SqlDbType.Decimal);
            if (quizUserLink.Result == null)
            {
                result.Value = DBNull.Value;
            }
            else
            {
                result.Value = quizUserLink.Result;
            }
            result.Value = quizUserLink.Result;
            ExecuteCommand(QUIZ_USER_LINK_UPDATE_FINAL_RESULT, quizGuid, userGuid, result);
        }

        /// <summary>
        /// Return all the QuizUserLink objects from the database.
        /// </summary>
        /// <returns></returns>
        public List<QuizUserLink> ReadAll()
        {
            return ExecuteReadCommand(QUIZ_USER_LINK_READ_ALL, ReaderToModel);
        }

        /// <summary>
        /// Read all the QuizUserLink objects having QuizUID specified in the method parametert.
        /// </summary>
        /// <param name="quizUID"></param>
        /// <returns>A List of QuizUserLink objects</returns>
        public List<QuizUserLink> ReadByQuizId(Guid quizUID)
        {
            SqlParameter quizGuid = new SqlParameter("@QuizGUID", System.Data.SqlDbType.UniqueIdentifier);
            quizGuid.Value =quizUID;
            return ExecuteReadCommand(QUIZ_USER_LINK_READ_BY_QUIZ_ID,ReaderToModel, quizGuid);
        }

        /// <summary>
        /// Read all the QuizUserLink objects having UserUID specified in the method parametert.
        /// </summary>
        /// <param name="quizUID"></param>
        /// <returns>A List of QuizUserLink objects</returns>
        public List<QuizUserLink> ReadByUserId(Guid userUID)
        {
            SqlParameter userGuid = new SqlParameter("@UserGUID", System.Data.SqlDbType.UniqueIdentifier);
            userGuid.Value = userUID;
            return ExecuteReadCommand(QUIZ_USER_LINK_READ_BY_USER_ID,ReaderToModel ,userGuid);
        }

        protected override QuizUserLink ReaderToModel(SqlDataReader reader)
        {
          decimal? result;
           if(reader.IsDBNull(3))
           {
               result=null;
           }
           else
           {
               result = reader.GetDecimal(2);
           }

           bool? isTaken;
           if (reader.IsDBNull(5))
           {
               isTaken = null;
           }
           else
           {
               isTaken = reader.GetBoolean(5);
           }
     
            QuizUserLink q = new QuizUserLink()
            {
                QuizUID = reader.GetGuid(0),
                UserUID = reader.GetGuid(1),
                Result = reader.GetDecimal(2),
                QuizDate = reader.GetDateTime(3),
                OnlineOrDownloaded = reader.GetBoolean(4),
                IsTaken = isTaken
            };
           return q;
        }
#endregion
    }
}
