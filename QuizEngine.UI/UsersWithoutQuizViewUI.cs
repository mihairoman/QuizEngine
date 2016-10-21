using QuizEngine.Model.View;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.UI
{
    public class UsersWithoutQuizViewUI : UIObject
    {
          #region Constructors
        /// <summary>
        /// Creates a new UserBussiness 
        /// </summary>
        /// <param name="connectionString">connection String of to the database</param>
        /// <remarks>It will create a new UsersWithoutQuizViewUI by the connection String <seealso cref=""/> cref="QuizDBConnectedDB1.Repositories.UserRepository"/></remarks>
        public UsersWithoutQuizViewUI(UIContext context) : base(context) { }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a list of users who don't have the specified quiz id
        /// </summary>
        /// <param name="quizID">String that represents a UUID</param>
        public List<UsersWithoutQuizView> ReadUsersWithoutTheQuizId(string sortExpresion, int rowsPerPage, int pageNumber,Guid quizID)
        {
            return _context.Business.Objects.UsersWithoutQuizView.ReadUsersWithoutTheQuizId(sortExpresion, rowsPerPage,pageNumber, quizID);
        }
        #endregion 
    }
}
