using System.Collections.Generic;
using QuizEngine.Repository;
using QuizEngine.Model.Data;
using System;
using QuizEngine.Infrastructure;
using QuizEngine.Business.Core;
using QuizEngine.Model.View;


namespace QuizEngine.Business
{
    public class ArchivedQuestionBusiness : BusinessObject
    {
       
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        public ArchivedQuestionBusiness(BusinessContext repositoryContext) : base(repositoryContext) { }
        #endregion Constructor

        #region Methods
        /// <summary>
        /// Reads all <see cref="QuizEngine.Models.ArchivedQuestion"/>, it uses the repositoryContext in order to retrieve the repository object that will be used in order to read all ArchivedQuestions.
        /// from the Database 
        /// </summary>
        /// <returns>A List of <see cref="QuizEngine.Models.ArchivedQuestion"/></returns>
        public List<ArchivedQuestion> ReadAll()
        {
            return _context.Repository.Objects.ArchivedQuestion.ReadAll();
        }

        /// <summary>
        /// Reads a specific <see cref="QuizEngine.Model.Data.ArchivedQuestion"/> , it uses the repositoryContext in order to retrieve the repository object that will be used to read once ArchivedQuestion based on the GUID.
        /// </summary>
        /// <param name="archivedQuestionID">Of type <see cref="QuizEngine.Model.Data.ArchivedQuestion"/></param>
        /// <returns><see cref="QuizEngine.Model.Data.ArchivedQuestion"/></returns>
        public ArchivedQuestion ReadByID(Guid archivedQuestionID)
        {
            return _context.Repository.Objects.ArchivedQuestion.ReadByID(archivedQuestionID);
        }
                
        /// <summary>
        /// Deletes a <see cref="QuizEngine.Models.ArchivedQuestion"/>, it uses the repositoryContext in order to retrieve the repository object that will be used to delete.
        /// </summary>
        /// <param name="archivedQuestionID">Of type <see cref="QuizEngine.Models.ArchivedQuestion"/></param>
        public void Delete(Guid archivedQuestionID)
        {
            _context.Repository.Objects.ArchivedQuestion.Delete(archivedQuestionID);
        }

        public void Insert_ById(List<Guid> questionID, Guid quizID, List<Int16> rownumber)
        {
            for (var i = 0; i < questionID.Count; i++)
            {
                    _context.Repository.Objects.ArchivedQuestion.Insert_ById(questionID[i], quizID, rownumber[i]);
            }
        }

        /// <summary>
        /// Uses a platformContext object to create an ArchviedQuestionRepository instance, which reads all the ArchivedQuizes
        /// related to the specified QuizID
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public List<ArchivedQuestion> ReadAllByQuizID(Guid quizID)
        {
            return _context.Repository.Objects.ArchivedQuestion.ReadAllByQuizID(quizID);
        }

        /// <summary>
        /// Creates a List of QuizQuestionView model
        /// </summary>
        /// <param name="quizID">ID of the quiz</param>
        /// <returns></returns>
        public List<QuizQuestionView> GetQuizQuestionsByQuizID(Guid quizID)
        {
            List<QuizQuestionView> quizQuestions = new List<QuizQuestionView>();
            List<ArchivedQuestion> archivedQuestions = ReadAllByQuizID(quizID);

            /////////////////////////////////////////////// GETS GUIDS FROM LIST OF QUESTIONS
            List<Guid> questionGUIDS = new List<Guid>();
            foreach (ArchivedQuestion aq in archivedQuestions)
            {
                questionGUIDS.Add(aq.ArchivedQuestionID);
            }
            ///////////////////////////////////////////////

            List<ArchivedChoice> archivedChoices = _context.Repository.Objects.ArchivedChoice.ReadByQuestionIDS(questionGUIDS);

            foreach(ArchivedQuestion aq in archivedQuestions)
            {
                QuizQuestionView questionView = new QuizQuestionView() 
                {
                    QuestionID = aq.ArchivedQuestionID,
                    Text = aq.ArchivedQuestionText,
                    QuestionType = aq.QuestionType,
                    Choices = _context.Repository.Objects.ArchivedChoice.GetByQuestionID(aq.ArchivedQuestionID, archivedChoices)
                };
                quizQuestions.Add(questionView);
            }

            return quizQuestions;
        }
        #endregion Methods
    }
}
