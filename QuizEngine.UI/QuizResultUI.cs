using QuizEngine.UI.Core;
using System;

namespace QuizEngine.UI
{
    public class QuizResultUI : UIObject
    {

        public QuizResultUI(UIContext context) : base(context) { }

        /// <summary>
        /// Returns the final grade of a quiz
        /// </summary>
        /// <param name="takenQuizUID">id of the taken quiz</param>
        /// <param name="userUID">id of the user that has taken the quiz</param>
        /// <returns></returns>
        public Double GetResult(Guid takenQuizUID, Guid userUID)
        {
            return _context.Business.Objects.QuizResult.GetResult(takenQuizUID, userUID);
        }
    }
}
