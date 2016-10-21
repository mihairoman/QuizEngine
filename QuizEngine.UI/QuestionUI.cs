using QuizEngine.Business.Core;
using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using QuizEngine.Model.View;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizEngine.UI
{
    public class QuestionUI : UIObject
    {
        #region Constructors
        /// <summary>
        /// Creates a new QuestionBussiness 
        /// </summary>
        /// <param name="connectionString">connection String of to the database</param>
        /// <remarks>It will create a new QuestionRespository by the connection String <see cref="QuizEngine.Repository.QuestionRepository"/></remarks>
        public QuestionUI(UIContext context) : base(context) { }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns a list with all the Questions from the database</returns>
        public List<Question> ReadAll()
        {
            return _context.Business.Objects.Question.ReadAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionID">String that represents a UUID</param>
        /// <returns>Returns the question described by the ID if it doesen't find that question returns null</returns>
        public Question ReadByID(Guid questionID)
        {
            return _context.Business.Objects.Question.ReadByID(questionID);
        }

        /// <summary>
        /// Reads all question by a given difficulty
        /// </summary>
        /// <returns>Returns a list with all the Questions from the database having a certain difficulty</returns>
        public List<QuestionViewForQuiz> ReadByDifficulty(Int16 level, Int16 number, Int16 levelminus, Int16 levelplus, Int16 othernumber, string taglist, string category, string questionlist)
        {
            return _context.Business.Objects.Question.ReadByDifficulty(level, number, levelminus, levelplus, othernumber, taglist, category, questionlist);
        }

        /// <summary>
        /// Deletes a Question from DB
        /// </summary>
        /// <param name="question"><see cref="QuizEngine.Modelss.Question"/></param>
        public void Delete(Question question)
        {
            _context.Business.Objects.Question.Delete(question);
        }

        /// <summary>
        /// According to the data received from the webservice creates new question, tags, questiontags and choices
        /// and sends them to the corresponding business layer
        /// </summary>
        /// <param name="question"><see cref="QuizEngine.Model.Data.Question"/></param>
        /// <param name="tags">List of <see cref="QuizEngine.Model.Data.Tag"/></param>
        /// <param name="choices">List of <see cref="QuizEngine.Model.Data.Choice"/></param>
        public void Insert(Question question, List<Tag> tags, List<Choice> choices)
        {
            _context.Business.Objects.Question.Insert(question);

            foreach (Tag tag in tags.Where(t => t.TagUID == Guid.Empty))
            {
                tag.TagUID = Guid.NewGuid();
                _context.Business.Objects.Tag.Insert(tag);
            }

            _context.Business.Objects.QuestionTag.InsertAllTagsForQuestion(question.QuestionGUID, tags.Select(tag => tag.TagUID).ToList());

            foreach (Choice choice in choices)
            {
                choice.ChoiceGUID = Guid.NewGuid();
                _context.Business.Objects.Choice.Insert(choice);
            }
        }

        /// <summary>
        /// According to the data received from the webservice updates the question and delets and
        /// reinserts all tags,questiontags and choices and sends them to the corresponding business layer
        /// </summary>
        /// <param name="question"><see cref="QuizEngine.Model.Data.Question"/></param>
        /// <param name="tags">List of <see cref="QuizEngine.Model.Data.Tag"/></param>
        /// <param name="choices">List of <see cref="QuizEngine.Model.Data.Choice"/></param>
        public void Update(Question question, List<Tag> tags, List<Choice> choices)
        {
            _context.Business.Objects.Question.Update(question);
            _context.Business.Objects.QuestionTag.DeleteByQuestionID(question);
            _context.Business.Objects.Choice.DeleteByQuestionID(question);
            if (tags != null)
            {
                foreach (Tag tag in tags)
                {
                    if (tag.TagUID == Guid.Empty)
                    {
                        tag.TagUID = Guid.NewGuid();
                        _context.Business.Objects.Tag.Insert(tag);
                    }
                }
                _context.Business.Objects.QuestionTag.InsertAllTagsForQuestion(question.QuestionGUID, tags.Select(tag => tag.TagUID).ToList());
            }

            if (choices != null)
            {
                foreach (Choice choice in choices)
                {

                    choice.ChoiceGUID = Guid.NewGuid();
                    _context.Business.Objects.Choice.Insert(choice);
                }
            }
        }

        /// <summary>
        /// This method returns all questions based on the selected filters
        /// </summary>
        /// <param name="categoryUid"></param>
        /// <param name="levelUid"></param>
        /// <param name="tagUids"></param>
        /// <param name="typeUids"></param>
        /// <param name="sortExpression"></param>
        /// <param name="rowsPerPage"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public List<QuestionView> ReadAllFiltered(Guid categoryUid, Guid levelUid, List<Guid> tagUids, List<Int16> typeUids, string sortExpression, int rowsPerPage, int pageNumber)
        {
            return _context.Business.Objects.Question.ReadAllFiltered(categoryUid, levelUid, tagUids, typeUids, sortExpression, rowsPerPage, pageNumber);
        }

        /// <summary>
        /// This method returns only the number of questions that respect the filters
        /// </summary>
        /// <param name="categoryUid"></param>
        /// <param name="levelUid"></param>
        /// <param name="tagUids"></param>
        /// <param name="typeUids"></param>
        /// <returns></returns>
        public int CountAllFilteredQuestions(Guid categoryUid, Guid levelUid, List<Guid> tagUids, List<Int16> typeUids)
        {
            return _context.Business.Objects.Question.CountAllFilteredQuestions(categoryUid, levelUid, tagUids, typeUids);
        }

        /// <summary>
        /// This method returns the total nubmer of questions from the database
        /// </summary>
        /// <returns></returns>
        public int CountAllQuestions() {
            return _context.Business.Objects.Question.CountAllQuestions();
        }

        #endregion
    }
}
