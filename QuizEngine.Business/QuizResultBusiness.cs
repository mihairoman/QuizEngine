using QuizEngine.Business.Core;
using QuizEngine.Infrastructure;
using QuizEngine.Model.Data.View;
using QuizEngine.Model.View;
using QuizEngine.Repository;
using System;
using System.Collections.Generic;
namespace QuizEngine.Business
{
    /// <summary>
    /// Class that computes the final result of a quiz
    /// </summary>
    public class QuizResultBusiness:BusinessObject
    {
        #region Constructors
        public QuizResultBusiness(BusinessContext context) : base(context) { }
        #endregion Constructors 
        
        #region Methods

        /// <summary>
        /// Returns the final grade of a quiz
        /// </summary>
        /// <param name="takenQuizUID">id of the taken quiz</param>
        /// <param name="userUID">id of the user that has taken the quiz</param>
        /// <returns></returns>
        public Double GetResult(Guid takenQuizUID, Guid userUID)
        {
            Double finalResult = 0;
            List<QuizResultView> quizValidationViewList = _context.Repository.Objects.QuizResult.ReadResultsPerQuestion(takenQuizUID, userUID);
            if (quizValidationViewList.Count != 0)
            {
                foreach (var listElement in quizValidationViewList)
                {
                    finalResult += listElement.Result;
                }
            }
            return finalResult;
        }
       
        #endregion Methods
    }
}
