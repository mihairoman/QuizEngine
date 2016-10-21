using QuizEngine.Business.Core;
using QuizEngine.Infrastructure;
using System;
using System.Web;

namespace QuizEngine.UI.Core
{
    public class UIContext : PlatformContext
    {
        #region Members
        private BusinessContext _businessContext;
        private UIObjectContainer _objects;

        [ThreadStatic]
        private static UIContext _cachedContext;
        #endregion Members

        #region Constructor
        public UIContext(ContextSeed contextSeed) : base(contextSeed) 
        {
            CachedContext = this;
        }
        #endregion Constructor

        #region Properties
        public static UIContext CachedContext
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Items["UIContext"] as UIContext;
                }
                return _cachedContext;
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items["UIContext"] = value;
                }
                _cachedContext = value;
            }
        }

        public BusinessContext Business
        {
            get
            {
                if (_businessContext == null)
                    _businessContext = new BusinessContext(_contextSeed);
                return _businessContext;
            }
        }

        public UIObjectContainer Objects
        {
            get
            {
                if (_objects == null)
                    _objects = new UIObjectContainer(this);
                return _objects;
            }
        }
        #endregion Properties

        #region Methods
        public static void ResetCachedContext()
        {
            CachedContext = null;
        }
        #endregion Methods

        #region IDisposable Implementation
        private bool disposed;

        public override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if(_businessContext != null)
                    _businessContext.Dispose();
                if(_objects != null)
                    _objects.Dispose();
            }
            disposed = true;
            base.Dispose(disposing);
        }
        #endregion IDisposable Implementation
    }
}
