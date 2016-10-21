using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuizEngine.Infrastructure;
using QuizEngine.Business;
using QuizEngine.Model.Data.View;
using QuizEngine.Model.Data;
using System.Configuration;
using QuizEngine.UI.Core;


namespace QuizEngine.ClassicWebForms
{
    public partial class Validation : System.Web.UI.Page
    {
        protected UIContext context = new UIContext(new ContextSeed());
        private Guid QuizId;
        private Guid UserId;
        protected string JsonString;
        protected string PostedString;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie myCookie = Request.Cookies["currentUser"];
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);

            QuizId = new Guid(Request.QueryString["QuizUID"]);
            UserId = new Guid(Request.QueryString["UserUID"]);
            List<ValidationFreeTextQuestionsView> list = context.Objects.ValidationFreeTextQuestionsView.ReadFreeTextQuestions(QuizId,UserId);
            JsonString = JsonSerializer.SerializeJSon<List<ValidationFreeTextQuestionsView>>(list);
            this.ClientScript.GetPostBackEventReference(this, "arg");
            if (IsPostBack)
            {
                string eventTarget = this.Request["__EVENTTARGET"];
                string eventArgument = this.Request["__EVENTARGUMENT"];

                if (eventTarget != String.Empty && eventTarget == "callPostBack")
                {
                    if (eventArgument != String.Empty)
                    {
                        PostedString = eventArgument;
                        List<ValidationFreeTextQuestionsView> updateList = JsonSerializer.DeserializeJSon<ValidationFreeTextQuestionsView>(PostedString);
                        context.Objects.ValidationFreeTextQuestionsView.UpdateFreeTextQuestionsList(updateList);
                        QuizUserLink quizUserLink = new QuizUserLink()
                        {
                            QuizUID = QuizId,
                            UserUID = UserId,
                            Result =  Convert.ToDecimal(context.Objects.QuizResult.GetResult(QuizId, UserId))                                                        
                        };
                        context.Objects.QuizUserLink.UpdateFinalResult(quizUserLink);
                        Response.Redirect(String.Format("AdminHomeScreen.aspx"));
                    }
                }
            }
           
        }
       
       
    }
}