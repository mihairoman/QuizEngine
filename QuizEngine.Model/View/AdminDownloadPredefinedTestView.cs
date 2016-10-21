using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Model.View
{
    public class AdminDownloadPredefinedTestView
    {
        #region Properties
        public Guid QuizUID { get; set; }
        public string TypeName { get; set; }
        public string SortExpresion { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }
        #endregion
    }
}
