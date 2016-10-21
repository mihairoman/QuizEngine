using QuizEngine.Model.Data;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace QuizEngine.Repository
{
    public class QuizRepository:BaseRepository<Quiz>
    {
        #region Constants
        private const string QUIZ_READ_ALL      = "dbo.Quiz_ReadAll";
        private const string QUIZ_READ_BY_ID    = "dbo.Quiz_ReadById";
        private const string QUIZ_INSERT        = "dbo.Quiz_Insert";
        private const string QUIZ_DELETE        = "dbo.Quiz_Delete";
        private const string QUIZ_UPDATE        = "dbo.Quiz_Update";
        private const string QUIZ_COUNT_ALL_RANDOM     = "dbo.Quiz_CountAllRandom";
        private const string QUIZ_COUNT_ALL_PREDEFINED = "dbo.Quiz_CountAllPredefined";
       
        #endregion

        #region Constructors
        public QuizRepository(RepositoryContext context)
            : base(context)
        { }
        #endregion

        #region Methods
        /// <summary>
        /// Return all the Quiz objects from the database.
        /// </summary>
        /// <returns>A List of Quiz objects</returns>
        public List<Quiz> ReadAll()
        {
            return ExecuteReadCommand(QUIZ_READ_ALL,ReaderToModel);
        }

        public int CountAllRandom()
        {
            return (int)ExecuteScalarCommand(QUIZ_COUNT_ALL_RANDOM);
        }
        public int CountAllPredefined()
        {
            return (int)ExecuteScalarCommand(QUIZ_COUNT_ALL_PREDEFINED);
        }
        /// <summary>
        /// Return one Quiz object which has the uniqueidentifier equal with the method parameter.
        /// </summary>
        /// <param name="quizGUID"></param>
        /// <returns>A Quiz object</returns>
        public Quiz ReadById(Guid quizUID)
        {
            SqlParameter quizGuid = new SqlParameter("@QuizGUID", System.Data.SqlDbType.UniqueIdentifier);
            quizGuid.Value = quizUID;
            return ExecuteReadCommand(QUIZ_READ_BY_ID, ReaderToModel, quizGuid).SingleOrDefault();
        }

        /// <summary>
        /// Add a Quiz object to the database.
        /// </summary>
        /// <param name="quiz"></param>
        public void Insert(Quiz quiz)
        {
                SqlParameter quizGuid = new SqlParameter("@QuizGUID", System.Data.SqlDbType.UniqueIdentifier);
                quizGuid.Value = quiz.QuizUID;

                SqlParameter levelGuid = new SqlParameter("@LevelGUID", System.Data.SqlDbType.UniqueIdentifier);
                levelGuid.Value = quiz.LevelUID;

                SqlParameter categoryGuid = new SqlParameter("@CategoryGUID", System.Data.SqlDbType.UniqueIdentifier);
                categoryGuid.Value = quiz.CategoryUID;

                SqlParameter time = new SqlParameter("@Time",System.Data.SqlDbType.Time);
                time.Value = quiz.Time;

                ExecuteCommand(QUIZ_INSERT, quizGuid, levelGuid, categoryGuid,time); //call the base class method to execute the insert command 
        }

        /// <summary>
        /// Delete a Quiz object from the database, which has the uniqueidentifer equal with quizUid
        /// </summary>
        /// <param name="quizUID"></param>
        /// 
        [Obsolete("Is not Allowed to delete a quiz!")]
        public void Delete(Guid quizUID)
        {
            SqlParameter quizGuid = new SqlParameter("@QuizGUID", System.Data.SqlDbType.UniqueIdentifier);
            quizGuid.Value =quizUID;
            ExecuteCommand(QUIZ_DELETE, quizGuid); //call the base class method t execute the delete command
        }

        /// <summary>
        /// Update a Quiz object having the uniqueidentifier equal with the parameter object UID. 
        /// </summary>
        /// <param name="updatedQuiz"></param>
        /// 
        [Obsolete("A quiz can not be updated plase do not use these method")]
        public void Update(Quiz quiz)
        {
            SqlParameter quizGuid = new SqlParameter("@QuizGUID", System.Data.SqlDbType.UniqueIdentifier);
            quizGuid.Value = quiz.QuizUID;

            SqlParameter levelGuid = new SqlParameter("@LevelGUID", System.Data.SqlDbType.UniqueIdentifier);
            levelGuid.Value = quiz.LevelUID;

            SqlParameter categoryGuid = new SqlParameter("@CategoryGUID", System.Data.SqlDbType.UniqueIdentifier);
            categoryGuid.Value = quiz.CategoryUID;

            ExecuteCommand(QUIZ_UPDATE, quizGuid, levelGuid, categoryGuid); //call the base class method to execute the update cmmand
        }

   

        /// <summary>
        /// Return a Quiz object initialized from the reader SqlDataReader properties.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override Quiz ReaderToModel(SqlDataReader reader)
        {
            TimeSpan? time;
            if (reader.IsDBNull(3))
            {
                time = null;
            }
            else
            {
                time = reader.GetTimeSpan(3);
            }
            return new Quiz()
            {
               
                QuizUID = reader.GetGuid(0),
                LevelUID = reader.GetGuid(1),
                CategoryUID = reader.GetGuid(2),
                Time = time
            };

        }
        #endregion
    }
}
