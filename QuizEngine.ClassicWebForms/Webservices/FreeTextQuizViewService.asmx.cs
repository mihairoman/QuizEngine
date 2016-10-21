using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using QuizEngine.UI.Core;
using QuizEngine.ClassicWebForms.WebServices;
using QuizEngine.Model.View;

namespace QuizEngine.ClassicWebForms.Webservices
{
    /// <summary>
    /// Summary description for FreeTextQuizViewService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FreeTextQuizViewService : BaseWebservice
    {
        #region Members
        JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();
        string _sortExpression;
        int _pageNumber;
        int _rowsPerPage;
        #endregion Members

        #region Methods
        [WebMethod]
        public string GetFreeTextQuizes(string sortExpression, string rowsPerPage, string pageNumber)
        {
            if (!string.IsNullOrEmpty(sortExpression))
            {
                _sortExpression = _javaScriptSerializer.Deserialize<String>(sortExpression);
                _rowsPerPage = _javaScriptSerializer.Deserialize<Int16>(rowsPerPage);
                _pageNumber = _javaScriptSerializer.Deserialize<Int16>(pageNumber);
               
                List<FreeTextQuizView> lista = _context.Objects.FreeTextQuizView.ReadAll(_sortExpression,_rowsPerPage,_pageNumber);

                if (lista.Count > 0)
                {
                    return _javaScriptSerializer.Serialize(lista);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            };
        }
        [WebMethod]
        public String CountAllFilteredQuizzes()
        {
            return _javaScriptSerializer.Serialize(_context.Objects.FreeTextQuizView.CountAllFilteredQuizzes());
        }

        #endregion Methods


    }
}
