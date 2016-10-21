using QuizEngine.Webservice;
using QuizEngine.Model.Data;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace QuizEngine.Webservice
{
    /// <summary>
    /// Summary description for GenerateQuizLinkService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GenerateQuizLinkService : BaseWebservice
    {
        #region Members
        private JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();

        #endregion

        #region Methods
        [WebMethod]
        public string GetUsers()
        {
            List<User> users = UIContext.CachedContext.Objects.User.ReadAll();
            List<KeyValuePairInfo<Guid, String>> userKeys = new List<KeyValuePairInfo<Guid, String>>();

            foreach (User user in users)
            {
                KeyValuePairInfo<Guid, String> key = new KeyValuePairInfo<Guid, string>
                {
                    Key = user.UserGuid,
                    Value = user.UserName
                };
                userKeys.Add(key);
            }
            return _javaScriptSerializer.Serialize(userKeys);
        }

        /// <summary>
        /// Has the responsibility of sending a  mail to all users with the quiz link.
        /// Updates the QuizUserLink table with new data.
        /// </summary>
        /// <param name="allEmails"></param>
        /// <param name="allGUID"></param>
        /// <param name="quizGUID"></param>
        [WebMethod]
        public void SendDefaultEmailAndInsertQuizLink(string allEmails, string allGUID, string quizGUID)
        {
            List<string> selectedEmails = _javaScriptSerializer.Deserialize<List<string>>(allEmails);
            List<Guid> selectedGUID = _javaScriptSerializer.Deserialize<List<Guid>>(allGUID);
            Guid realQuizGUID = _javaScriptSerializer.Deserialize<Guid>(quizGUID);
            _context.Business.Objects.QuizUserLink.SendEmail(selectedEmails, selectedGUID, realQuizGUID);

            foreach (Guid guid in selectedGUID)
            {
                QuizUserLink quLink = new QuizUserLink()
                {
                    QuizUID = realQuizGUID,
                    UserUID = guid,
                    Result = null,
                    QuizDate = System.DateTime.Now,
                    OnlineOrDownloaded = true,
                    IsTaken = false
                };
                _context.Objects.QuizUserLink.Insert(quLink);
            }

        }

        /// <summary>
        /// Has the responsibility of sending a  mail to all users with the quiz link.
        /// Updates the QuizUserLink table with new data.
        /// </summary>
        /// <param name="allEmails"></param>
        /// <param name="allGUID"></param>
        /// <param name="quizGUID"></param>
        [WebMethod]
        public void SendCustomEmailAndInsertQuizLink(string allGUID, string quizGUID, string allEmails, string emailFrom, string emailSubject, string emailBody)
        {
            List<Guid> selectedGUID = _javaScriptSerializer.Deserialize<List<Guid>>(allGUID);
            Guid realQuizGUID = _javaScriptSerializer.Deserialize<Guid>(quizGUID);
            List<string> selectedEmails = _javaScriptSerializer.Deserialize<List<string>>(allEmails);
            string emailFromAddr = _javaScriptSerializer.Deserialize<string>(emailFrom);
            string realEmailSubject = _javaScriptSerializer.Deserialize<string>(emailSubject);
            string realEmailBody = _javaScriptSerializer.Deserialize<string>(emailBody);

            _context.Business.Objects.QuizUserLink.SendEmail(selectedGUID, realQuizGUID, selectedEmails, emailFromAddr, realEmailSubject, realEmailBody);

            foreach (Guid guid in selectedGUID)
            {
                QuizUserLink quLink = new QuizUserLink()
                {
                    QuizUID = realQuizGUID,
                    UserUID = guid,
                    Result = null,
                    QuizDate = System.DateTime.Now,
                    OnlineOrDownloaded = true,
                    IsTaken = false
                };
                _context.Objects.QuizUserLink.InsertForGeneratingNewLink(quLink);
            }

        }

        [WebMethod]
        public void SendEmail1(string allEmails, string allGUID, string quizGUID)
        {
            List<string> selectedEmails = _javaScriptSerializer.Deserialize<List<string>>(allEmails);
            List<Guid> selectedGUID = _javaScriptSerializer.Deserialize<List<Guid>>(allGUID);
            Guid realQuizGUID = _javaScriptSerializer.Deserialize<Guid>(quizGUID);
            _context.Business.Objects.QuizUserLink.SendEmail(selectedEmails, selectedGUID, realQuizGUID);

            foreach (Guid guid in selectedGUID)
            {
                QuizUserLink quLink = new QuizUserLink()
                {
                    QuizUID = realQuizGUID,
                    UserUID = guid,
                    Result = null,
                    QuizDate = System.DateTime.Now,
                    OnlineOrDownloaded = true,
                    IsTaken = false
                };
                UIContext.CachedContext.Objects.QuizUserLink.InsertForGeneratingNewLink(quLink);
            }

        }

        #endregion

    }
}
