using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizEngine.ClassicWebForms.Pages.Base
{
    public class BasePage : System.Web.UI.Page
    {
        #region Members
        protected UIContext _context;
        #endregion Members

        #region Properties
        public UIContext QuizUIContext { get { return _context; } }
        #endregion Properties

        #region Constructor
        public BasePage()
        {

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
        #endregion Methods

        #region Events
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            if (HttpContext.Current.Request.Cookies["currentUser"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }

            if (HttpContext.Current.Request.Cookies["currentUser"]["userID"] == null)
            {
                Response.Redirect("~/Pages/AccessDenied.aspx");
            }

            _context = new UIContext(InitializeSeed());
        }        
        #endregion Events

        protected bool HasAllPermissions(Guid userGUID, params Guid[] permissionsToCheck)
        {
            List<SecurityPermission> userPermissions = _context.Objects.Security.ReadUserPermissions(userGUID);
            foreach (Guid requiredPermission in permissionsToCheck)
            {
                if (!userPermissions.Any(perm => perm.PermissionGuid == requiredPermission))
                    return false;
            }

            return true;
        }

        #region IDisposable Implementation
        public override void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                UIContext.ResetCachedContext();
            }
            base.Dispose();
        }
        #endregion IDisposable Implementation
    }
}