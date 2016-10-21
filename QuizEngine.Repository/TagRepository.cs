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
    /// Class TagsRepository
    /// </summary>
    public class TagRepository : BaseRepository<Tag>
    {
        #region Constants
        private const string TAGS_READBYID = "dbo.Tags_ReadByID";
        private const string TAGS_READALL = "dbo.Tags_ReadAll";
        private const string TAGS_READALLBYQUESTIONID = "dbo.Tags_ReadAllByQuestionID";
        private const string TAGS_INSERT = "dbo.Tags_Insert";
        private const string TAGS_UPDATE = "dbo.Tags_Update";
        private const string TAGS_DELETE = "dbo.Tags_Delete";
        #endregion Constants

        #region Constructors
        /// <summary>
        /// Instantiates a <see cref="QuizEngine.Repository.TagRepository"/>.
        /// </summary>
        public TagRepository(RepositoryContext context)
            : base(context)
        {
 
        }
               
        #endregion Constructors

        #region Methods
        public Tag ReadByID(Guid tagUID)
        {
            SqlParameter tagUidParam = new SqlParameter("@TagGUID", SqlDbType.UniqueIdentifier)
            {
                Value = tagUID
            };
            return ExecuteReadCommand(TAGS_READBYID, ReaderToModel, tagUidParam).SingleOrDefault();
        }

        public List<Tag> ReadAll()
        {
            return ExecuteReadCommand(TAGS_READALL, ReaderToModel);
        }

        /// <summary>
        /// Computest the list of tags for a given question.
        /// </summary>
        /// <param name="questionID"> Question identifier. </param>
        /// <returns>A List of <see cref="QuizEngine.Models.Tag"/>.</returns>
        public List<Tag> ReadByQuestionID(Guid questionID)
        {
            SqlParameter questionUidParam = new SqlParameter("@QuestionGUID", SqlDbType.UniqueIdentifier)
            {
                Value = questionID
            };
            return ExecuteReadCommand(TAGS_READALLBYQUESTIONID, ReaderToModel, questionUidParam);
        }

        public void Insert(Tag tag)
        {

            SqlParameter[] param = {
                 new SqlParameter("@TagGUID", SqlDbType.UniqueIdentifier){Value = tag.TagUID},
                 new SqlParameter("@TagName", SqlDbType.NVarChar, 50){Value = tag.TagName}
            };
            ExecuteCommand(TAGS_INSERT, param);
        }

        public void Update(Tag tag)
        {

            SqlParameter[] param = {
                 new SqlParameter("@TagGUID", SqlDbType.UniqueIdentifier){Value = tag.TagUID},
                 new SqlParameter("@TagName", SqlDbType.NVarChar, 50){Value = tag.TagName}
            };
           ExecuteCommand(TAGS_UPDATE, param);
        }

        public void Delete(Tag tag)
        {

            SqlParameter[] param = {
                 new SqlParameter("@TagGUID", SqlDbType.UniqueIdentifier){Value = tag.TagUID},
            };
            ExecuteCommand(TAGS_DELETE, param);
        }

        protected override Tag ReaderToModel(SqlDataReader reader)
        {
            return new Tag
            {
                TagUID = reader.GetGuid(0),
                TagName = reader.GetString(1),
            };
        }

        #endregion Methods

    }
}
