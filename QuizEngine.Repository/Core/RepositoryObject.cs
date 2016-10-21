using QuizEngine.Infrastructure;

namespace QuizEngine.Repository.Core
{
    public class RepositoryObject : BaseObject<RepositoryContext>
    {
        #region Constructor
        public RepositoryObject(RepositoryContext repositoryContext) : base(repositoryContext) { }
        #endregion Constructor
    }
}
