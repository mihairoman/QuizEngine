using QuizEngine.ClassicWebForms.Pages.Base;
using QuizEngine.Infrastructure;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using QuizEngine.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuizEngine.WebForms
{
    public partial class EditQuestion :BasePage
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {

            ContextSeed contextSeed = InitializeSeed();

            if (!HasAllPermissions(contextSeed.UserID, Permissions.ManageQuestions))
            {
                Response.Redirect("PermissionDenied.aspx");
            }
        } 
        #endregion
    }
}