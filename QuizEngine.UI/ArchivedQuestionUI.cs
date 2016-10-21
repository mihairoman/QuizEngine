using System.Collections.Generic;
using QuizEngine.Model.Data;
using QuizEngine.Model.View;
using System;
using QuizEngine.Infrastructure;
using QuizEngine.Business.Core;
using QuizEngine.UI.Core;

namespace QuizEngine.UI
{
    public class ArchivedQuestionUI : UIObject
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        public ArchivedQuestionUI(UIContext context) : base(context) { }
        #endregion Constructor

        #region Methods
        /// <summary>
        /// Reads all <see cref="QuizEngine.Models.ArchivedQuestion"/>, it uses the repositoryContext in order to retrieve the repository object that will be used in order to read all ArchivedQuestions.
        /// from the Database 
        /// </summary>
        /// <returns>A List of <see cref="QuizEngine.Models.ArchivedQuestion"/></returns>
        public List<ArchivedQuestion> ReadAll()
        {
            return _context.Business.Objects.ArchivedQuestion.ReadAll();
        }
        /// <summary>
        /// Reads a specific <see cref="QuizEngine.Model.Data.ArchivedQuestion"/> , it uses the repositoryContext in order to retrieve the repository object that will be used to read once ArchivedQuestion based on the GUID.
        /// </summary>
        /// <param name="archivedQuestionID">Of type <see cref="QuizEngine.Model.Data.ArchivedQuestion"/></param>
        /// <returns><see cref="QuizEngine.Model.Data.ArchivedQuestion"/></returns>
        public ArchivedQuestion ReadByID(Guid archivedQuestionID)
        {
            return _context.Business.Objects.ArchivedQuestion.ReadByID(archivedQuestionID);
        }
        
        /// <summary>
        /// Deletes a <see cref="QuizEngine.Models.ArchivedQuestion"/>, it uses the repositoryContext in order to retrieve the repository object that will be used to delete.
        /// </summary>
        /// <param name="archivedQuestionID">Of type <see cref="QuizEngine.Models.ArchivedQuestion"/></param>
        public void Delete(Guid archivedQuestionID)
        {
            _context.Business.Objects.ArchivedQuestion.Delete(archivedQuestionID);
        }
        public void Insert_ById(List<Guid> questionID, Guid quizID, List<Int16> rownumber)
        { 
            _context.Business.Objects.ArchivedQuestion.Insert_ById(questionID, quizID, rownumber);
        } 
        /// <summary>
        /// Uses a platformContext object to create an ArchviedQuestionRepository instance, which reads all the ArchivedQuizes
        /// related to the specified QuizID
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public List<ArchivedQuestion> ReadAllByQuizID(Guid quizID)
        {
            return _context.Business.Objects.ArchivedQuestion.ReadAllByQuizID(quizID);
        }

        /// <summary>
        /// Calls the business method which creates a list of QuizQuestionView by combining
        /// the data from ArchivedQuestions and ArchivedChoices.
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns>List of <see cref="QuizEngine.Model.View.QuizQuestionView"/></returns>
        public List<QuizQuestionView> GetQuizQuestionsByQuizID(Guid quizID)
        {
            return _context.Business.Objects.ArchivedQuestion.GetQuizQuestionsByQuizID(quizID);
        }
        #endregion Methods
    }
}
