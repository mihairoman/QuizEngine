using QuizEngine.Model.View;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.UI
{
    public class AdminDownloadPredefinedTestViewUI : UIObject
    {
         #region Constructor
        public AdminDownloadPredefinedTestViewUI(UIContext context) : base(context) { }
        #endregion

         #region Methods
        /// <summary>
        /// ReadFreeTextQuestions returns all the questions where the type is free text
        /// </summary>
        /// <returns>Returns a list with all the FreeTextQuizes from the database</returns>
        public List<AdminDownloadPredefinedTestView> ReadAllPredefinedTests(string sortExpression, int rowsPerPage, int pageNumber)
        {
            return _context.Business.Objects.AdminDownloadPredefinedTestView.ReadAllPredefinedTests(sortExpression, rowsPerPage, pageNumber);
          
        }
        #endregion
    }
}
