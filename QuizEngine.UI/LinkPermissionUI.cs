using QuizEngine.Infrastructure;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuizEngine.AssemblyResources;
using System.Resources;


namespace QuizEngine.UI
{
    public class LinkPermissionUI : UIObject
    {
        #region Constructors
        public LinkPermissionUI(UIContext context) : base(context) { }
        #endregion Constructors

        #region Methods
        public List<LinkPermission> ReadByUserID(Guid userID)
        {
            List<LinkPermission> PermissionList =  _context.Business.Objects.LinkPermission.ReadByUserID(userID);

            foreach (LinkPermission item in PermissionList)
            {
                item.Name = UIResources.ResourceManager.GetString(item.Link);
            }

            return PermissionList;
        }
        #endregion Methods
    }
}
