using QuizEngine.Business.Core;
using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using QuizEngine.Model.View;
using QuizEngine.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizEngine.Business
{
    public class QuestionBusiness : BusinessObject
    {
        #region Constructors
        /// <summary>
        /// Creates a new QuestionBussiness 
        /// </summary>
        /// <param name="connectionString">connection String of to the database</param>
        /// <remarks>It will create a new QuestionRespository by the connection String <see cref="QuizEngine.Repository.QuestionRepository"/></remarks>
        public QuestionBusiness(BusinessContext repositoryContext) : base(repositoryContext) { }
        #endregion

        #region Methods
        /// <summary>
        /// Reads all questions
        /// </summary>
        /// <returns>Returns a list with all the Questions from the database</returns>
        public List<Question> ReadAll()
        {
            return _context.Repository.Objects.Question.ReadAll();
        }

        /// <summary>
        /// Reads all question by a given difficulty
        /// </summary>
        /// <returns>Returns a list with all the Questions from the database having a certain difficulty</returns>
        public List<QuestionViewForQuiz> ReadByDifficulty(Int16 level, Int16 number, Int16 levelminus, Int16 levelplus, Int16 othernumber, string taglist, string category, string questionlist)
        {
            return _context.Repository.Objects.QuestionQuizView.ReadByDifficulty(level, number, levelminus, levelplus, othernumber, taglist, category, questionlist);
        }

        /// <summary>
        /// Reads a question by a given ID
        /// </summary>
        /// <param name="questionID">String that represents a UUID</param>
        /// <returns>Returns the question described by the ID if it doesen't find that question returns null</returns>
        public Question ReadByID(Guid questionID)
        {
            return _context.Repository.Objects.Question.ReadByID(questionID);
        }

        /// <summary>
        /// Updates a Question
        /// </summary>
        /// <param name="question"><see cref="QuizEngine.Models.Question"/></param>
        public void Update(Question question)
        {
            _context.Repository.Objects.Question.Update(question);
        }

        /// <summary>
        /// Inserts a new Question in the DB
        /// </summary>
        /// <param name="question"><see cref="QuizEngine.Models.Question"/></param>
        public void Insert(Question question)
        {
            _context.Repository.Objects.Question.Insert(question);
        }

        /// <summary>
        /// Deletes a Question from DB
        /// </summary>
        /// <param name="question"><see cref="QuizEngine.Modelss.Question"/></param>
        public void Delete(Question question)
        {
            _context.Repository.Objects.Question.Delete(question);
        }

        /// <summary>
        /// This method reads all view filtered options.
        /// </summary>
        /// <param name="foundCategory"></param>
        /// <param name="foundLevel"></param>
        /// <param name="tagUids"></param>
        /// <param name="typeUids"></param>
        /// <returns></returns>
        public List<QuestionView> ReadAllFiltered(Guid categoryUid, Guid levelUid, List<Guid> tagUids, List<Int16> typeUids, string sortExpression, int rowsPerPage, int pageNumber)
        {
            //Linq Method Syntax
            string types = string.Join(",", typeUids.Select(Int16 => Int16.ToString()));
            string tags = string.Join(",", tagUids.Select(guid => guid.ToString()));

            // aici o sa avem 5 parametri: Guid, Guid, List<Guid>, List<Guid>, string sortExpression
            return _context.Repository.Objects.QuestionView.ReadAllViewFiltered(categoryUid, levelUid, tags, types, sortExpression, rowsPerPage, pageNumber);
        }

        /// <summary>
        /// This method is used in order to count how many questions respect the selected filters
        /// </summary>
        /// <param name="categoryUid"></param>
        /// <param name="levelUid"></param>
        /// <param name="tagUids"></param>
        /// <param name="typeUids"></param>
        /// <returns></returns>
        public int CountAllFilteredQuestions(Guid categoryUid, Guid levelUid, List<Guid> tagUids, List<Int16> typeUids)
        {
            //Linq Method Syntax
            string types = string.Join(",", typeUids.Select(Int16 => Int16.ToString()));
            string tags = string.Join(",", tagUids.Select(guid => guid.ToString()));

            // aici o sa avem 5 parametri: Guid, Guid, List<Guid>, List<Guid>, string sortExpression
            return _context.Repository.Objects.QuestionView.CountAllFilteredQuestions(categoryUid, levelUid, tags, types);
        }

        /// <summary>
        /// This method returns the total number of questions
        /// </summary>
        /// <returns></returns>
        public int CountAllQuestions()
        {

            return _context.Repository.Objects.QuestionView.CountAllQuestions();
        }

        #endregion Methods
    }
}
