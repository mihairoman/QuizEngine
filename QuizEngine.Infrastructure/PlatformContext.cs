using System;
using System.Collections.Generic;

namespace QuizEngine.Infrastructure
{
    public class PlatformContext : IDisposable
    {
        #region Members
        protected ContextSeed _contextSeed;
        #endregion Members

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextSeed"></param>
        public PlatformContext(ContextSeed contextSeed)
        {
            _contextSeed = contextSeed;
        }
        #endregion Constructor

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
                _contextSeed = null;
            }
            disposed = true;
        }

        ~PlatformContext()
        {
            Dispose(false);
        }
        #endregion IDisposable Implementation
    }
}