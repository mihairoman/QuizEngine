using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Model.Data
{
    public class LinkPermission
    {
        #region Constructors
        public LinkPermission()
        {

        }
        /// <summary>
        /// Instantiates a <see cref="QuizEngine.Model.Data.LinkPermission"/>.
        /// </summary>
        /// <param name="tagUID"></param>
        /// <param name="tagName"></param>
        public LinkPermission(string link, Guid permissionGUID, string cssClass)
        {
            Link = link;
            PermissionGUID = permissionGUID;
            CSSClass = cssClass;
        }
        #endregion Constructors

        #region Properties

        public string Link { get; set; }

        public Guid PermissionGUID { get; set; }

        public string CSSClass { get; set; }

        public string Name { get; set; }

        #endregion Properties
    }
}
