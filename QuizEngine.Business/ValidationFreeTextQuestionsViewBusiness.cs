using QuizEngine.Infrastructure;
using QuizEngine.Model.Data.View;
using QuizEngine.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizEngine.Library;
using QuizEngine.Business.Core;
using QuizEngine.Model.Data;

namespace QuizEngine.Business
{
    public class ValidationFreeTextQuestionsViewBusiness : BusinessObject
    {
          #region Constructors
            /// <summary>
            /// Constructor for a new Answer Business. Also calls the default constructor.
            /// </summary>
            /// <param name="connectionString">String for the database connection, used for creating a new <see cref="DatabseConnection.Repository.AnswerRepository">Answer Repository</see>.</param>
            public ValidationFreeTextQuestionsViewBusiness(BusinessContext businessContext): base(businessContext)
            {}
            #endregion Constructors

        
          #region Methods
            /// <summary>
            /// ReadFreeTextQuestions returns all the questions where the type is free text
            /// </summary>
            /// <returns>Returns a list with all the FreeTextQuizes from the database</returns>
            public List<ValidationFreeTextQuestionsView> ReadFreeTextQuestions(Guid quizUID, Guid userUID)
            {
                List<ValidationFreeTextQuestionsView> questions_list= _context.Repository.Objects.ValidationFreeTextQuestionsView.ReadFreeTextQuestions(quizUID,userUID);
                List<ValidationFreeTextQuestionsView> new_list = new List<ValidationFreeTextQuestionsView>();
                for (int i = 0; i < questions_list.Count; i++)
                {
                    if (Convert.ToInt16(QuestionType.FreeText)==questions_list[i].QuestionType)
                    {
                        new_list.Add(questions_list[i]);
                    }
                }
                return new_list;
            }
                /// <summary>
                /// 
                /// </summary>
                /// <param name="final_list"></param>
            public void UpdateFreeTextQuestionsList(List<ValidationFreeTextQuestionsView> finalList)
            {                
                foreach (var element in finalList)
                {
                    _context.Repository.Objects.Answer.UpdateGrade(new Answer()
                        {
                            QuizUID = element.QuizUID,
                            ArchivedQuestionUID = element.ArchivedQuestionUID,
                            ArchivedChoiceUID = element.ArchivedChoiceUID,
                            UserUID = element.UserUID,
                            Grade = Convert.ToDouble(element.Grade)
                        });
                }               
            }
      
        #endregion
    }
}
