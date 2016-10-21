using QuizEngine.Business.Core;
using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;

namespace QuizEngine.UI
{
    public class ChoiceUI : UIObject
    {
        #region Constructor
        /// <summary>
        /// The constructor receives the context as a paramter and calls the super with it as a paramter.
        /// </summary>
        /// <param name="platformContext"></param>
        public ChoiceUI(UIContext context) : base(context) { }
        #endregion

        #region Methods
        /// <summary>
        /// Reads all <see cref="QuizEngine.Models.Choice"/>, it uses the platformContext in order to retrieve the repository object that will be used in order to read all choices.
        /// from the Database 
        /// </summary>
        /// <returns>A List of <see cref="QuizEngine.Models.Choice"/></returns>
        public List<Choice> ReadAll()
        {
            return _context.Business.Objects.Choice.ReadAll();
        }
        /// <summary>
        /// Reads a specific <see cref="QuizEngine.Model.Data.Choice"/> , it uses the platformContext in order to retrieve the repository object that will be used to read once choice based on the GUID.
        /// </summary>
        /// <param name="choice">Of type <see cref="QuizEngine.Model.Data.Choice"/></param>
        /// <returns><see cref="QuizEngine.Model.Data.Choice"/></returns>
        public Choice ReadByID(Guid choice)
        {
            return _context.Business.Objects.Choice.ReadByID(choice);
        }

        /// <summary>
        /// Updates a <see cref="QuizEngine.Model.Data.Choice"/>, it uses the platformContext in order to retrieve the repository object that will be used to update.
        /// </summary>
        /// <param name="choice">Of type <see cref="QuizEngine.Model.Data.Choice"/></param>
        public void Update(Choice choice)
        {
            _context.Business.Objects.Choice.Update(choice);
        }
        /// <summary>
        /// Inserts a new <see cref="QuizEngine.Models.Choice"/>, , it uses the platformContext in order to retrieve the repository object that will be used to insert a choice.
        /// </summary>
        /// <param name="choice">Of type <see cref="QuizEngine.Models.Choice"/></param>
        public void Insert(Choice choice)
        {
            _context.Business.Objects.Choice.Insert(choice);
        }
        /// <summary>
        /// Deletes a <see cref="QuizEngine.Models.Choice"/>, it uses the platformContext in order to retrieve the repository object that will be used to delete.
        /// </summary>
        /// <param name="choice">Of type <see cref="QuizEngine.Models.Choice"/></param>
        public void Delete(Choice choice)
        {
            _context.Business.Objects.Choice.Delete(choice);
        }

        /// <summary>
        /// This method returns all the Chocies based on the QuestionGUID.
        /// </summary>
        /// <param name="guid">Using this guid it can return all the neccesary chocies.</param>
        /// <returns>A list of /// <param name="choice">Of type <see cref="QuizEngine.Models.Choice"/>.</returns>
        public List<Choice> ReadChoiceByQuestionID(Guid questionGuid)
        {
            return _context.Business.Objects.Choice.ReadChoiceByQuestionID(questionGuid);
        }
        #endregion Methods
    }
}