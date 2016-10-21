using Microsoft.SqlServer.Server;
using QuizEngine.ClassicWebForms.Pages.Base;
using QuizEngine.Infrastructure;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuizEngine.ClassicWebForms
{
    public partial class ManageQuestions : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie myCookie = Request.Cookies["currentUser"];
            myCookie.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Add(myCookie);
        }
        
    }
}