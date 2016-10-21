using QuizEngine.Infrastructure;

namespace QuizEngine.Business.Core
{
    public class BusinessObject : BaseObject<BusinessContext>
    {
        #region Constructor
        public BusinessObject(BusinessContext businessContext) : base(businessContext) { }
        #endregion Constructor
    }
}
