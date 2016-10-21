using QuizEngine.Infrastructure;
using System.Configuration;

namespace QuizEngine.Repository.Core
{
    public class RepositoryContext : PlatformContext
    {
        #region Members
        private string _connectionString;
        private RepositoryObjectContainer _objects;
        private bool disposed = false;
        #endregion Members

        #region Constructor
        public RepositoryContext(ContextSeed contextSeed) : base(contextSeed)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        }
        #endregion Constructor

        #region Properties
        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public RepositoryObjectContainer Objects
        {
            get 
            {
                if (_objects == null)
                    _objects = new RepositoryObjectContainer(this);
                return _objects;
            }
        }
        #endregion Properties

        #region Methods
        public override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (_objects != null)
                    _objects.Dispose();
            }
            disposed = true;
            base.Dispose(disposing);
        }
        #endregion Methods
    }
}
