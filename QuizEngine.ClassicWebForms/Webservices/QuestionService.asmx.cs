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

namespace QuizEngine.ClassicWebForms.WebServices
{
    /// <summary>
    /// Summary description for QuestionService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class QuestionService : BaseWebservice
    {
        private JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();

        [WebMethod]
        public string GetTags()
        {
            return _javaScriptSerializer.Serialize(UIContext.CachedContext.Objects.Tag.ReadAll());
        }

        [WebMethod]
        public string GetLevels()
        {
            return _javaScriptSerializer.Serialize(UIContext.CachedContext.Objects.Level.ReadAll());
        }

        [WebMethod]
        public string GetLevelByID(string levelUID)
        {
            return _javaScriptSerializer.Serialize(UIContext.CachedContext.Objects.Level.ReadByID(_javaScriptSerializer.Deserialize<Guid>(levelUID)));
        }

        [WebMethod]
        public string GetTemplateNames()
        {
            return _javaScriptSerializer.Serialize(UIContext.CachedContext.Objects.QuizTemplate.ReadAllQuizTemplates());
        }

        [WebMethod]
        public string GetCategories()
        {
            return _javaScriptSerializer.Serialize(UIContext.CachedContext.Objects.Category.ReadAll());
        }

        [WebMethod]
        public string GetTagsByQuestionID(string questionID)
        {
            return _javaScriptSerializer.Serialize(UIContext.CachedContext.Objects.Tag.ReadByQuestionID(new Guid(questionID)));
        }
        [WebMethod]
        public string GetChoiceByQuestionID(string questionGUID)
        {
            return _javaScriptSerializer.Serialize(UIContext.CachedContext.Objects.Choice.ReadChoiceByQuestionID(new Guid(questionGUID)));
        }

        [WebMethod]
        public string GetQuestionByID(string questionID)
        {
            return _javaScriptSerializer.Serialize(UIContext.CachedContext.Objects.Question.ReadByID(new Guid(questionID)));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AddQuestion(string question, string tags, string choices)
        {
            Question _question = _javaScriptSerializer.Deserialize<Question>(question);
            List<Tag> _tags = _javaScriptSerializer.Deserialize<List<Tag>>(tags);
            List<Choice> _choices = _javaScriptSerializer.Deserialize<List<Choice>>(choices);
            UIContext.CachedContext.Objects.Question.Insert(_question, _tags, _choices);
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UpdateQuestion(string question, string tags, string choices)
        {
            Question _question = _javaScriptSerializer.Deserialize<Question>(question);
            List<Tag> _tags = _javaScriptSerializer.Deserialize<List<Tag>>(tags);
            List<Choice> _choices = _javaScriptSerializer.Deserialize<List<Choice>>(choices);
            UIContext.CachedContext.Objects.Question.Update(_question, _tags, _choices);
            return null;
        }
    }
}
