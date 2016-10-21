using QuizEngine.Business.Core;
using QuizEngine.Model.Data;
using QuizEngine.Model.Data.View;
using QuizEngine.Repository;
using System;
using System.Collections.Generic;
namespace QuizEngine.Business
{
    public class QuizTemplateBusiness:BusinessObject
    {
        #region Constructors
        public QuizTemplateBusiness(BusinessContext context):base(context)
        { }
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Recives as parameter a List of question Id's and populate the QuizTemplate and QuizTemplateLinks tables.
        /// </summary>
        /// <param name="questionGuidList"></param>
        /// <param name="specialQuizName"></param>
        /// <returns>The Guid of the generated QuizTemplate Object</returns>
        public Guid MakeTemplate(Guid quizID, List<Guid> questionGuidList, string specialQuizName, TimeSpan? timespan=null)
        {
            QuizTemplates quizTemplate = new QuizTemplates()
            {
                QuizTemplateUID = quizID,
                TypeName = specialQuizName,
                Time = timespan
            };
            Guid quizTemplateGuid = quizTemplate.QuizTemplateUID;
          _context.Repository.Objects.QuizTemplate.Insert(quizTemplate);
          QuizTemplateLinksRepository myRepository = _context.Repository.Objects.QuizTemplateLink;
          foreach (Guid myGuid in questionGuidList)
          {
              QuizTemplateLinks quizTemplateLink = new QuizTemplateLinks()
              {
                  QuestionUID = myGuid,
                  QuizTemplateUID = quizTemplateGuid
              };
              myRepository.Insert(quizTemplateLink);

          }
          return quizTemplateGuid;
        }

        /// <summary>
        /// Read all the questions belonging to the same QuizTemplate
        /// </summary>
        /// <param name="specialQuizUID">The ID of the QuizTemplate</param>
        /// <returns></returns>
        public  List<Question> ReadAllQuestions(Guid specialQuizUID)
        {
            return _context.Repository.Objects.Question.ReadByQuizTemplateId(specialQuizUID);
        }

        /// <summary>
        /// Return all the possible choices belonging to the question with the same Guid as the parameter.
        /// </summary>
        /// <param name="questionUID"></param>
        /// <returns></returns>
        public List<Choice> ReadAllChoices(Guid questionUID)
        {
            return _context.Repository.Objects.Choice.ReadChoiceByQuestionID(questionUID);
        }

        /// <summary>
        /// Return all the SpecialQuizes from the database.
        /// </summary>
        /// <returns></returns>
        public List<QuizTemplates> ReadAllQuizTemplates()
        {
            return _context.Repository.Objects.QuizTemplate.ReadAll();
        }

        public List<QuizTemplateView> ReadAllQuizTemplateView(Guid userID,int? pageNumber=null, int? rowPerPage=null, string sortExpression = null)
        {
            return _context.Repository.Objects.QuizTemplateView.Read(userID,pageNumber,rowPerPage,sortExpression);
        }

        public int Count(Guid _userGuid)
        {
            return _context.Repository.Objects.QuizTemplate.Count(_userGuid);
        }
        public List<QuizTemplates> ReadAllPredefinedTests()
        {
            return _context.Repository.Objects.QuizTemplate.ReadAllPredefinedTests();
        }
   } 
        #endregion Methods
}
