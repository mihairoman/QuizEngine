using QuizEngine.Model.Data;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizEngine.ClassicWebForms.Utility
{
    /// <summary>
    /// Base class for file writers 
    /// Collects the needed data for the writers
    /// </summary>
    public abstract class QuizDocumentWriter
    {
        #region Members
        public List<ArchivedQuestion> _questionList;
        public Dictionary<Guid, List<ArchivedChoice>> _questionAndChoices = new Dictionary<Guid, List<ArchivedChoice>>();
        public UIContext _uiContext;
        public string _userName;
        #endregion Members

        public QuizDocumentWriter(UIContext uiContext)
        {
            _uiContext = uiContext;
        }

        #region Methods

        /// <summary>
        /// Method to generate a stream of data containing a file
        /// </summary>
        /// <returns>Returns the stream of data</returns>
        public abstract byte[] GenerateFile();

        /// <summary>
        /// Method that fills the dictionary of choices based on the question ids of a given quiz
        /// </summary>
        /// <param name="quizUID">the id of the quiz</param>
        /// <param name="userName">the name of the creator</param>
        public void GenerateDictionary(Guid quizUID, string userName)
        {
            _userName = userName;

            List<ArchivedChoice> choiceList = new List<ArchivedChoice>();

            _questionList = _uiContext.Objects.ArchivedQuestion.ReadAllByQuizID(quizUID);

            if (_questionList.Count > 0)
            {
                List<Guid> questionIdList = (List<Guid>)_questionList.Select(it => it.ArchivedQuestionID).ToList();

                choiceList = _uiContext.Objects.ArchivedChoice.ReadByQuestionIDS(questionIdList);

                if (choiceList.Count > 0)
                {
                    foreach (var choiceListItem in choiceList)
                    {
                        if (_questionAndChoices.ContainsKey(choiceListItem.QuestionID))
                        {
                            _questionAndChoices[choiceListItem.QuestionID].Add(choiceListItem);
                        }
                        else
                        {
                            List<ArchivedChoice> choiceAuxlist = new List<ArchivedChoice>();
                            choiceAuxlist.Add(choiceListItem);
                            _questionAndChoices.Add(choiceListItem.QuestionID, choiceAuxlist);
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}