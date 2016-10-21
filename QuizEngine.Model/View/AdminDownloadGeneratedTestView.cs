using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Model.View
{
    public class AdminDownloadGeneratedTestView
    {
        #region Properties
        public Guid QuizUID { get; set; }
        public Guid LevelUID { get; set; }
        public Guid CategoryUID { get; set; }
        public string LevelName { get; set; }
        public Int16 LevelDifficulty { get; set; }
        public string CategoryName { get; set; }
        public string SortExpresion { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }
        #endregion
    }
}
