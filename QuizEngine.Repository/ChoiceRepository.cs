using QuizEngine.Model.Data;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QuizEngine.Repository
{
    public class ChoiceRepository : BaseRepository<Choice>
    {
        #region Constants
        private const string CHOICES_READBYID = "dbo.Choices_ReadByID";
        private const string CHOICES_READALL = "dbo.Choices_ReadAll";
        private const string CHOICES_INSERT = "dbo.Choices_Insert";
        private const string CHOICES_UPDATE = "dbo.Choices_Update";
        private const string CHOICES_DELETE = "dbo.Choices_Delete";
        private const string CHOICES_READBYQUESTIONID = "dbo.Choices_ReadByQuestionID";
        private const string CHOICES_DELETEBYQUESTIONID = "dbo.Choices_DeleteByQuestionID";
        #endregion

        #region Constructors
        /// <summary>
        /// The constructor that calls the Baserepository Constructor with the connectionString.
        /// </summary>
        public ChoiceRepository(RepositoryContext context) : base(context) { }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Used to Add the new choice.
        /// </summary>
        /// <param name="choice">Based on this parameter, the method vuilds the neccessary parameters in order to insert a new Choice.</param>
        public void Insert(Choice choice)
        {
            List<SqlParameter> choiceParam = new List<SqlParameter>();
            
                choiceParam.Add(new SqlParameter("@ChoiceGUID", SqlDbType.UniqueIdentifier) { Value = choice.ChoiceGUID });
                choiceParam.Add(new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier) { Value = choice.QuestionGUID });
                choiceParam.Add(new SqlParameter("@AnswerText", SqlDbType.NVarChar,-1) { Value = choice.AnswerText });
            if(choice.Value != null)
                choiceParam.Add(new SqlParameter("@Value", SqlDbType.Decimal) { Value = (Decimal) choice.Value });
            if (choice.IsCorrect != null)
                choiceParam.Add(new SqlParameter("@IsCorrect", SqlDbType.Bit) { Value = choice.IsCorrect });
            choiceParam.Add(new SqlParameter("@ChoicePosition", SqlDbType.Int) { Value = choice.ChoicePosition });
            
            ExecuteCommand(CHOICES_INSERT, choiceParam.ToArray());
        }

        /// <summary>
        /// Used to update an existing choice.
        /// </summary>
        /// <param name="choice">This is used to create the parameters for the stored procedure. The ChoiceGUID should not be changed.</param>
        public void Update(Choice choice)
        {
            SqlParameter[] choiceParam = {
                new SqlParameter("@ChoiceGUID", SqlDbType.UniqueIdentifier) { Value = choice.ChoiceGUID },
                new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier) { Value = choice.QuestionGUID },
                new SqlParameter("@AnswerText", SqlDbType.NVarChar,-1) { Value = choice.AnswerText },
                new SqlParameter("@Value", SqlDbType.Decimal) { Value = (Decimal) choice.Value },
                new SqlParameter("@IsCorrect", SqlDbType.Bit){ Value = choice.IsCorrect }
            };
            ExecuteCommand(CHOICES_UPDATE, choiceParam);
        }

        /// <summary>
        /// This method is used to delete a choice based on the ChoiceGUID.
        /// </summary>
        /// <param name="choice"></param>
        public void Delete(Choice choice)
        {
            SqlParameter[] choiceParam = {
                new SqlParameter("@ChoiceGUID", SqlDbType.UniqueIdentifier) { Value = choice.ChoiceGUID }
            };
            ExecuteCommand(CHOICES_DELETE, choiceParam);
        }

        /// <summary>
        /// Deletes all choices corresponding to a certain Question
        /// </summary>
        /// <param name="question"></param>
        public void DeleteByQuestionID(Question question)
        {
            SqlParameter[] questionParam = {
                new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier) { Value = question.QuestionGUID}
            };
            ExecuteCommand(CHOICES_DELETEBYQUESTIONID, questionParam);
        }

        /// <summary>
        /// This is used to return all choices.
        /// </summary>
        /// <returns></returns>
        public List<Choice> ReadAll()
        {
            return ExecuteReadCommand(CHOICES_READALL, ReaderToModel);
        }

        /// <summary>
        /// Based on the guid offered this method returns the choice.
        /// </summary>
        /// <param name="choiceGUID">This parameters determines what choice will be returned.</param>
        /// <returns></returns>
        public Choice ReadByID(Guid choiceGUID)
        {
            SqlParameter choiceParam = new SqlParameter("@ChoiceGUID", SqlDbType.UniqueIdentifier) { Value = choiceGUID };
            return ExecuteReadCommand(CHOICES_READBYID, ReaderToModel, choiceParam).SingleOrDefault();
        }

        /// <summary>
        /// Used to read the choices by the GUID of the question.
        /// </summary>
        /// <param name="questionGuid">The GUID needed to get all the choices.</param>
        /// <returns></returns>
        public List<Choice> ReadChoiceByQuestionID(Guid questionGuid)
        {
            SqlParameter choiceParam = new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier) { Value = questionGuid };
            return ExecuteReadCommand(CHOICES_READBYQUESTIONID, ReaderToModel, choiceParam);
        }


        /// <summary>
        /// Overriden method that returns a Choice.
        /// </summary>
        /// <param name="reader">The reader parameter is needed in order to retrieve the Choice from the database.</param>
        /// <returns></returns>
        protected override Choice ReaderToModel(SqlDataReader reader)
        {
            Choice choice = new Choice();
            choice.ChoiceGUID = reader.GetGuid(0);
            choice.QuestionGUID = reader.GetGuid(1);
            if (reader.IsDBNull(2))
                choice.AnswerText = "";
            else
                choice.AnswerText = reader.GetString(2);
            if (reader.IsDBNull(3))
                choice.Value = null;
            else
                choice.Value = reader.GetDecimal(3);
            if (reader.IsDBNull(4))
                choice.IsCorrect = null;
            else
                choice.IsCorrect = reader.GetBoolean(4);
            return choice;
            
        }
        #endregion Methods

    }
}