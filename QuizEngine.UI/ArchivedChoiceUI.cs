using QuizEngine.Model.Data;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;

namespace QuizEngine.UI
{
    public class ArchivedChoiceUI : UIObject
    {
        #region Constructors
        /// <summary>
        /// Receives a repository context as a parameter and calls the constructor of the parent class
        /// </summary>
        /// <param name="repositoryContext"></param>
        public ArchivedChoiceUI(UIContext context) : base(context) { }
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns all the stored <see cref="QuizEngine.Models.ArchivedChoice"/>
        /// </summary>
        /// <returns>Returns a list of ArchivedChoice</returns>
        public List<ArchivedChoice> ReadAll()
        {
            return _context.Business.Objects.ArchivedChoice.ReadAll();
        }

        /// <summary>
        /// Returns the <see cref="QuizEngine.Models.ArchivedChoice"/> specified by the given parameters
        /// </summary>
        /// <param name="choiceId"></param>
        /// <param name="questionId"></param>
        /// <returns>Returns <see cref="QuizEngine.Models.ArchivedChoice"/></returns>
        public ArchivedChoice ReadById(Guid choiceId, Guid questionId)
        {
            return _context.Business.Objects.ArchivedChoice.ReadById(choiceId, questionId);
        }

        /// <summary>
        /// Deletes the <see cref="QuizEngine.Models.ArchivedChoice"/> specified by the parameters
        /// </summary>
        /// <param name="choiceId"></param>
        /// <param name="questionId"></param>
        public void Delete(Guid choiceId, Guid questionId)
        {
            _context.Business.Objects.ArchivedChoice.Delete(choiceId, questionId);
        }

        /// <summary>
        /// Updates the <see cref="QuizEngine.Models.ArchivedChoice"/> specified in the parameters
        /// </summary>
        /// <param name="archivedChoice"></param>
        public void Update(ArchivedChoice archivedChoice)
        {
            _context.Business.Objects.ArchivedChoice.Update(archivedChoice);
        }

        /// <summary>
        /// Adds a new <see cref="QuizEngine.Models.ArchivedChoice"/> 
        /// </summary>
        /// <param name="archivedChoice"></param>
        public void Insert(ArchivedChoice archivedChoice)
        {
            _context.Business.Objects.ArchivedChoice.Insert(archivedChoice);
        }

        /// <summary>
        /// Returns a list of choices which belong to the specified questionGUIDS
        /// </summary>
        /// <param name="questionGUIDS"></param>
        /// <returns></returns>
        public List<ArchivedChoice> ReadByQuestionIDS(List<Guid> questionGUIDS)
        {
            return _context.Business.Objects.ArchivedChoice.ReadByQuestionIDS(questionGUIDS);
        }
        #endregion Methods


    }
}
