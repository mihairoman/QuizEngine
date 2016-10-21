using QuizEngine.Business.Core;
using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using QuizEngine.Repository;
using System;
using System.Collections.Generic;

namespace QuizEngine.Business
{
    public class TagBusiness : BusinessObject
    {
        #region Constructors
        public TagBusiness(BusinessContext repositoryContext) : base(repositoryContext) { }
        #endregion

        #region Methods

        public List<Tag> ReadAll()
        {
            return _context.Repository.Objects.Tag.ReadAll();
        }

        public Tag ReadByID(Guid tag)
        {
            return _context.Repository.Objects.Tag.ReadByID(tag);
        }

        /// <summary>
        /// Computest the list of tags for a given question.
        /// </summary>
        /// <param name="questionID"> Question identifier. </param>
        /// <returns>A List of <see cref="QuizEngine.Models.Tag"/>.</returns>
        public List<Tag> ReadByQuestionID(Guid questionID)
        {
            return _context.Repository.Objects.Tag.ReadByQuestionID(questionID);
        }

        public void Update(Tag tag)
        {
            _context.Repository.Objects.Tag.Update(tag);
        }

        public void Insert(Tag tag)
        {
            _context.Repository.Objects.Tag.Insert(tag);
        }

        public void Delete(Tag tag)
        {
            _context.Repository.Objects.Tag.Delete(tag);
        }

        #endregion Methods
    }
}
