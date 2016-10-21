using QuizEngine.Library;
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
    /// Makes changes to the User table from the Database 
    /// </summary>
    public class UserRepository : BaseRepository<User>
    {
        #region Constants
        private const string USERS_READBYID = "dbo.Users_ReadByID";
        private const string USERS_READALL = "dbo.Users_ReadAll";
        private const string USERS_INSERT = "dbo.Users_Insert";
        private const string USERS_UPDATE = "dbo.Users_Update";
        private const string USERS_UPDATE_PERMISSIONS = "dbo.Users_UpdatePermissions";
        private const string USERS_DELETE = "dbo.Users_Delete";
        private const string USERS_SET_STATUS = "dbo.Users_SetStatus";
        private const string USERS_READBYEMAIL = "dbo.Users_ReadByEmail";
        private const string USERS_READ_BY_USERTYPE = "dbo.Users_ReadAllNonAdmin";
        private const string USER_READALLFILTERED = "dbo.Users_ReadAllFilltered";
        private const string USER_COUNTALLUSERS = "dbo.Users_CountAllUsers";
        private const string USERS_WITHOUT_QUIZ_ID_COUNT_ALL = "dbo.Users_WithoutQuizID_CountAll";
        #endregion

        #region Constructors
        public UserRepository(RepositoryContext context)
            : base(context)
        {

        }
        #endregion

        #region Methods


        public int CountAllUsers()
        {
            return Convert.ToInt32(ExecuteScalarCommand(USER_COUNTALLUSERS, null));
        }

        public List<User> ReadAllViewFiltered(string sortExpression, int rowsPerPage, int pageNumber)
        {
            SqlParameter[] param = {
               !string.IsNullOrEmpty(sortExpression) ? new SqlParameter("@SortExpression", SqlDbType.NVarChar,500){Value = sortExpression}
                                                     : new SqlParameter("@SortExpression", SqlDbType.NVarChar,500){Value = null},

                new SqlParameter("@FoundPageNumber", SqlDbType.Int){Value = pageNumber},
                new SqlParameter("@FoundRowspPage", SqlDbType.Int){Value = rowsPerPage}
            };
            return ExecuteReadCommand(USER_READALLFILTERED, ReaderToModel, param);
        }


        public User ReadByEmail(String username)
        {
            SqlParameter userParam = new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = username };
            return ExecuteReadCommand(USERS_READBYEMAIL, ReaderToModel, userParam).SingleOrDefault();
        }

        /// <summary>
        /// Reads a specific <see cref="QuizDBConnectedDB1.Models.User"/> from the Database
        /// </summary>
        /// <param name="userID">String that represens an unique identifier of the <see cref="QuizDBConnectedDB1.Models.User"/> </param>
        /// <returns><see cref="QuizDBConnectedDB1.Models.User"/> if a User with this GUID exists otherwise null</returns>
        public User ReadByID(Guid userID)
        {
            SqlParameter userParam = new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier) { Value = userID };

            return ExecuteReadCommand(USERS_READBYID, ReaderToModel, userParam).SingleOrDefault();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all <see cref="QuizDBConnectedDB1.Models.User"/> from the Database</returns>
        public List<User> ReadAll()
        {
            return ExecuteReadCommand(USERS_READALL, ReaderToModel);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns how many users who didn't take a specified quiz exist in the database</returns>
        public int Users_CountAll(Guid quizId)
        {
            SqlParameter userParam = new SqlParameter("@QuizGUID", SqlDbType.UniqueIdentifier) { Value = quizId };
            return (int)ExecuteScalarCommand(USERS_WITHOUT_QUIZ_ID_COUNT_ALL, userParam) ;
        }

        /// <summary>
        /// Inserts in the Database a new <see cref="QuizDBConnectedDB1.Models.User"/>
        /// </summary>
        /// <param name="user">Of type <see cref="QuizDBConnectedDB1.Models.User"/></param>
        public void Insert(User user, string permissionList = default(string))
        {
            SqlParameter[] userParam = {
                new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier) { Value = user.UserGuid },
                new SqlParameter("@Username", SqlDbType.NVarChar, -1) { Value = user.UserName },
                new SqlParameter("@Usertype", SqlDbType.SmallInt) { Value = user.UserType }
            };

            ExecuteCommand(USERS_INSERT, userParam);


            if (!object.ReferenceEquals(null, permissionList))
            {
                SqlParameter[] permissionParam = {
                new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier) { Value = user.UserGuid },
                new SqlParameter("@PermissionsListGUID", SqlDbType.NVarChar, -1) { Value = permissionList}
            };

                ExecuteCommand(USERS_UPDATE_PERMISSIONS, permissionParam);
            }
        }

        /// <summary>
        /// Update in the Database all the fields of the row identified by <see cref="QuizDBConnectedDB1.Models.User.UserGUID"/>
        /// from the parameter
        /// </summary>
        /// <param name="user">Of type <see cref="QuizDBConnectedDB1.Models.User"/></param>
        public void Update(User user, string permissionList = default(string))
        {
            SqlParameter[] userParam = {
                new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier) { Value = user.UserGuid },
                new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = user.UserName },
                new SqlParameter("@Usertype", SqlDbType.SmallInt) { Value = user.UserType },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = user.IsActive }
            };

            ExecuteCommand(USERS_UPDATE, userParam);

            if (!object.ReferenceEquals(null, permissionList))
            {
                SqlParameter[] permissionParam = {
                new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier) { Value = user.UserGuid },
                new SqlParameter("@PermissionsListGUID", SqlDbType.NVarChar, -1) { Value = permissionList}
            };

                ExecuteCommand(USERS_UPDATE_PERMISSIONS, permissionParam);
            }
        }


        /// <summary>
        /// Deletes from the database searching by the <see cref="QuizDBConnectedDB1.Models.User.Username"/>
        /// of the <see cref="QuizDBConnectedDB1.Models.User"/>
        /// </summary>
        /// <param name="user">Of type <see cref="QuizDBConnectedDB1.Models.User"/></param>
        public void Delete(User user)
        {
            SqlParameter[] userParam = {
                new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = user.UserName }
            };

            ExecuteCommand(USERS_DELETE, userParam);
        }

        /// <summary>
        /// Deactivate a user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="newStatus"></param>
        public void SetStatus(Guid userID, bool newStatus)
        {
            SqlParameter[] userParam = {
                new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier) { Value = userID },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = newStatus }
            };

            ExecuteCommand(USERS_SET_STATUS, userParam);
        }

        public List<User> ReadAllNonAdmin()
        {
            return ExecuteReadCommand(USERS_READ_BY_USERTYPE, ReaderToModel);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override User ReaderToModel(SqlDataReader reader)
        {

            return new User
            {
                UserGuid = reader.GetGuid(0),
                UserName = reader.GetString(1),
                UserType = (UserType)reader.GetInt16(2),
                IsActive = reader.GetBoolean(3)
            };
        }

        #endregion
    }
}