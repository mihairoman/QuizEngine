using QuizEngine.Model.Data;
using QuizEngine.Model.Data.View;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.UI
{
    public class QuizTemplateUI: UIObject
    {
        #region Constructors
        public QuizTemplateUI(UIContext context) : base(context)
        { }
        #endregion

        #region Methods

        /// <summary>
        /// Recives as parameter a List of question Id's and populate the QuizTemplate and QuizTemplateLinks tables.
        /// </summary>
        /// <param name="questionGuidList"></param>
        /// <param name="specialQuizName"></param>
        /// <returns>The Guid of the generated QuizTemplate Object</returns>
        public Guid MakeTemplate(Guid quizID, List<Guid> questionGuidList, string specialQuizName, TimeSpan? timespan=null)
        {
            return _context.Business.Objects.QuizTemplate.MakeTemplate(quizID, questionGuidList, specialQuizName, timespan);
        }

        /// <summary>
        /// Read all the questions belonging to the same QuizTemplate
        /// </summary>
        /// <param name="specialQuizUID">The ID of the QuizTemplate</param>
        /// <returns></returns>
        public List<Question> ReadAllQuestions(Guid templateQuizUID)
        {
            return _context.Business.Objects.QuizTemplate.ReadAllQuestions(templateQuizUID);
        }

        /// <summary>
        /// Return all the possible choices belonging to the question with the same Guid as the parameter.
        /// </summary>
        /// <param name="questionUID"></param>
        /// <returns></returns>
        public List<Choice> ReadAllChoices(Guid questionUID)
        {
            return _context.Business.Objects.QuizTemplate.ReadAllChoices(questionUID);
        }

        /// <summary>
        /// Return all the SpecialQuizes from the database.
        /// </summary>
        /// <returns></returns>
        public List<QuizTemplates> ReadAllQuizTemplates()
        {
            return _context.Business.Objects.QuizTemplate.ReadAllQuizTemplates();
        }

        public List<QuizTemplateView> ReadAllQuizTemplateView(Guid userID,int? pageNumber =null, int? rowPerPage = null,string sortExpression = null)
        {
            return _context.Business.Objects.QuizTemplate.ReadAllQuizTemplateView(userID,pageNumber, rowPerPage, sortExpression);
        }

        public int CountQuizTemplate(Guid _userGuid)
        {
            return _context.Business.Objects.QuizTemplate.Count(_userGuid);
        }
        public List<QuizTemplates> ReadAllPredefinedTests()
        {
            return _context.Business.Objects.QuizTemplate.ReadAllPredefinedTests();
        }
        #endregion

    }
}
