
using QuizEngine.Infrastructure;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuizEngine.ClassicWebForms.Pages
{
    public partial class EditArchivedChoice : System.Web.UI.Page
    {
        #region Members
        //private PlatformContext _platContext = new PlatformContext();
        private ArchivedChoice _currentChoice;
        #endregion Members

        #region Properties

        public PageMode PageMode
        {
            get
            {
                return (PageMode)Convert.ToInt32(Request.QueryString["mode"]);
            }
        }

        /// <summary>
        /// Gets the ChoiceID from the QueryString of the requested page
        /// </summary>
        public Guid ChoiceID
        {
            get { return new Guid(Request.QueryString["ChoiceID"].ToString()); }
        }

        /// <summary>
        /// Gets the QuestionID from the QueryString of the requested page
        /// </summary>
        public Guid QuestionID
        {
            get { return new Guid(Request.QueryString["QuestionID"].ToString()); }
        }


        //public ArchivedChoice CurrentChoice
        //{
        //    get
        //    {
        //        if (ViewState["CurrentChoice"] == null)
        //        {
        //            ViewState["CurrentChoice"] = new ArchivedChoice();
        //        }
        //        return (ArchivedChoice)ViewState["CurrentChoice"];
        //    }
        //    set
        //    {
        //        ViewState["CurrentChoice"] = value;
        //    }
        //}
        #endregion Properties

        #region Methods
        private void PopulatePage()
        {
            if (PageMode == PageMode.View || PageMode == PageMode.Update)
            {
               // _currentChoice = _platContext.GetBusinessObject<ArchivedChoiceBusiness>().ReadById(ChoiceID, QuestionID);
            }

            switch (PageMode)
            {
                case PageMode.View:
                    txtAnswerText.Text = _currentChoice.AnswerText;
                    txtValue.Text = _currentChoice.Value.ToString();
                    txtIsCorrect.Text = _currentChoice.IsCorrect.ToString();
                    txtAnswerText.ReadOnly = true;
                    txtValue.ReadOnly = true;
                    txtIsCorrect.ReadOnly = true;
                    break;
                case PageMode.Update:
                    txtAnswerText.Text = _currentChoice.AnswerText;
                    txtValue.Text = _currentChoice.Value.ToString();
                    txtIsCorrect.Text = _currentChoice.IsCorrect.ToString();
                    break;
                case PageMode.Insert:
                    txtAnswerText.Text = string.Empty;
                    txtValue.Text = string.Empty;
                    txtIsCorrect.Text = string.Empty;
                    break;
            }
        }
        #endregion Methods

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulatePage();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            switch (PageMode)
            {
                case PageMode.View:
                    break;
                case PageMode.Update:
                    ArchivedChoice archivedChoice = new ArchivedChoice()
                    {
                        ChoiceID = ChoiceID,
                        QuestionID = QuestionID,
                        AnswerText = txtAnswerText.Text,
                        Value = decimal.Parse(txtValue.Text),
                        IsCorrect = (String.IsNullOrWhiteSpace(txtIsCorrect.Text)) ? null : (bool?)Convert.ToBoolean(txtIsCorrect.Text)
                    };
                    //_platContext.GetBusinessObject<ArchivedChoiceBusiness>().Update(archivedChoice);

                    break;
                case PageMode.Insert:
                    ArchivedChoice newChoice = new ArchivedChoice()
                    {
                        ChoiceID = Guid.NewGuid(),
                        QuestionID = Guid.NewGuid(),
                        AnswerText = txtAnswerText.Text,
                        Value = decimal.Parse(txtValue.Text),
                        IsCorrect = (String.IsNullOrWhiteSpace(txtIsCorrect.Text)) ? null : (bool?)Convert.ToBoolean(txtIsCorrect.Text)
                    };
                    //_platContext.GetBusinessObject<ArchivedChoiceBusiness>().Insert(newChoice);
                    break;
            }
            Response.Redirect("ManageArchivedChoices.aspx");
        }
        #endregion Events
    }
}