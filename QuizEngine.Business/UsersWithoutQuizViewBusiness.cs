using QuizEngine.Business.Core;
using QuizEngine.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Business
{
    public class UsersWithoutQuizViewBusiness : BusinessObject
    {
         #region Constructors
        /// <summary>
        /// Creates a new UserBussiness 
        /// </summary>
        /// <param name="connectionString">connection String of to the database</param>
        /// <remarks>It will create a new UsersWithoutQuizViewBusiness by the connection String <seealso cref=""/> cref="QuizDBConnectedDB1.Repositories.UserRepository"/></remarks>
        public UsersWithoutQuizViewBusiness(BusinessContext repositoryContext)
            : base(repositoryContext)
        {
        } 
        #endregion

         #region Methods
        public List<UsersWithoutQuizView> ReadUsersWithoutTheQuizId(string sortExpression, int rowsPerPage, int pageNumber,Guid quizID)
        {
            return _context.Repository.Objects.UsersWithoutQuizView.ReadUsersWithoutTheQuizId(sortExpression, rowsPerPage, pageNumber, quizID);
        }
        #endregion Methods
    }
}
