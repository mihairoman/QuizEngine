using QuizEngine.Library;
using System;

namespace QuizEngine.Model.Data
{
    /// <summary>
    /// I'm a User
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// Unique identifier of the User
        /// </summary>
        public Guid UserGuid { get; set; }

        public string UserName { get; set; }

        /// <summary>
        /// Enumeration of type <see cref="QuizEngine.Library.UserType"/>
        /// </summary>
        public UserType UserType { get; set; }
        
        /// <summary>
        /// A User can not be deleted. It can be only deactivated.
        /// By convention true means Activated and false means deactivated
        /// </summary>
        public bool IsActive { get; set; }

    }
}
