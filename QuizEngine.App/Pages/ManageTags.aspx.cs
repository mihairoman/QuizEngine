using QuizEngine.ClassicWebForms.Pages.Base;
using QuizEngine.Infrastructure;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using System;
using System.Web.UI.WebControls;

namespace QuizEngine.ClassicWebForms.Pages
{
    public partial class ManageTags : BasePage
    {
        #region Methods
        protected void FillGrid()
        {
            grdTag.DataSource = _context.Objects.Tag.ReadAll();
            grdTag.DataBind();
        }
        #endregion Methods

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            ContextSeed contextSeed = InitializeSeed();

            if (!HasAllPermissions(contextSeed.UserID, Permissions.ManageTags))
            {
                Response.Redirect("PermissionDenied.aspx");
            }

            if (!IsPostBack)
            {
                FillGrid();
            }
        }

        protected void grdTag_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Tag tag = new Tag();
            tag.TagUID = new Guid(grdTag.DataKeys[e.RowIndex].Values[0].ToString());
            _context.Objects.Tag.Delete(tag);
            FillGrid();
        }

        protected void grdTag_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string redirectURL = string.Format("EditTag.aspx?TagID={0}&Mode=2", grdTag.DataKeys[e.NewEditIndex].Values[0].ToString());
            Response.Redirect(redirectURL);
            grdTag.EditIndex = e.NewEditIndex;
            FillGrid();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            string redirectURL = string.Format("EditTag.aspx?TagID={0}&Mode=1", Guid.Empty.ToString());
            Response.Redirect(redirectURL);
        } 
        #endregion Events
    }
}