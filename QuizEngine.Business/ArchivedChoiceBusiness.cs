using System;
using System.Collections.Generic;
using QuizEngine.Model.Data;
using QuizEngine.Repository;
using QuizEngine.Infrastructure;
using QuizEngine.Business.Core;

namespace QuizEngine.Business
{
    public class ArchivedChoiceBusiness : BusinessObject
    {

        #region Constructors
        /// <summary>
        /// Receives a repository context as a parameter and calls the constructor of the parent class
        /// </summary>
        /// <param name="repositoryContext"></param>
        public ArchivedChoiceBusiness(BusinessContext repositoryContext) : base(repositoryContext)
        {
        }
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns all the stored <see cref="QuizEngine.Models.ArchivedChoice"/>
        /// </summary>
        /// <returns>Returns a list of ArchivedChoice</returns>
        public List<ArchivedChoice> ReadAll()
        {
            return _context.Repository.Objects.ArchivedChoice.ReadAll();
        }

        /// <summary>
        /// Returns the <see cref="QuizEngine.Models.ArchivedChoice"/> specified by the given parameters
        /// </summary>
        /// <param name="choiceId"></param>
        /// <param name="questionId"></param>
        /// <returns>Returns <see cref="QuizEngine.Models.ArchivedChoice"/></returns>
        public ArchivedChoice ReadById(Guid choiceId, Guid questionId)
        {
            return _context.Repository.Objects.ArchivedChoice.ReadById(choiceId, questionId);
        }

        /// <summary>
        /// Deletes the <see cref="QuizEngine.Models.ArchivedChoice"/> specified by the parameters
        /// </summary>
        /// <param name="choiceId"></param>
        /// <param name="questionId"></param>
        public void Delete(Guid choiceId, Guid questionId)
        {
            _context.Repository.Objects.ArchivedChoice.Delete(choiceId, questionId);
        }

        /// <summary>
        /// Updates the <see cref="QuizEngine.Models.ArchivedChoice"/> specified in the parameters
        /// </summary>
        /// <param name="archivedChoice"></param>
        public void Update(ArchivedChoice archivedChoice)
        {
            _context.Repository.Objects.ArchivedChoice.Update(archivedChoice);
        }

        /// <summary>
        /// Adds a new <see cref="QuizEngine.Models.ArchivedChoice"/> 
        /// </summary>
        /// <param name="archivedChoice"></param>
        public void Insert(ArchivedChoice archivedChoice)
        {
            _context.Repository.Objects.ArchivedChoice.Insert(archivedChoice);
        }
              

        /// <summary>
        /// Returns a list of choices which belong to the specified questionGUIDS
        /// </summary>
        /// <param name="questionGUIDS"></param>
        /// <returns></returns>
        public List<ArchivedChoice> ReadByQuestionIDS(List<Guid> questionGUIDS)
            {
            return _context.Repository.Objects.ArchivedChoice.ReadByQuestionIDS(questionGUIDS);
        }
        #endregion Methods

    }
}
