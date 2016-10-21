using QuizEngine.Webservice;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace QuizEngine.Webservice
{
    /// <summary>
    /// Summary description for QuizGenerate
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService]
    public class QuizGenerate : BaseWebservice
    {
        #region Members
        private JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();
        #endregion Members

        #region Methods
        [WebMethod]
        public string InsertQuiz(string quiz)
        {
            Quiz q = _javaScriptSerializer.Deserialize<Quiz>(quiz);
            _context.Objects.Quiz.Insert(q);
            return null;
        }

        [WebMethod]
        public string MakeTemplate(string quizID, string questionGuidList, string specialQuizName, string timespan)
        {
            Guid _quizID = _javaScriptSerializer.Deserialize<Guid>(quizID);
            TimeSpan? _timespan = _javaScriptSerializer.Deserialize<TimeSpan>(timespan);
            List<Guid> _questionGuidList = _javaScriptSerializer.Deserialize<List<Guid>>(questionGuidList);
            string _templateQuizName = _javaScriptSerializer.Deserialize<string>(specialQuizName);
            if (timespan != null)
            {
                _timespan = _javaScriptSerializer.Deserialize<TimeSpan>(timespan);
            }
            return _javaScriptSerializer.Serialize(_context.Objects.QuizTemplate.MakeTemplate(_quizID, _questionGuidList, _templateQuizName, _timespan));
        }

        [WebMethod]
        public string InsertArchivedQuiz(string questionlist, string quizUID, string userUID, string takequiz, string indexorder)
        {
            List<Guid> questionList = _javaScriptSerializer.Deserialize<List<Guid>>(questionlist);
            Guid quizID = _javaScriptSerializer.Deserialize<Guid>(quizUID);
            Guid userID = _javaScriptSerializer.Deserialize<Guid>(userUID);
            List<Int16> indexOrder = _javaScriptSerializer.Deserialize<List<Int16>>(indexorder);
            _context.Objects.ArchivedQuestion.Insert_ById(questionList, quizID, indexOrder);

            if (takequiz.Equals("true"))
            {
                QuizUserLink quLink = new QuizUserLink()
                {
                    QuizUID = quizID,
                    UserUID = userID,
                    Result = null,
                    QuizDate = System.DateTime.Now,
                    OnlineOrDownloaded = true,
                    IsTaken = true
                };
                _context.Objects.QuizUserLink.Insert(quLink);
            }
            return null;
        }
        #endregion Methods
    }
}
