using QuizEngine.Model.Data;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.UI
{
    public class SecurityUI : UIObject
    {
        #region Constructors
        public SecurityUI(UIContext context) : base(context) { }
        #endregion

        #region Methods
        public List<SecurityPermission> ReadUserPermissions(Guid userID)
        {
            return _context.Business.Objects.Security.ReadUserPermissions(userID);
        } 
        #endregion
    }
}
