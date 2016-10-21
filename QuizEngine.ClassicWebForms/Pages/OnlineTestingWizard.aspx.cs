using QuizEngine.ClassicWebForms.Pages.Base;
using QuizEngine.Infrastructure;
using QuizEngine.Library;
using QuizEngine.Model.View;
using QuizEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuizEngine.ClassicWebForms.Pages
{
    public partial class OnlineTestingWizard : BasePage
    {
        #region Members
        private List<QuizQuestionView> _quizQuestions;
        protected Guid _quizID;
        protected string _jsonString;
        #endregion Members

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        #endregion Events

        #region Methods
        #endregion Methods
    }
}