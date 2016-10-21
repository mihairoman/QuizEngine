using QuizEngine.Model.Data;
using QuizEngine.Repository;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuizEngine
{
    /// <summary>
    /// Class QuestionTagsRepository
    /// </summary>
    public class QuestionTagRepository:BaseRepository<QuestionTag>
    {
        #region Constants
        private const string QUESTIONTAGS_READBYID = "dbo.QuestionTags_ReadByID";
        private const string QUESTIONTAGS_READALL = "dbo.QuestionTags_ReadAll";
        private const string QUESTIONTAGS_INSERT = "dbo.QuestionTags_Insert";
        private const string QUESTIONTAGS_UPDATE = "dbo.QuestionTags_Update";
        private const string QUESTIONTAGS_DELETE = "dbo.QuestionTags_Delete";
        private const string QUESTIONTAGS_QUESTIONBYTAGNAME = "dbo.QuestionTags_readQuestionsByTagName";
        private const string QUESTIONTAGS_DELETEBYQUESTIONID = "dbo.QuestionTags_DeleteByQuestionID";
        private const string QUESTIONTAGS_INSERTBYQUESTIONGUIDANDLISTOFTAGGUID = "dbo.QuestionTags_InsertByQuestionGUIDAndListOfTagGUID";
        #endregion Constants

        #region Constructors
        /// <summary>
        /// The constructor that calls the Baserepository Constructor with the connectionString.
        /// </summary>
        /// 
        public QuestionTagRepository(RepositoryContext context)
            : base(context)
        {
           
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// This is used to return all QuestionTags.
        /// </summary>
        /// <returns></returns>
        public List<QuestionTag> ReadAll()
        {
            return ExecuteReadCommand(QUESTIONTAGS_READALL, ReaderToModel);
        }
        
        /// <summary>
        /// Used to Add the new QuestionTag.
        /// </summary>
        /// <param name="questionTag">Based on this parameter, the method vuilds the neccessary parameters in order to insert a new QuestionTag.</param>
        public void Insert(QuestionTag questionTag)
        {
            SqlParameter[] param = {
                new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier){Value = questionTag.QuestionUID},
                new SqlParameter("@TagGUID", SqlDbType.UniqueIdentifier){Value = questionTag.TagUID}
                                   };
             ExecuteCommand(QUESTIONTAGS_INSERT, param);   
        }

        /// <summary>
        /// Adds all corresponding tags to a certain Question
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="tags"></param>
        public void InsertAllTagsForQuestion(Guid questionId, List<Guid> tags)
        {
            string tagsString = string.Join(",", tags);
            SqlParameter[] param = {
                new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier){Value = questionId},
                new SqlParameter("@TagsGUIDList", SqlDbType.NVarChar, -1){Value =tagsString}
                                   };
            ExecuteCommand(QUESTIONTAGS_INSERTBYQUESTIONGUIDANDLISTOFTAGGUID, param);   
        }
        /// <summary>
        /// This method is used to delete a questionTag based on the QuestionGUID and on the TagGUID.
        /// </summary>
        /// <param name="questionTag"></param>
        public void Delete(QuestionTag questionTag)
        {
                SqlParameter[] param = {
                new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier){Value = questionTag.QuestionUID},
                new SqlParameter("@TagGUID", SqlDbType.UniqueIdentifier){Value = questionTag.TagUID}
                                   };
            ExecuteCommand(QUESTIONTAGS_DELETE, param); 
            
        }

        public void DeleteByQuestionID(Question question) {
            SqlParameter param = new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier) { Value = question.QuestionGUID };
                                   
            ExecuteCommand(QUESTIONTAGS_DELETEBYQUESTIONID, param); 
        }

        public void ReadQuestionsByTagName(string tagName)
        {
            SqlParameter[] param = {
                new SqlParameter("@TagName", SqlDbType.NVarChar,50){Value = tagName}
                                   };
            ExecuteCommand(QUESTIONTAGS_QUESTIONBYTAGNAME, param); 
        }

        protected override QuestionTag ReaderToModel(SqlDataReader reader)
        {
            return new QuestionTag
            {
                QuestionUID = reader.GetGuid(0),
                TagUID = reader.GetGuid(1)
            };
        }
        #endregion Methods
    }
}
