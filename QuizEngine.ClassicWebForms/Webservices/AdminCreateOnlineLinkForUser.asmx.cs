using QuizEngine.ClassicWebForms.WebServices;
using QuizEngine.Model.Data;
using QuizEngine.Model.View;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace QuizEngine.ClassicWebForms.Webservices
{
    /// <summary>
    /// Summary description for AdminCreateOnlineLinkForUser
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AdminCreateOnlineLinkForUser : BaseWebservice
    {

        #region Members
        JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();
        string _sortExpression;
        int _pageNumber;
        int _rowsPerPage;
        #endregion Members

        #region Methods
        [WebMethod]
        public string ReadAllUsersWithoutTheQuizID(string sortExpression, string rowsPerPage, string pageNumber, string quiz)
        {
            Guid _quizID = _javaScriptSerializer.Deserialize<Guid>(quiz);
            _sortExpression = _javaScriptSerializer.Deserialize<String>(sortExpression);
            _rowsPerPage = _javaScriptSerializer.Deserialize<Int16>(rowsPerPage);
            _pageNumber = _javaScriptSerializer.Deserialize<Int16>(pageNumber);
            return _javaScriptSerializer.Serialize(UIContext.CachedContext.Objects.UsersWithoutQuizView.ReadUsersWithoutTheQuizId(_sortExpression, _rowsPerPage, _pageNumber, _quizID));
          
        }


        [WebMethod]
        public String CountAllUsers(string quiz)
        {
            Guid quizID = Guid.Parse(quiz);
            return _javaScriptSerializer.Serialize(_context.Objects.User.Users_CountAll(quizID));
        }
        #endregion
    }
}
