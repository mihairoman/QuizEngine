using QuizEngine.ClassicWebForms.Pages.Base;
using QuizEngine.Infrastructure;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using System;

namespace QuizEngine.ClassicWebForms.Pages
{
    public partial class EditTag : BasePage
    {
        #region Members
        private string _tagID;
        private Tag _currentTag;
        #endregion Members

        #region Properties
        public PageMode PageMode
        {
            get
            {
                return (PageMode)Convert.ToInt32(Request.QueryString["Mode"]);
            }
        }
        #endregion Properties

        #region Methods
        private void PopulatePage()
        {
            if (PageMode == PageMode.View || PageMode == PageMode.Update)
            {
                _currentTag = _context.Objects.Tag.ReadByID(new Guid(_tagID));
            }

            switch (PageMode)
            { 
                case PageMode.View:
                    txtTagName.Text = _currentTag.TagName;
                    txtTagName.ReadOnly = true;
                    break;
                case PageMode.Update:
                    txtTagName.Text = _currentTag.TagName;
                    break;
                case PageMode.Insert:
                    txtTagName.Text = string.Empty;
                    break;
            }
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

            _tagID = Request.QueryString["TagID"];
            if (!IsPostBack)
            {
                PopulatePage();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            switch (PageMode)
            { 
                case PageMode.View:
                    break;
                case PageMode.Update:
                    _currentTag = _context.Objects.Tag.ReadByID(new Guid(_tagID));
                    if (_currentTag.TagName != txtTagName.Text)
                    {
                        _currentTag.TagName = txtTagName.Text;
                    }
                    _context.Objects.Tag.Update(_currentTag);
                    break;
                case PageMode.Insert:
                    _currentTag = new Tag(Guid.NewGuid(), txtTagName.Text);
                    _context.Objects.Tag.Insert(_currentTag);
                    break;

            }
            Response.Redirect("ManageTags.aspx");
        }
        #endregion Events
    }
}