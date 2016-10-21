using QuizEngine.Business.Core;
using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using QuizEngine.Repository;
using System;
using System.Collections.Generic;

namespace QuizEngine.Business
{
    public class ChoiceBusiness : BusinessObject
    {
        #region Constructor
        /// <summary>
        /// The constructor receives the context as a paramter and calls the super with it as a paramter.
        /// </summary>
        /// <param name="platformContext"></param>
        public ChoiceBusiness(BusinessContext repositoryContext) : base(repositoryContext) { }
        #endregion

        #region Methods
        /// <summary>
        /// Reads all <see cref="QuizEngine.Models.Choice"/>, it uses the platformContext in order to retrieve the repository object that will be used in order to read all choices.
        /// from the Database 
        /// </summary>
        /// <returns>A List of <see cref="QuizEngine.Models.Choice"/></returns>
        public List<Choice> ReadAll()
        {
            return _context.Repository.Objects.Choice.ReadAll();
        }
        /// <summary>
        /// Reads a specific <see cref="QuizEngine.Model.Data.Choice"/> , it uses the platformContext in order to retrieve the repository object that will be used to read once choice based on the GUID.
        /// </summary>
        /// <param name="choice">Of type <see cref="QuizEngine.Model.Data.Choice"/></param>
        /// <returns><see cref="QuizEngine.Model.Data.Choice"/></returns>
        public Choice ReadByID(Guid choice)
        {
            return _context.Repository.Objects.Choice.ReadByID(choice);
        }
        /// <summary>
        /// Updates a <see cref="QuizEngine.Model.Data.Choice"/>, it uses the platformContext in order to retrieve the repository object that will be used to update.
        /// </summary>
        /// <param name="choice">Of type <see cref="QuizEngine.Model.Data.Choice"/></param>
        public void Update(Choice choice)
        {
            _context.Repository.Objects.Choice.Update(choice);
        }
        /// <summary>
        /// Inserts a new <see cref="QuizEngine.Models.Choice"/>, , it uses the platformContext in order to retrieve the repository object that will be used to insert a choice.
        /// </summary>
        /// <param name="choice">Of type <see cref="QuizEngine.Models.Choice"/></param>
        public void Insert(Choice choice)
        {
            _context.Repository.Objects.Choice.Insert(choice);
        }
        /// <summary>
        /// Deletes a <see cref="QuizEngine.Models.Choice"/>, it uses the platformContext in order to retrieve the repository object that will be used to delete.
        /// </summary>
        /// <param name="choice">Of type c.Choice"/></param>
        public void Delete(Choice choice)
        {
            _context.Repository.Objects.Choice.Delete(choice);
        }
        /// <summary>
        /// Deletes all <see cref="QuizEngine.Models.Data.Choice"/> by the QuestionID
        /// </summary>
        /// <param name="question"><see cref="QuizEngine.Models.Data.Question"/></param>
        public void DeleteByQuestionID(Question question) 
        {
            _context.Repository.Objects.Choice.DeleteByQuestionID(question);
        }

        /// <summary>
        /// This method returns all the Chocies based on the QuestionGUID.
        /// </summary>
        /// <param name="guid">Using this guid it can return all the neccesary chocies.</param>
        /// <returns>A list of /// <param name="choice">Of type <see cref="QuizEngine.Models.Choice"/>.</returns>
        public List<Choice> ReadChoiceByQuestionID(Guid questionGuid)
        {
            return _context.Repository.Objects.Choice.ReadChoiceByQuestionID(questionGuid);
        }
        #endregion Methods
    }
}