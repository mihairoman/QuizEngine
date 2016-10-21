using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using QuizEngine.Library;

namespace QuizEngine.ClassicWebForms
{
    public partial class ManageArchivedChoices : System.Web.UI.Page
    {
        #region Members
        public PlatformContext _platContext;
        public ArchivedChoice archivedChoice;
        #endregion Members

        #region Constructors
        public ManageArchivedChoices()
        {
           // _platContext = new PlatformContext();
        }
        #endregion Constructors

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
            
        }

        #region Methods
        protected void FillGrid()
        {
            //gvArchivedChoices.DataSource = _platContext.GetBusinessObject<ArchivedChoiceBusiness>().ReadAll();
            gvArchivedChoices.DataBind();
        }
        #endregion Methods

        #region Events
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            string URL = String.Format("EditArchivedChoice.aspx?ChoiceID={0}&QuestionID={1}&mode=1", Guid.Empty.ToString(), Guid.Empty.ToString());
            Response.Redirect(URL);

        }

        protected void gvArchivedChoices_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ArchivedChoice currentArchivedChoice = e.Row.DataItem as ArchivedChoice;

            }
        }

        protected void gvArchivedChoices_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string URL = String.Format(string.Format("EditArchivedChoice.aspx?ChoiceID={0}&QuestionID={1}&mode=2", gvArchivedChoices.DataKeys[e.NewEditIndex].Values[0].ToString(), gvArchivedChoices.DataKeys[e.NewEditIndex].Values[1].ToString()));
            Response.Redirect(URL);
        }

        protected void gvArchivedChoices_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvArchivedChoices_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
             archivedChoice = new ArchivedChoice(); 
             archivedChoice.ChoiceID = new Guid(gvArchivedChoices.DataKeys[e.RowIndex].Values[0].ToString());
             archivedChoice.QuestionID = new Guid(gvArchivedChoices.DataKeys[e.RowIndex].Values[1].ToString());
             //_platContext.GetBusinessObject<ArchivedChoiceBusiness>().Delete(archivedChoice.ChoiceID, archivedChoice.QuestionID);
             FillGrid();
        }

        #endregion Events
    }
}