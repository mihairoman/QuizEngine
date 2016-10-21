using QuizEngine.UI.Core;
using System;
using System.Web;
using System.Web.Services;

namespace QuizEngine.ClassicWebForms.WebServices
{
    public class BaseWebservice : WebService
    {
        #region Members
        protected UIContext _context;
        #endregion Members

        #region Constructor
        public BaseWebservice()
        {
            _context = new UIContext(new Infrastructure.ContextSeed { UserID =  Guid.NewGuid()});
        }
        #endregion Constructor

        #region Methods
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _context.Dispose();
            UIContext.ResetCachedContext();
        }
        #endregion Methods
    }
}