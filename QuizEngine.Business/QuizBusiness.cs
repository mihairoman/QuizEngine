using QuizEngine.Repository;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using QuizEngine.Infrastructure;
using QuizEngine.Model.View;
using QuizEngine.Business.Core;
using QuizEngine.Model.Data.View;
namespace QuizEngine.Business
{
    public class QuizBusiness: BusinessObject
    {
        #region Constructors
        public QuizBusiness(BusinessContext context)
            : base(context)
        { }
        #endregion

        #region Methods
        /// <summary>
        /// Return one Quiz object which has the uniqueidentifier equal with the method parameter.
        /// </summary>
        /// <param name="quizUID"></param>
        /// <returns>A Quiz object</returns>
        public Quiz ReadById(Guid quizUID)
        {
            return _context.Repository.Objects.Quiz.ReadById(quizUID);
        }

        /// <summary>
        /// Return all the Quiz objects from the database.
        /// </summary>
        /// <returns>A list of Quiz objects</returns>
        public List<Quiz> ReadAll()
        {
            return _context.Repository.Objects.Quiz.ReadAll();
        }

        public int CountAllRandom()
        {
            return _context.Repository.Objects.Quiz.CountAllRandom();
        }

        public int CountAllPredefined()
        {
            return _context.Repository.Objects.Quiz.CountAllPredefined();
        }


        public List<QuizesPerUserView> ReadAllQuizesPerUserView(Guid userUID)
        {
            return _context.Repository.Objects.QuizesPerUserView.ReadAllQuizesPerUserView(userUID);
        }

        /// <summary>
        /// Add a Quiz object to the database.
        /// </summary>
        /// <param name="quiz"></param>
        public void Insert(Quiz quiz)
        {
            _context.Repository.Objects.Quiz.Insert(quiz);
        }

        /// <summary>
        /// Update a Quiz object having the uniqueidentifier equal with the parameter object UID. 
        /// </summary>
        /// <param name="quiz"></param>
        public void Update(Quiz quiz)
        {
            _context.Repository.Objects.Quiz.Update(quiz);
        }

        /// <summary>
        /// Delete a Quiz object from the database, which has the uniqueidentifer equal with guid
        /// </summary>
        /// <param name="quizUID"></param>
        public void Delete(Guid quizUID)
        {
            _context.Repository.Objects.Quiz.Delete(quizUID);
        }

        public List<QuizView> ReadAllQuizAdminView(int? pageNumber = null,int? rowPerPage =null, string sortExpression = null, Guid? UserGuid = null)
        {
            return _context.Repository.Objects.QuizView.ReadAll(pageNumber,rowPerPage,sortExpression,UserGuid);
        }

        public List<QuizUserView> ReadAllQuizUserView()
        {
            return _context.Repository.Objects.QuizUserView.ReadAll();
        }

        public int CountQuizView(Guid? UserGuid = null)
        {
            return _context.Repository.Objects.QuizView.Count(UserGuid);
        }
        #endregion
    }
}
