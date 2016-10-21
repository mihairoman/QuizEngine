using QuizEngine.Infrastructure;
using QuizEngine.Repository.Core;

namespace QuizEngine.Business.Core
{
    public class BusinessContext : PlatformContext
    {
        #region Members
        private RepositoryContext _repositoryContext;
        private BusinessObjectContainer _objects;
        #endregion Members

        #region Constructor
        public BusinessContext(ContextSeed contextSeed) : base(contextSeed) { }
        #endregion Constructor

        #region Properties
        public RepositoryContext Repository
        {
            get 
            {
                if (_repositoryContext == null)
                    _repositoryContext = new RepositoryContext(_contextSeed);
                return _repositoryContext; 
            }
        }

        public BusinessObjectContainer Objects
        {
            get
            {
                if (_objects == null)
                    _objects = new BusinessObjectContainer(this);
                return _objects;
            }
        }
        #endregion Properties

        #region IDisposable Implementation
        private bool disposed = false;

        public override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if(_repositoryContext != null)
                    _repositoryContext.Dispose();
                if(_objects != null)
                    _objects.Dispose();
            }
            disposed = true;
            base.Dispose(disposing);
        }
        #endregion IDisposable Implementation
    }
}
