using QuizEngine.Business.Core;
using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.UI
{
    public class QuestionTagUI : UIObject
    {
        #region Constructors
        /// <summary>
        /// The constructor receives the context as a parameter and calls the super with it as a paramter.
        /// </summary>
        /// <param name="repositoryContext"></param>
        public QuestionTagUI(UIContext context) : base(context) { }
        #endregion

        #region Methods
        /// <summary>
        /// Reads all <see cref="QuizEngine.Models.QuestionTag"/>, it uses the repositoryContext in order to retrieve the repository object that will be used in order to read all QuestionTags.
        /// from the Database 
        /// </summary>
        /// <returns>A List of <see cref="QuizEngine.Models.QuestionTag"/></returns>
        public List<QuestionTag> ReadAll()
        {
            return _context.Business.Objects.QuestionTag.ReadAll();
        }
        /// <summary>
        /// Inserts a new <see cref="QuizEngine.Models.QuestionTag"/>, , it uses the repositoryContext in order to retrieve the repository object that will be used to insert a QuestionTag.
        /// </summary>
        /// <param name="questionTag">Of type <see cref="QuizEngine.Models.QuestionTag"/></param>
        public void Insert(QuestionTag questionTag)
        {
            _context.Business.Objects.QuestionTag.Insert(questionTag);
        }
        /// <summary>
        /// Deletes a <see cref="QuizEngine.Models.QuestionTag"/>, it uses the repositoryContext in order to retrieve the repository object that will be used to delete.
        /// </summary>
        /// <param name="questionTag">Of type <see cref="QuizEngine.Models.QuestionTag"/></param>
        public void Delete(QuestionTag questionTag)
        {
            _context.Business.Objects.QuestionTag.Delete(questionTag);
        }

        #endregion Methods
    }
}
