using QuizEngine.Business.Core;
using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Business
{
    public class QuestionTagBusiness : BusinessObject
    {

        #region Constructors
        /// <summary>
        /// The constructor receives the context as a parameter and calls the super with it as a paramter.
        /// </summary>
        /// <param name="repositoryContext"></param>
        public QuestionTagBusiness(BusinessContext repositoryContext) : base(repositoryContext) { }
        #endregion

        #region Methods
        /// <summary>
        /// Reads all <see cref="QuizEngine.Models.QuestionTag"/>, it uses the repositoryContext in order to retrieve the repository object that will be used in order to read all QuestionTags.
        /// from the Database 
        /// </summary>
        /// <returns>A List of <see cref="QuizEngine.Models.QuestionTag"/></returns>
        public List<QuestionTag> ReadAll()
        {
            return _context.Repository.Objects.QuestionTag.ReadAll();
        }
        /// <summary>
        /// Inserts a new <see cref="QuizEngine.Models.QuestionTag"/>, , it uses the repositoryContext in order to retrieve the repository object that will be used to insert a QuestionTag.
        /// </summary>
        /// <param name="questionTag">Of type <see cref="QuizEngine.Models.QuestionTag"/></param>
        public void Insert(QuestionTag questionTag)
        {
            _context.Repository.Objects.QuestionTag.Insert(questionTag);
        }

        /// <summary>
        /// Links a list of tags to a certain Question
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="tags"></param>
        public void InsertAllTagsForQuestion(Guid questionId, List<Guid> tags)
        {
            _context.Repository.Objects.QuestionTag.InsertAllTagsForQuestion(questionId, tags);
        }

        /// <summary>
        /// Deletes a <see cref="QuizEngine.Models.QuestionTag"/>, it uses the repositoryContext in order to retrieve the repository object that will be used to delete.
        /// </summary>
        /// <param name="questionTag">Of type <see cref="QuizEngine.Models.QuestionTag"/></param>
        public void Delete(QuestionTag questionTag)
        {
            _context.Repository.Objects.QuestionTag.Delete(questionTag);
        }

        /// <summary>
        /// Deletes all links between a question and its tags by QuestionId
        /// </summary>
        /// <param name="question"></param>
        public void DeleteByQuestionID(Question question) 
        {
            _context.Repository.Objects.QuestionTag.DeleteByQuestionID(question);
        }

        #endregion Methods
    }
}
