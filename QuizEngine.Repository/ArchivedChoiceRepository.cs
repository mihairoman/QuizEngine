using QuizEngine.Model.Data;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QuizEngine.Repository
{
    /// <summary>
    /// The repository class of the ArchivedChoice model
    /// </summary>
    public class ArchivedChoiceRepository : BaseRepository<ArchivedChoice>
    {
        #region Constants
        private const string ARCHIVED_CHOICE_READ_ALL = "dbo.ArchivedChoices_ReadAll";
        private const string ARCHIVED_CHOICE_READ_BY_ID = "dbo.ArchivedChoices_ReadByID";
        private const string ARCHIVED_CHOICE_INSERT = "dbo.ArchivedChoices_Insert";
        private const string ARCHIVED_CHOICE_DELETE = "dbo.ArchivedChoices_Delete";
        private const string ARCHIVED_CHOICE_UPDATE = "dbo.ArchivedChoices_Update";
        private const string ARCHIVED_CHOICE_READ_BY_QUESTION_IDS = "dbo.ArchivedChoices_ReadByQuestionIDS";
        //END OF STORED PROCEDURES
        #endregion 

        #region Constructors
        public ArchivedChoiceRepository(RepositoryContext context) : base(context) { }
                #endregion Constructors

        #region Methods
        /// <summary>
        /// Returns a list of all the data entries in the QuizEngine taBusinesse
        /// </summary>
        /// <returns>Returns List<QuizEngine></returns>
        public List<ArchivedChoice> ReadAll()
        {
            return ExecuteReadCommand(ARCHIVED_CHOICE_READ_ALL,ReaderToModel);
        }

        /// <summary>
        /// Returns an ArchivedChoice with the keys specified by the parameters
        /// </summary>
        /// <param name="choiceID"></param>
        /// <param name="questionID"></param>
        /// <returns>Returns ArchivedChoice</returns>
        public ArchivedChoice ReadById(Guid choiceID, Guid questionID)
        {
            SqlParameter choiceGuid = new SqlParameter("@ChoiceGUID", SqlDbType.UniqueIdentifier) { Value = choiceID };
            SqlParameter archivedQuestionGuid = new SqlParameter("@ArchivedQuestionGUID", SqlDbType.UniqueIdentifier) { Value = questionID };

            return ExecuteReadCommand(ARCHIVED_CHOICE_READ_BY_ID, ReaderToModel, choiceGuid,archivedQuestionGuid).SingleOrDefault();
        }

        /// <summary>
        /// Adds a new ArchivedChoice
        /// </summary>
        /// <param name="archivedChoice"></param>
        public void Insert(ArchivedChoice archivedChoice)
        {
            SqlParameter choiceGuid = new SqlParameter("@ChoiceGUID", SqlDbType.UniqueIdentifier) { Value = archivedChoice.ChoiceID };
            SqlParameter archivedQuestionGuid = new SqlParameter("@ArchivedQuestionGUID", SqlDbType.UniqueIdentifier) { Value = archivedChoice.QuestionID };
            SqlParameter answerText = new SqlParameter("@AnswerText", SqlDbType.NVarChar) { Value = archivedChoice.AnswerText };
            SqlParameter value = new SqlParameter("@Value", SqlDbType.Decimal) { Value = archivedChoice.Value };
            SqlParameter isCorrect = new SqlParameter("@IsCorrect", SqlDbType.Bit) { Value = archivedChoice.IsCorrect};
            SqlParameter choicePosition = new SqlParameter("@ChoicePosition", SqlDbType.Int) { Value = archivedChoice.ChoicePosition };
            ExecuteCommand(ARCHIVED_CHOICE_INSERT,choiceGuid,archivedQuestionGuid,answerText,value,isCorrect,choicePosition);
            
        }

        //}
        /// <summary>
        /// Updates the specified ArchivedChoice
        /// </summary>
        /// <param name="archivedChoice"></param>
        public void Update(ArchivedChoice archivedChoice)
        {     
            SqlParameter choiceGuid = new SqlParameter("@ChoiceGUID", SqlDbType.UniqueIdentifier) { Value = archivedChoice.ChoiceID };
            SqlParameter archivedQuestionGuid = new SqlParameter("@ArchivedQuestionGUID", SqlDbType.UniqueIdentifier) { Value = archivedChoice.QuestionID };
            SqlParameter answerText = new SqlParameter("@AnswerText", SqlDbType.NVarChar) { Value = archivedChoice.AnswerText};
            SqlParameter value = new SqlParameter("@Value", SqlDbType.Decimal) { Value = archivedChoice.Value };
            SqlParameter isCorrect = new SqlParameter("@IsCorrect", SqlDbType.Bit) { Value = archivedChoice.IsCorrect };
            ExecuteCommand(ARCHIVED_CHOICE_INSERT, choiceGuid, archivedQuestionGuid, answerText, value, isCorrect);
        }

        /// <summary>
        /// Deletes the ArchivedChoice specified by the parameters
        /// </summary>
        /// <param name="choiceID"></param>
        /// <param name="questionID"></param>
        public void Delete(Guid choiceID, Guid questionID)
        {
            
            SqlParameter choiceGuid = new SqlParameter("@ChoiceGUID", SqlDbType.UniqueIdentifier) { Value = choiceID };
            SqlParameter archivedQuestionGuid = new SqlParameter("@ArchivedQuestionGUID", SqlDbType.UniqueIdentifier) { Value = questionID };

            ExecuteCommand(ARCHIVED_CHOICE_DELETE, choiceGuid,archivedQuestionGuid);
        }

        /// <summary>
        /// Receives as an argument a list of Question IDs. Transforms the
        /// received list in a string and sets it as a stored procedure parameter.
        /// /// </summary>
        /// <param name="questionGUIDS"></param>
        /// <returns> A list of all the ArchivedChoices related to the list of Question IDs</returns>
        public List<ArchivedChoice> ReadByQuestionIDS(List<Guid> questionGUIDS)
        {
            string guids = string.Join(",", questionGUIDS);
            SqlParameter questGUIDS = new SqlParameter("@QuestionGUIDS", SqlDbType.NVarChar) { Value = guids};
            return ExecuteReadCommand(ARCHIVED_CHOICE_READ_BY_QUESTION_IDS, ReaderToModel, questGUIDS);
        }

        /// <summary>
        /// Searches the ArchivedChoices list, for choices with the same QuestionID as the one given as an argument.
        /// </summary>
        /// <param name="questionID"></param>
        /// <param name="archivedChoices"></param>
        /// <returns>List of ArchivedChoices</returns>
        public List<ArchivedChoice> GetByQuestionID(Guid questionID, List<ArchivedChoice> archivedChoices)
        {
            List<ArchivedChoice> questionChoices = new List<ArchivedChoice>();
            foreach (ArchivedChoice ac in archivedChoices)
            {
                if (Guid.Equals(ac.QuestionID, questionID))
                {
                    questionChoices.Add(ac);
                }
            }
            return questionChoices;
        }

        protected override ArchivedChoice ReaderToModel(SqlDataReader reader)
        {
            ArchivedChoice archivedChoice = new ArchivedChoice();
            archivedChoice.ChoiceID = reader.GetGuid(0);
            archivedChoice.QuestionID = reader.GetGuid(1);
            if (reader.IsDBNull(2))
                archivedChoice.AnswerText = "";
            else
                archivedChoice.AnswerText = reader.GetString(2);
            if (reader.IsDBNull(3))
                archivedChoice.Value = null;
            else
                archivedChoice.Value = reader.GetDecimal(3);
            if (reader.IsDBNull(4))
                archivedChoice.IsCorrect = null;
            else
                archivedChoice.IsCorrect = reader.GetBoolean(4);
            return archivedChoice;
        }
        #endregion Methods
    }
}
