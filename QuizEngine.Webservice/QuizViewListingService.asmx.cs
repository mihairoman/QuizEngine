using System.Web.Services;
using System.Web.Script.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using QuizEngine.UI;
using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using QuizEngine.Webservice;
namespace QuizEngine.Webservice
{
   
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    //in order to the web service to be called from a script uncomment the line below
    [System.Web.Script.Services.ScriptService]
    public class QuizViewListingService : BaseWebservice
    {
        #region Members
        JavaScriptSerializer _serializer = new JavaScriptSerializer();
        #endregion

        #region WebMethods
        /// <summary>
        /// Return all the quizes taken by all the Users, in JSON format.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="rowPerPage"></param>
        /// <param name="sortExpression"></param>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        [WebMethod]
        public string QuizViewRead(string pageNumber = null,string rowPerPage =null, string sortExpression = "userGUID ASC", string userGuid = null)
        {
             int? pageNumberInter = 1;
             int? rowPerPageInter = 5;
            try
            {
               pageNumberInter = _serializer.Deserialize<int?>(pageNumber);
               rowPerPageInter = _serializer.Deserialize<int?>(rowPerPage);
                if (pageNumberInter <= 0 || rowPerPageInter <= 0)
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
            string sortExpressionInter = sortExpression.Trim();
            string[] components = sortExpressionInter.Split(' ');
            components[0].Trim();
            components[1].Trim();
            if (!components[0].Equals("userGUID") && !components[0].Equals("UserName") && !components[0].Equals("Result") && !components[0].Equals("CategoryName")
                && !components[0].Equals("QuizDate") && !components[0].Equals("Difficulty") && !components[0].Equals("QuizGUID") && !components[0].Equals("Time"))
            {
                return null;
            }
            if (!components[1].ToLower().Equals("asc") && !components[1].ToLower().Equals("desc"))
            {
                return null;
            }
            if (String.IsNullOrEmpty(userGuid) || userGuid.Equals("00000000-0000-0000-0000-000000000000"))
            {
                return _serializer.Serialize(_context.Objects.Quiz.ReadAllViewQuizes(pageNumberInter, rowPerPageInter, sortExpression));
            }
            Guid UserGuidInter = new Guid(userGuid);
            return _serializer.Serialize(_context.Objects.Quiz.ReadAllViewQuizes(pageNumberInter,rowPerPageInter,sortExpression,UserGuidInter));
        }


        /// <summary>
        /// Return all the Users in JSON format.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string AllUsersRead()
        {
            return _serializer.Serialize( _context.Objects.User.ReadAll());
        }

        [WebMethod]
        [Obsolete("Can be replaced by the QuizViewRead method if the caller code specify the userGuid optional parameter")]
        public string ReadAllQuizPerUserView(Guid userGuid)
        {
            return _serializer.Serialize(_context.Objects.Quiz.ReadAllQuizesPerUserView(userGuid));
        }

        /// <summary>
        /// Return the number of QuizView objects.
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        [WebMethod]
        public string CountAllQuizView(string userGuid = null)
        {
            if (!String.IsNullOrEmpty(userGuid))
            {
                 Guid userGuidInter = new Guid(userGuid);
                 return _serializer.Serialize(_context.Objects.Quiz.CountQuizView(userGuidInter));
            }else
            {
                return _serializer.Serialize(_context.Objects.Quiz.CountQuizView());
            }
        }

        /// <summary>
        /// Return the All the template Quizes.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string ReadAllTemplateQuizes(string pageNumber=null,string quizTemplatePerPage=null,string sortExpression = null)
        {
            try
            {
                int? pageNumberInter = _serializer.Deserialize<int?>(pageNumber);
                int? quizTemplatePerPageInter = _serializer.Deserialize<int?>(quizTemplatePerPage);
                if (pageNumberInter > 0 && quizTemplatePerPageInter > 0)
                {
                    return _serializer.Serialize(_context.Objects.QuizTemplate.ReadAllQuizTemplateView(pageNumberInter,quizTemplatePerPageInter,sortExpression));
                }
            }
            catch (Exception)
            {
                return null;
            }
         
            return _serializer.Serialize(_context.Objects.QuizTemplate.ReadAllQuizTemplateView());
        }

        [WebMethod]
        public string CountAllTemplateQuizes()
        {
            return _serializer.Serialize(_context.Objects.QuizTemplate.CountQuizTemplate());
        }

        [WebMethod]
        public string ReadAllAnswersByQuizUserId(string quizId, string userId)
        {
         //   Guid _quizId = _serializer.Deserialize<Guid>(quizId);
          //  Guid _userId = _serializer.Deserialize<Guid>(userId);
            Guid _quizId = new Guid(quizId);
            Guid _userId = new Guid(userId);
            return  _serializer.Serialize(_context.Objects.Answer.ReadByQuizUserId(_quizId, _userId));
        }
        #endregion
    }
}
