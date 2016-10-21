using QuizEngine.Library;
using System;

namespace QuizEngine.Model.View
{
    /// <summary>
    /// I'm a User
    /// </summary>
    [Serializable]
    public class UsersWithoutQuizView
    {

        /// <summary>
        /// Unique identifier of the User
        /// </summary>
   
        public Guid UserGuid { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// Enumeration of type <see cref="QuizEngine.Library.UserType"/>
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// A User can not be deleted. It can be only deactivated.
        /// By convention true means Activated and false means deactivated
        /// </summary>
        public bool IsActive { get; set; }
        public string SortExpresion { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }
    }
}
