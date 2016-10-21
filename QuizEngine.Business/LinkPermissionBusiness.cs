using QuizEngine.Business.Core;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Business
{
    public class LinkPermissionBusiness : BusinessObject
    {
        #region Constructors
        public LinkPermissionBusiness(BusinessContext repositoryContext)
            : base(repositoryContext)
        {
        }
        #endregion Constructors


        #region Methods
        public List<LinkPermission> ReadByUserID(Guid userID)
        {
            return _context.Repository.Objects.LinkPermission.ReadByUserID(userID);
        }
        #endregion Methods
    }
}
