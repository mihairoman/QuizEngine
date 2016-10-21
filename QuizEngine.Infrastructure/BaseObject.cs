using System;

namespace QuizEngine.Infrastructure
{
    public class BaseObject<T> : IDisposable
        where T: PlatformContext
    {
        #region Members
        protected T _context;
        #endregion Members

        #region Constructors
        public BaseObject(T context)
        {
            _context = context;
        }
        #endregion Constructors

        #region IDisposable Implementation
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (_context != null)
                    _context = null;
            }
            disposed = true;
        }

        ~BaseObject()
        {
            Dispose(false);
        }
        #endregion IDisposable Implementation
    }
}
