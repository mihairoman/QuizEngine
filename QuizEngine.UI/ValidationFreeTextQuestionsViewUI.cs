using QuizEngine.Library;
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

    public class ValidationFreeTextQuestionsViewUI : UIObject
    {
        #region Constructor
        public ValidationFreeTextQuestionsViewUI(UIContext context) : base(context) { }
        #endregion
        #region Methods
        /// <summary>
        /// ReadFreeTextQuestions returns all the questions where the type is free text
        /// </summary>
        /// <returns>Returns a list with all the FreeTextQuizes from the database</returns>
        public List<ValidationFreeTextQuestionsView> ReadFreeTextQuestions(Guid quizUID, Guid userUID)
        {
            List<ValidationFreeTextQuestionsView> questions_list = _context.Business.Objects.ValidationFreeTextQuestionsView.ReadFreeTextQuestions(quizUID, userUID);
            List<ValidationFreeTextQuestionsView> new_list = new List<ValidationFreeTextQuestionsView>();
            for (int i = 0; i < questions_list.Count; i++)
            {
                if (Convert.ToInt16(QuestionType.FreeText) == questions_list[i].QuestionType)
                {
                    new_list.Add(questions_list[i]);
                }
            }
            return new_list;
        }
        public void UpdateGrade(QuizUserLink quizUserLink)
        {
            _context.Business.Objects.QuizUserLink.UpdateFinalResult(quizUserLink);
        }
        public void UpdateFreeTextQuestionsList(List<ValidationFreeTextQuestionsView> final_list)
        {
            _context.Business.Objects.ValidationFreeTextQuestionsView.UpdateFreeTextQuestionsList(final_list);
        }
        #endregion
    }
}
