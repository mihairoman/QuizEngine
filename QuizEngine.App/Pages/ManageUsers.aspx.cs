using QuizEngine.ClassicWebForms.Pages.Base;
using QuizEngine.Infrastructure;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace QuizEngine.ClassicWebForms.Pages
{
    public partial class ManageUsers : BasePage
    {


        #region Events
        /// <summary>
        /// Verify if the user has enough permissions to access the current page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie myCookie = Request.Cookies["currentUser"];
            myCookie.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Add(myCookie);

            ContextSeed contextSeed = InitializeSeed();

            if (!HasAllPermissions(contextSeed.UserID, Permissions.ManageUsers))
            {
                Response.Redirect("PermissionDenied.aspx");
            }
        }
        #endregion Events
    }
}
