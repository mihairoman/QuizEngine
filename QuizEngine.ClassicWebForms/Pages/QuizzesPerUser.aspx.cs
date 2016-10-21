using QuizEngine.ClassicWebForms.Pages.Base;
using QuizEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuizEngine.ClassicWebForms.Pages
{
    public partial class QuizesPerUser : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlUsers.DataSource = _context.Objects.User.ReadAll();
                ddlUsers.DataTextField = "UserName";
                ddlUsers.DataValueField = "UserGUID";
                ddlUsers.DataBind();
                ddlUsers.Items.Insert(0, new ListItem("None", String.Empty));
                ddlUsers.SelectedIndex = 0;
            }
        }

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblTitle.Text = "";
            if (ddlUsers.SelectedIndex != 0)
            {
                grdQuizes.DataSource = _context.Objects.Quiz.ReadAllQuizesPerUserView(new Guid(ddlUsers.SelectedValue));
                grdQuizes.DataBind();
                if (grdQuizes.Rows.Count == 0)
                    lblMessage.Text = (String)GetLocalResourceObject("NoQuizzes");
                else
                    lblTitle.Text = (String)GetLocalResourceObject("QuizzesPerUser");
            }
            else
            {
                grdQuizes.DataSource = null;
                grdQuizes.DataBind();
            }
        }
    }
}