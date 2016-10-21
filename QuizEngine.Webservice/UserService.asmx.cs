using QuizEngine.Webservice;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace QuizEngine.Webservice
{
    /// <summary>
    /// Summary description for UserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserService : BaseWebservice
    {

        #region Members
        JavaScriptSerializer _serializer = new JavaScriptSerializer();
        string _sortExpression;
        int _rowsPerPage;
        int _pageNumber;
        #endregion Members

        #region Methods

        [WebMethod]
        public String CountAllUsers()
        {
            return _serializer.Serialize(_context.Objects.User.CountAllUsers());
        }


        /// <summary>
        /// Get all users from database
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetAllUsers(string sortExpression, string rowsPerPage, string pageNumber)
        {
            _sortExpression = _serializer.Deserialize<String>(sortExpression);
            _rowsPerPage = _serializer.Deserialize<Int16>(rowsPerPage);
            _pageNumber = _serializer.Deserialize<Int16>(pageNumber);

            return _serializer.Serialize(_context.Objects.User.ReadAllViewFiltered(_sortExpression, _rowsPerPage, _pageNumber));
        }

        /// <summary>
        /// For a specific user, the status will change: if the status is active, it will be changed in deactive and reverse
        /// </summary>
        /// <param name="UserGuid"></param>
        /// <param name="setStatus"></param>
        [WebMethod]
        public void SetUserStatus(string UserGuid, string setStatus)
        {
            Guid userID = _serializer.Deserialize<Guid>(UserGuid);
            bool setUserStatus = _serializer.Deserialize<bool>(setStatus);

            _context.Objects.User.SetStatus(userID, !setUserStatus);
        }


        /// <summary>
        /// Add a user into database
        /// </summary>
        /// <param name="allCheckedPermissions"></param>
        /// <param name="user"></param>
        [WebMethod]
        public bool AddUser(string allCheckedPermissions, string user)
        {
            User _user = _serializer.Deserialize<User>(user);
            string _allCheckedPermissions = _serializer.Deserialize<string>(allCheckedPermissions);

            if (_context.Objects.User.ReadByEmail(_user.UserName) != null)
            {
                return true;
            }
            else
            {
                if (String.IsNullOrEmpty(_allCheckedPermissions))
                {
                    _context.Objects.User.Insert(_user);
                }
                else
                {
                    _context.Objects.User.Insert(_user, _allCheckedPermissions);
                }

                return false;
            }
        }

        /// <summary>
        /// Edit a specific user and save the data into database
        /// </summary>
        /// <param name="allCheckedPermissions"></param>
        /// <param name="user"></param>
        [WebMethod]
        public void EditUser(string allCheckedPermissions, string user)
        {
            User _user = _serializer.Deserialize<User>(user);
            string _allCheckedPermissions = _serializer.Deserialize<string>(allCheckedPermissions);

            if (String.IsNullOrEmpty(_allCheckedPermissions))
            {
                _context.Objects.User.Update(_user);
            }
            else
            {
                _context.Objects.User.Update(_user, _allCheckedPermissions);
            }
        }


        /// <summary>
        /// Get all the permissions that can be applied for a user
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetPermissionList()
        {
            Type typePerm = typeof(Permissions);
            FieldInfo[] fieldsPerm = typePerm.GetFields();

            List<String> allPermissions = new List<String>();

            foreach (var field in fieldsPerm)
            {
                allPermissions.Add(string.Format("permission={{PermissionName:'{0}', PermissionValue:'{1}'}};", field.Name, field.GetValue(null).ToString()));
            }

            return _serializer.Serialize(allPermissions);
        }


        /// <summary>
        /// Get all the user types that a user can be
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetAllUserTypes()
        {
            List<String> allUserTypesAsList = new List<String>();

            foreach (var val in Enum.GetValues(typeof(UserType)))
            {
                var name = Enum.GetName(typeof(UserType), val);

                allUserTypesAsList.Add(string.Format("user={{UserType:'{0}', UserValue:'{1}'}};", name, ((int)val).ToString()));

            }

            return _serializer.Serialize(allUserTypesAsList);
        }


        /// <summary>
        /// Get all the permissions for a specific user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetUserPermissions(string userID)
        {
            Guid _userID = _serializer.Deserialize<Guid>(userID);
            return _serializer.Serialize(_context.Objects.Security.ReadUserPermissions(_userID));
        }

        /// <summary>
        /// Get all the information about a specific user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetUserByID(string userID)
        {
            Guid _userID = _serializer.Deserialize<Guid>(userID);
            return _serializer.Serialize(_context.Objects.User.ReadByID(_userID));
        }

        #endregion Methods
    }
}
