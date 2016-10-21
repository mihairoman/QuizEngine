using QuizEngine.Business.Core;
using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using QuizEngine.Repository;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;

namespace QuizEngine.Business
{
    public class SecurityBusiness : BusinessObject
    {
        #region Constructors
        /// <summary>
        /// Creates a new SecurityBussiness 
        /// </summary>
        /// <param name="connectionString">connection String of to the database</param>
        /// <remarks>It will create a new UserRespository by the connection String <seealso cref=""/> cref="QuizDBConnectedDB1.Repositories.UserRepository"/></remarks>
        public SecurityBusiness(BusinessContext context)
            : base(context)
        {
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Returns a list with the current user and his permissions
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<SecurityPermission> ReadUserPermissions(Guid userID)
        {
            return _context.Repository.Objects.Security.ReadUserPermissions(userID);
        }
        #endregion Methods

    }
}
