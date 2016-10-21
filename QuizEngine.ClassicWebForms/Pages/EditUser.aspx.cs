using QuizEngine.ClassicWebForms.Pages.Base;
using QuizEngine.Infrastructure;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuizEngine.ClassicWebForms.Pages
{
    public partial class EditUser : BasePage
    {
        #region Members
        #endregion

        #region Properties
        /// <summary>
        /// Get the PageMode value
        /// </summary>
        ///  <returns><see cref="QuizEngine.Library.PageMode"/>  View = 0, Insert = 1, Update = 2</returns>
        public string PageMode
        {
            get
            {
                return Request.QueryString["PageMode"];
            }
        }

        /// <summary>
        /// Get the UserID using the get method
        /// </summary>
        public string UserId
        {
            get
            {
                return Request.QueryString["userID"];
            }
        }
        #endregion Properties

        #region Events
        /// <summary>
        /// Populate the page only if the page is being rendered for the first time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ContextSeed contextSeed = InitializeSeed();

            if (!HasAllPermissions(contextSeed.UserID, Permissions.ManageUsers))
            {
                Response.Redirect("PermissionDenied.aspx");
            }
        }
        #endregion Events
    }
}