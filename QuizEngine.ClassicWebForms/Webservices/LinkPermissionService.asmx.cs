using QuizEngine.ClassicWebForms.WebServices;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace QuizEngine.ClassicWebForms.Webservices
{
    /// <summary>
    /// Summary description for LinkPermissionService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LinkPermissionService : BaseWebservice
    {
        #region Members
        Guid _selectedUserGUID;
        JavaScriptSerializer _serializer = new JavaScriptSerializer();
        HttpCookie currentUser;
        #endregion Members

        [WebMethod]
        public String GetLinkPermissions(Guid userGUID)
        {
            List<LinkPermission> linkList = _context.Objects.LinkPermission.ReadByUserID(userGUID);
            return _serializer.Serialize(linkList);
        }


        [WebMethod]
        public String GetUserID(String username)
        {
            User user = _context.Objects.User.ReadByEmail(_serializer.Deserialize<String>(username));

            if (user != null)
            {
                return _serializer.Serialize(user.UserGuid);
            }
            else
            {
                return _serializer.Serialize(Guid.Empty);
            }
        }


        [WebMethod]
        public String LoginWindows()
        {
            User user = _context.Objects.User.ReadByEmail(WindowsIdentity.GetCurrent().Name);
            return _serializer.Serialize(user);
        }

        [WebMethod]
        public String LoginFacebook(String username)
        {
            User user = _context.Objects.User.ReadByEmail(_serializer.Deserialize<String>(username));
            return _serializer.Serialize(user);
        }

        [WebMethod]
        public String HasPermission(String userGuid, String PermissionName)
        {
            Guid userID2 = new Guid(userGuid);
            String permName = PermissionName;
            Type typePerm = typeof(Permissions);
            FieldInfo[] fieldsPerm = typePerm.GetFields();

            Guid permissionGUID = new Guid();

            foreach (var field in fieldsPerm)
            {
                if (field.Name == permName)
                {
                    permissionGUID = new Guid(field.GetValue(null).ToString());
                    break;
                }
                
            }

            List<SecurityPermission> userPermissions = _context.Objects.Security.ReadUserPermissions(userID2);

            foreach (SecurityPermission sp in userPermissions)
            {
                if (sp.PermissionGuid == permissionGUID)
                {
                    return _serializer.Serialize("true");
                }
            }
            return _serializer.Serialize("false");
        }

    }
}
