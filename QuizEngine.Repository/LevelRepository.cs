using QuizEngine.Model.Data;
using QuizEngine.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using QuizEngine.Repository.Core;

namespace QuizEngine
{
    /// <summary>
    /// Class LevelRepository
    /// </summary>
    public class LevelRepository : BaseRepository<Level>
    {

        #region Constants
        private const string LEVELS_READBYID = "dbo.Levels_ReadByID";
        private const string LEVELS_READALL = "dbo.Levels_ReadAll";
        private const string LEVELS_INSERT = "dbo.Levels_Insert";
        private const string LEVELS_UPDATE = "dbo.Levels_Update";
        private const string LEVELS_DELETE = "dbo.Levels_Delete";
        private const string LEVELS_CANBEDELETED = "dbo.Levels_Delete";
        #endregion Constants

        #region Constructors
        /// <summary>
        /// Instantiates a <see cref="QuizEngine.Repository.LevelRepository"/>.
        /// </summary>
        /// 
        public LevelRepository(RepositoryContext context)
            : base(context)
        {
        }


        #endregion Constructors

        #region Methods
        public List<Level> ReadAll()
        {

            return ExecuteReadCommand(LEVELS_READALL, ReaderToModel);
        }

        public Level ReadById(Guid LevelID)
        {
            SqlParameter levelUidParam = new SqlParameter("@LevelGUID", SqlDbType.UniqueIdentifier)
                    {
                        Value = LevelID
                    };
            return ExecuteReadCommand(LEVELS_READBYID, ReaderToModel, levelUidParam).SingleOrDefault();
        }

        protected override Level ReaderToModel(SqlDataReader reader)
        {
            return new Level
                                {
                                    LevelUID = reader.GetGuid(0),
                                    LevelName = reader.GetString(1),
                                    Difficulty = reader.GetInt16(2)
                                };
        }

        public void Insert(Level level)
        {

            SqlParameter[] param = {
                new SqlParameter("@LevelGUID", SqlDbType.UniqueIdentifier){Value = level.LevelUID},
                new SqlParameter("@LevelName", SqlDbType.NVarChar, 50){Value = level.LevelName},
                new SqlParameter("@Difficulty", SqlDbType.SmallInt){Value = level.Difficulty}
                                   };
            ExecuteCommand(LEVELS_INSERT, param);
        }



        public void Update(Level level)
        {

            SqlParameter[] param = {
                new SqlParameter("@LevelGUID", SqlDbType.UniqueIdentifier){Value = level.LevelUID},
                new SqlParameter("@LevelName", SqlDbType.NVarChar, 50){Value = level.LevelName},
                new SqlParameter("@Difficulty", SqlDbType.SmallInt){Value = level.Difficulty}
                                   };
            ExecuteCommand(LEVELS_UPDATE, param);


        }

        public void Delete(Level level)
        {
            SqlParameter[] param = {
                new SqlParameter("@LevelGUID", SqlDbType.UniqueIdentifier){Value = level.LevelUID}};
            if (ExecuteCheckCommand(LEVELS_CANBEDELETED, param))
            {
                param = new SqlParameter[1];
                param[0] = new SqlParameter("@LevelGUID", SqlDbType.UniqueIdentifier) { Value = level.LevelUID};
                ExecuteCommand(LEVELS_DELETE, param);
            }
            else
                throw new Exception("This level cannot be deleted.");

            
            ExecuteCommand(LEVELS_DELETE, param);

        }
        #endregion Methods
    }
}
