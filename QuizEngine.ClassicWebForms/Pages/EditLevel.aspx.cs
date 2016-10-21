using QuizEngine.ClassicWebForms.Pages.Base;
using QuizEngine.Infrastructure;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using System;

namespace QuizEngine.ClassicWebForms.Pages
{
    public partial class EditLevel : BasePage
    {
        #region Members
        private string _levelID;
        private Level _currentLevel;
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
                _currentLevel = _context.Objects.Level.ReadByID(new Guid(_levelID));
            }

            switch (PageMode)
            {
                case PageMode.View:
                    txtName.Text = _currentLevel.LevelName;
                    txtDifficulty.Text = _currentLevel.Difficulty.ToString();
                    break;
                case PageMode.Update:
                    txtName.Text = _currentLevel.LevelName;
                    txtDifficulty.Text = _currentLevel.Difficulty.ToString();
                    break;
                case PageMode.Insert:
                    txtName.Text = string.Empty;
                    txtDifficulty.Text = string.Empty;
                    break;
            }
        }
        #endregion Methods

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            ContextSeed contextSeed = InitializeSeed();

            if (!HasAllPermissions(contextSeed.UserID, Permissions.ManageLevels))
            {
                Response.Redirect("PermissionDenied.aspx");
            }

            _levelID = Request.QueryString["LevelID"];
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
                    _currentLevel = _context.Objects.Level.ReadByID(new Guid(_levelID));
                    if (_currentLevel.LevelName != txtName.Text) 
                    {
                        _currentLevel.LevelName = txtName.Text;
                    }

                    if (_currentLevel.Difficulty != Convert.ToInt16(txtDifficulty.Text))
                    {
                        _currentLevel.Difficulty = Convert.ToInt16(txtDifficulty.Text);
                    }
                    _context.Objects.Level.Update(_currentLevel);
                    break;
                case PageMode.Insert:
                    _currentLevel = new Level(Guid.NewGuid(), txtName.Text, Convert.ToInt16(txtDifficulty.Text));
                    _context.Objects.Level.Insert(_currentLevel);
                    break;
            }
            Response.Redirect("ManageLevels.aspx");
        }
        #endregion Events
    }
}