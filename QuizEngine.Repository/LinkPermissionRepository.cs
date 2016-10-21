using System;
using QuizEngine.Model.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizEngine.Repository.Core;
using System.Data.SqlClient;
using System.Data;

namespace QuizEngine.Repository
{
    public class LinkPermissionRepository : BaseRepository<LinkPermission>
    {
        #region Constants
        private const string LINKPERMISSION_READLINKPERMISSIONBYUSERGUID = "dbo.LinkPermission_ReadLinkPermissionsByUserGuid";
        #endregion Constants


        #region Constructors
        public LinkPermissionRepository(RepositoryContext context)
            : base(context)
        {

        }
        #endregion Constructors

        #region Methods
        public List<LinkPermission> ReadByUserID(Guid userID)
        {
            SqlParameter userParam = new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier) { Value = userID };

            return ExecuteReadCommand(LINKPERMISSION_READLINKPERMISSIONBYUSERGUID, ReaderToModel, userParam);
        }


        protected override LinkPermission ReaderToModel(SqlDataReader reader)
        {

            return new LinkPermission
            {
                Link = reader.GetString(0),
                PermissionGUID = reader.GetGuid(1),
                CSSClass=reader.GetString(2)
            };
        }
        #endregion Methods

    }
}
