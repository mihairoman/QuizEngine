using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.UI.WebControls;
using QuizEngine.Model.Data;
using QuizEngine.ClassicWebForms.Pages.Base;
using QuizEngine.UI.Core;
using System.Collections.Generic;
using QuizEngine.Library;

namespace QuizEngine.ClassicWebForms.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        #region Members
        private UIContext _context;
        #endregion Members

        #region Properties
        #endregion Properties

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["currentUser"] != null)
            {
                Response.Redirect("~/Pages/ScoreQuizzes.aspx");
            }
        }

        #endregion Events

        protected void btnGoogleLogin_Click(object sender, EventArgs e)
        {

        }

        #region Methods

        #endregion Methods

    }
}
