using QuizEngine.ClassicWebForms.Pages.Base;
using QuizEngine.Infrastructure;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuizEngine.ClassicWebForms.Pages
{
    public partial class UserHomeScreen : BasePage
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            ContextSeed _contextSeed = InitializeSeed();

            if (!IsPostBack)
            {
                lblUser.Text = _context.Objects.User.ReadByID(_contextSeed.UserID).UserName;
                grdQuizes.DataSource = _context.Objects.Quiz.ReadAllQuizesPerUserView(_contextSeed.UserID);
                grdQuizes.DataBind();
            }
        }
        #endregion Events
    }
}