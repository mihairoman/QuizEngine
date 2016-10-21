using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizEngine.Infrastructure;
using QuizEngine.Model.View;
using QuizEngine.Repository;
using QuizEngine.Business.Core;

namespace QuizEngine.Business
{
    public class FreeTextQuizViewBusiness : BusinessObject
    {
        #region Constructors
        /// <summary>
        /// The constructor receives the context as a parameter and calls the super with it as a paramter.
        /// </summary>
        /// <param name="platformContext"></param>
        public FreeTextQuizViewBusiness(BusinessContext context) : base(context) { }
        #endregion

        #region Methods
        /// <summary>
        /// ReadAll FreeText Quizes that have the result = null and are taken online
        /// </summary>
        /// <returns>Returns a list with all the FreeTextQuizes from the database</returns>
        public List<FreeTextQuizView> ReadAll(string sortExpression, int rowsPerPage, int pageNumber)
        {
            return _context.Repository.Objects.FreeTextQuizView.ReadAll(sortExpression, rowsPerPage, pageNumber);
        }
          public int CountAllFilteredQuizzes()
        {
            return _context.Repository.Objects.FreeTextQuizView.CountAllFilteredQuizzes();
        }

        
        #endregion
    }
}
