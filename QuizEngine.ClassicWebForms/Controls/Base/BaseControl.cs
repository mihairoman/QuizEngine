using QuizEngine.UI.Core;
using System.Web;
using System.Web.UI;

namespace QuizEngine.ClassicWebForms.Controls.Base
{
    public class BaseControl : UserControl
    {
        #region Members
        protected UIContext _context;
        #endregion Members

        #region Constructor
        public BaseControl() 
        {
        }
        #endregion Constructor

        #region Events
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
            _context = UIContext.CachedContext;
        }
        #endregion Events
    }
}