using QuizEngine.Webservice;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using QuizEngine.Model.Data.View;
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
    /// Summary description for OnlineTestingWizardService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class OnlineTestingWizardService : BaseWebservice
    {
        #region Members
        private JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();
        private List<QuizQuestionView> _wizardQuestions;
        private List<Answer> _desirializedJson;
        #endregion Members


        #region Methods
        [WebMethod]
        public string GetSerializedObject(string quizId, string userId)
        {
            Guid _quizID = Guid.Parse(quizId);
            Guid _userID = Guid.Parse(userId);
            if (_quizID != null)
            {
                _wizardQuestions = UIContext.CachedContext.Objects.ArchivedQuestion.GetQuizQuestionsByQuizID(_quizID);
                return _javaScriptSerializer.Serialize(_wizardQuestions);
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public string DeserializeJsonString(string transmitJsonString, string hasFreeText, string quizId, string userId, string questionsNr)
        {

            //Guid userID = _javaScriptSerializer.Deserialize<Guid>(userId);
            //Guid quizID = _javaScriptSerializer.Deserialize<Guid>(quizId);

            QuizUserLink quLink = new QuizUserLink()
            {
                QuizUID = Guid.Parse(quizId),
                UserUID = Guid.Parse(userId),
                Result = null,
                QuizDate = System.DateTime.Now,
                OnlineOrDownloaded = true,
                IsTaken = true
            };

            if (!transmitJsonString.Equals("[]"))
            {

                _desirializedJson = _javaScriptSerializer.Deserialize<List<Answer>>(transmitJsonString);
                foreach (Answer answer in _desirializedJson)
                {
                    UIContext.CachedContext.Objects.Answer.Insert(answer);
                }


                if (!Convert.ToBoolean(hasFreeText))
                {
                    quLink.Result = ((decimal)UIContext.CachedContext.Objects.QuizResult.GetResult(quLink.QuizUID, quLink.UserUID) / Convert.ToInt32(questionsNr));
                    UIContext.CachedContext.Objects.QuizUserLink.Update(quLink);
                    return String.Format("{0:0.00}", quLink.Result * 100);

                }
                else
                {

                    return "freeText";
                }
            }
            else
            {
                quLink.Result = (decimal)0;
                UIContext.CachedContext.Objects.QuizUserLink.Update(quLink);
                return "0";
            }
        }


        #endregion Methods
    }
}
