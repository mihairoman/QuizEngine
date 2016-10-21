using QuizEngine.Business.Core;
using QuizEngine.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Business
{
    public class AdminDownloadPredefinedTestViewBusiness : BusinessObject
    {
         #region Constructors
            /// <summary>
            /// Constructor for a new Answer Business. Also calls the default constructor.
            /// </summary>
            /// <param name="connectionString">String for the database connection, used for creating a new <see cref="DatabseConnection.Repository.AnswerRepository">Answer Repository</see>.</param>
        public AdminDownloadPredefinedTestViewBusiness(BusinessContext businessContext)
            : base(businessContext)
            {}
            #endregion Constructors

        
          #region Methods
            /// <summary>
            /// ReadAllGeneratedTests returns all the generated tests
            /// </summary>
            /// <returns>Returns a list with all the generated tests from the database</returns>
        public List<AdminDownloadPredefinedTestView> ReadAllPredefinedTests(string sortExpression, int rowsPerPage, int pageNumber)
            {
               return _context.Repository.Objects.AdminDownloadPredefinedTestView.ReadAllPredefinedTests(sortExpression,rowsPerPage,pageNumber);
            }
      
        #endregion
    }
}
