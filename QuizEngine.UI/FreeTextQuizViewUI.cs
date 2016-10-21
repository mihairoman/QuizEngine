using QuizEngine.Model.View;
using QuizEngine.UI.Core;
using System.Collections.Generic;

namespace QuizEngine.UI
{
    public class FreeTextQuizViewUI : UIObject
    {
        #region Constructors
        public FreeTextQuizViewUI(UIContext context) : base(context) { }
        #endregion

        #region Methods

        public List<FreeTextQuizView> ReadAll(string sortExpresion, int rowsPerPage, int pageNumber)
        {
            return _context.Business.Objects.FreeTextQuizView.ReadAll(sortExpresion,rowsPerPage, pageNumber);
        }
        public int CountAllFilteredQuizzes()
        {
            return _context.Business.Objects.FreeTextQuizView.CountAllFilteredQuizzes();
        }
        #endregion Methods
    }
}
