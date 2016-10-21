using QuizEngine.Library;
using QuizEngine.Model.Data;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuizEngine.Repository
{
    public class SecurityRepository : BaseRepository<SecurityPermission>
    {
        /// <summary>
        /// Names procedures declaration 
        /// </summary>
        #region Constants
        private const string USERS_READPERMISSIONS = "dbo.Users_ReadPermissions";
        #endregion

        #region Constructors
        public SecurityRepository(RepositoryContext context)
            : base(context)
        {

        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Create the list of parameters for the USERS_READPERMISSIONS stored procedure and then execute it
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<SecurityPermission> ReadUserPermissions(Guid userID)
        {
            SqlParameter userParam = new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier) { Value = userID };

            return ExecuteReadCommand(USERS_READPERMISSIONS, ReaderToModel, userParam);
        }

        protected override SecurityPermission ReaderToModel(SqlDataReader reader)
        {
            return new SecurityPermission
            {

                UserGuid = reader.GetGuid(0),
                PermissionGuid = reader.GetGuid(1)
            };
        }
        #endregion Methods
    }
}
