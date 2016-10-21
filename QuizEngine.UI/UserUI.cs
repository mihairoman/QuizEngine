﻿using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using QuizEngine.Infrastructure;
using QuizEngine.Business.Core;
using QuizEngine.UI.Core;
using QuizEngine.Library;

namespace QuizEngine.UI
{
    public class UserUI : UIObject
    {
        #region Constructors
        /// <summary>
        /// Creates a new UserBussiness 
        /// </summary>
        /// <param name="connectionString">connection String of to the database</param>
        /// <remarks>It will create a new UserRespository by the connection String <seealso cref=""/> cref="QuizDBConnectedDB1.Repositories.UserRepository"/></remarks>
        public UserUI(UIContext context) : base(context) { }
        #endregion

        #region Methods

        public int CountAllUsers()
        {
            return _context.Business.Objects.User.CountAllUsers();
        }

        public List<User> ReadAllViewFiltered(string sortExpression, int rowsPerPage, int pageNumber)
        {
            return _context.Business.Objects.User.ReadAllViewFiltered(sortExpression, rowsPerPage, pageNumber);
        }

        public User ReadByEmail(String username)
        {
            return _context.Business.Objects.User.ReadByEmail(username);
        }

        /// <summary>
        /// Returns a list with all the users from the database
        /// </summary>
        /// <returns></returns>
        public List<User> ReadAll()
        {
            return _context.Business.Objects.User.ReadAll();
        }

        /// <summary>
        /// Returns the user described by the ID if it doesen't find that user returns null
        /// </summary>
        /// <param name="userID">String that represents a UUID</param>
        /// <returns></returns>
        public User ReadByID(Guid userID)
        {
            return _context.Business.Objects.User.ReadByID(userID);
        }

        /// <summary>
        /// Updates a User
        /// </summary>
        /// <param name="user"><see cref="QuizEngine.Models.User"/></param>
        public void Update(User user, string permissionList = default(string))
        {
            _context.Business.Objects.User.Update(user, permissionList);
        }

        /// <summary>
        /// Inserts a new <see cref="QuizEngine.Models.User"/> in the DB
        /// </summary>
        /// <param name="user"><see cref="QuizEngine.Models.User"/></param>
        public void Insert(User user, string permissionList = default(string))
        {
            _context.Business.Objects.User.Insert(user, permissionList);
        }

        /// <summary>
        /// Deletes a User from DB
        /// </summary>
        /// <param name="user"><see cref="QuizEngine.Modelss.User"/></param>
        public void Delete(User user)
        {
            _context.Business.Objects.User.Delete(user);
        }

        public void SetStatus(Guid userID, bool newStatus)
        {
            _context.Business.Objects.User.SetStatus(userID, newStatus);
        }

        public List<User> ReadAllNonAdmin()
        {
            return _context.Business.Objects.User.ReadAllNonAdmin();
        }

        public int Users_CountAll(Guid quizId)
        {
            return _context.Business.Objects.User.Users_CountAll(quizId);
        }
        #endregion
    }
}
