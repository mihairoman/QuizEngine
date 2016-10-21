using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using QuizEngine.Infrastructure;
using QuizEngine.Model.View;
using QuizEngine.Business.Core;
using QuizEngine.UI.Core;
using QuizEngine.Model.Data.View;

namespace QuizEngine.UI
{
    public class QuizUI : UIObject
    {
        #region Constructors
        public QuizUI(UIContext context) : base(context) { }
        #endregion

        #region Methods
        /// <summary>
        /// Return one Quiz object which has the uniqueidentifier equal with the method parameter.
        /// </summary>
        /// <param name="quizUID"></param>
        /// <returns>A Quiz object</returns>
        public Quiz ReadById(Guid quizUID)
        {
            return _context.Business.Objects.Quiz.ReadById(quizUID);
        }

        /// <summary>
        /// Return all the Quiz objects from the database.
        /// </summary>
        /// <returns>A list of Quiz objects</returns>
        public List<Quiz> ReadAll()
        {
            return _context.Business.Objects.Quiz.ReadAll();
        }
        public int CountAllRandom()
        {
            return _context.Business.Objects.Quiz.CountAllRandom();
        }

        public int CountAllPredefined()
        {
            return _context.Business.Objects.Quiz.CountAllPredefined();
        }
        public List<QuizesPerUserView> ReadAllQuizesPerUserView(Guid userUID)
        {
            return _context.Business.Objects.Quiz.ReadAllQuizesPerUserView(userUID);
        }

        /// <summary>
        /// Add a Quiz object to the database.
        /// </summary>
        /// <param name="quiz"></param>
        public void Insert(Quiz quiz)
        {
            _context.Business.Objects.Quiz.Insert(quiz);
        }

        /// <summary>
        /// Update a Quiz object having the uniqueidentifier equal with the parameter object UID. 
        /// </summary>
        /// <param name="quiz"></param>
        public void Update(Quiz quiz)
        {
            _context.Business.Objects.Quiz.Update(quiz);
        }

        /// <summary>
        /// Delete a Quiz object from the database, which has the uniqueidentifer equal with guid
        /// </summary>
        /// <param name="quizUID"></param>
        public void Delete(Guid quizUID)
        {
            _context.Business.Objects.Quiz.Delete(quizUID);
        }

        public List<QuizView> ReadAllViewQuizes(int? pageNumber = null, int? rowPerPage = null, string sortExpression = null, Guid? UserGuid = null)
        {
            return _context.Business.Objects.Quiz.ReadAllQuizAdminView(pageNumber,rowPerPage,sortExpression,UserGuid);
        }

        public List<QuizUserView> ReadAllQuizUserView()
        {
            return _context.Business.Objects.Quiz.ReadAllQuizUserView();
        }

        /// <summary>
        /// Count all the quiz view objects, and return thier number.
        /// If the optional parameter is specified then 
        /// it return only the quizview objects belonging to a specific user.
        /// </summary>
        /// <param name="UserGuid"></param>
        /// <returns></returns>
        public int CountQuizView(Guid? UserGuid = null)
        {
            return _context.Business.Objects.Quiz.CountQuizView(UserGuid);
        }
        #endregion
    }
}
