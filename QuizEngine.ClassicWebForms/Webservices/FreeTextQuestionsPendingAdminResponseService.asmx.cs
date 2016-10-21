using QuizEngine.ClassicWebForms.WebServices;
using QuizEngine.Model.Data;
using QuizEngine.Model.Data.View;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace QuizEngine.ClassicWebForms.Webservices
{
    /// <summary>
    /// Summary description for FreeTextQuestionsPendingAdminResponseService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FreeTextQuestionsPendingAdminResponseService : BaseWebservice
    {
        private JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();
        private static List<ValidationFreeTextQuestionsView> _validationFreeTextQuestionList;
     
        [WebMethod]
        public string GetSerializedObject(string quiz, string user)
        {
            if (!string.IsNullOrEmpty(quiz) && !string.IsNullOrEmpty(user))
            {
                Guid _quizId = Guid.Parse(quiz);
                Guid _userId = Guid.Parse(user);
                if (_quizId != null && _userId != null)
                {
                    _validationFreeTextQuestionList = UIContext.CachedContext.Objects.ValidationFreeTextQuestionsView.ReadFreeTextQuestions(_quizId, _userId);
                    if (_validationFreeTextQuestionList.Count > 0)
                    {
                        return _javaScriptSerializer.Serialize(_validationFreeTextQuestionList);
                    }                   
                }               
            }
            return "";           
        }

        [WebMethod]
        public string DeserializeJsonString(string listObjects)
        {
            if (!string.IsNullOrEmpty(listObjects) && !listObjects.Equals("[]"))
            {
                    _validationFreeTextQuestionList = _javaScriptSerializer.Deserialize<List<ValidationFreeTextQuestionsView>>(listObjects);
                    if (_validationFreeTextQuestionList.Count > 0)
                    {
                        foreach (ValidationFreeTextQuestionsView freeTextQuestion in _validationFreeTextQuestionList)
                        {
                            if(freeTextQuestion.Grade < 0 && freeTextQuestion.Grade > 1)
                            {
                                return "";
                            }
                        }

                        UIContext.CachedContext.Objects.ValidationFreeTextQuestionsView.UpdateFreeTextQuestionsList(_validationFreeTextQuestionList);

                        Guid quiz = _validationFreeTextQuestionList[0].QuizUID;
                        Guid user = _validationFreeTextQuestionList[0].UserUID;

                        QuizUserLink quizUserLink = new QuizUserLink()
                        {
                             QuizUID = quiz,
                             UserUID = user,
                             Result = (Convert.ToDecimal(UIContext.CachedContext.Objects.QuizResult.GetResult(quiz, user))/ UIContext.CachedContext.Objects.ArchivedQuestion.ReadAllByQuizID(quiz).Count)*100
                        };

                        UIContext.CachedContext.Objects.QuizUserLink.UpdateFinalResult(quizUserLink);

                        return "OK!";
                    }                                      
              }        
                return "";            
          }   
    }
}
