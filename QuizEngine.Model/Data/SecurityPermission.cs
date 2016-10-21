using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Model.Data
{
    public class SecurityPermission
    {
        /// <summary>
        /// Unique identifier of the User
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// Unique identifier of the Permission
        /// </summary>
        public Guid PermissionGuid { get; set; }
    }
}
