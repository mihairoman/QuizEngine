using QuizEngine.Library;
using QuizEngine.Model.View;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Repository
{
    public class UsersWithoutQuizViewRepository : BaseRepository<UsersWithoutQuizView>
    {
        #region Constants
        private const string USERS_WITHOUT_THE_QUIZ_ID = "dbo.Users_WithoutTheQuizID";
        #endregion

        #region Constructors
        public UsersWithoutQuizViewRepository(RepositoryContext context)
            : base(context)
        {

        }
        #endregion
 
        #region Methods
        /// <summary>
        /// Reads a specific <see cref="QuizDBConnectedDB1.Models.User"/> from the Database
        /// </summary>
        /// <param name="userID">String that represens an unique identifier of the <see cref="QuizDBConnectedDB1.Models.User"/> </param>
        /// <returns><see cref="QuizDBConnectedDB1.Models.User"/> Returns all users who doesn't have the specified quiz id</returns>
        public List<UsersWithoutQuizView> ReadUsersWithoutTheQuizId(string sortExpression, int rowsPerPage, int pageNumber, Guid quizID)
        {
            SqlParameter[] param =
                                    {
                                      new SqlParameter("@SortExpression", SqlDbType.NVarChar){ Value = sortExpression},
                                      new SqlParameter("@GivenFoundrowPerPage", SqlDbType.Int){Value = rowsPerPage},
                                      new SqlParameter("@GivenFoundPageNumber", SqlDbType.Int){Value = pageNumber},
                                      new SqlParameter("@GivenQuizGUID", SqlDbType.UniqueIdentifier){Value = quizID}

                                    };
            return ExecuteReadCommand(USERS_WITHOUT_THE_QUIZ_ID, ReaderToModel, param);
        }

        protected override UsersWithoutQuizView ReaderToModel(SqlDataReader reader)
        {

            return new UsersWithoutQuizView()
            {
                            UserGuid=reader.GetGuid(0),
                            UserName =reader.GetString(1),
                            TypeName =reader.GetString(2),
                            IsActive = reader.GetBoolean(3)
            };
        }
        #endregion
    }
}
