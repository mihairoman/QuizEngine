using QuizEngine.Webservice;
using QuizEngine.Model.Data;
using QuizEngine.Model.View;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace QuizEngine.Webservice
{
    /// <summary>
    /// Summary description for AdminDownloadGeneratedTestViewService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AdminDownloadGeneratedTestViewService : BaseWebservice
    {

        #region Members
        JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();
        string _sortExpression;
        int _pageNumber;
        int _rowsPerPage;
        #endregion Members

        #region Methods
        [WebMethod]
        public string GetRandomGeneratedQuizzes(string sortExpression,string rowsPerPage, string pageNumber)
        {
             if (!string.IsNullOrEmpty(sortExpression))
            {
            _sortExpression = _javaScriptSerializer.Deserialize<String>(sortExpression);
            _rowsPerPage = _javaScriptSerializer.Deserialize<Int16>(rowsPerPage);
            _pageNumber = _javaScriptSerializer.Deserialize<Int16>(pageNumber);
            List<AdminDownloadGeneratedTestView> lista= UIContext.CachedContext.Objects.AdminDownloadGeneratedTestView.ReadAllGeneratedTests(_sortExpression,_rowsPerPage, _pageNumber);
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
        public string GetPredefinedQuizzes(string sortExpressionPredefined, string rowsPerPagePredefined, string pageNumberPredefined)
        {


            if (!string.IsNullOrEmpty(sortExpressionPredefined))
            {
                _sortExpression = _javaScriptSerializer.Deserialize<String>(sortExpressionPredefined);
                _rowsPerPage = _javaScriptSerializer.Deserialize<Int16>(rowsPerPagePredefined);
                _pageNumber = _javaScriptSerializer.Deserialize<Int16>(pageNumberPredefined);
                List<AdminDownloadPredefinedTestView> lista = UIContext.CachedContext.Objects.AdminDownloadPredefinedTestView.ReadAllPredefinedTests(_sortExpression, _rowsPerPage, _pageNumber);
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
        public String CountAllRandomGeneratedQuizzes()
        {
            return _javaScriptSerializer.Serialize(_context.Objects.Quiz.CountAllRandom());
        }
        [WebMethod]
        public String CountAllPredefinedQuizzes()
        {
            return _javaScriptSerializer.Serialize(_context.Objects.Quiz.CountAllPredefined());
        }
        #endregion Methods
    }
}
