using QuizEngine.Infrastructure;
using QuizEngine.UI.Core;
using System;
using System.Web;
using System.Web.Services;

namespace QuizEngine.Webservice
{
    public class BaseWebservice : WebService
    {
         #region Members
        protected UIContext _context;
        #endregion Members

        #region Properties
        public UIContext QuizUIContext { get { return _context; } }
        #endregion Properties

        #region Constructor
        public BaseWebservice()
        {
            _context = new UIContext(new Infrastructure.ContextSeed { UserID =  Guid.NewGuid()});
        }
        #endregion Constructor

        #region Methods
        public ContextSeed InitializeSeed()
        {
            Guid userGUID = Guid.Empty;

            if (HttpContext.Current.Request.Cookies["currentUser"]["userID"] != null)
            {
                userGUID = new Guid(HttpContext.Current.Request.Cookies["currentUser"]["userID"]);
            }

            return new ContextSeed
            {
                UserID = userGUID
            };
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _context.Dispose();
            UIContext.ResetCachedContext();
        }
        #endregion Methods
    }
}