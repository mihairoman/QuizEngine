using QuizEngine.Library;
using QuizEngine.Model.Data;
using QuizEngine.Model.View;
using QuizEngine.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuizEngine.Repository
{
    public class QuestionViewRepository : BaseRepository<QuestionView>
    {
        #region Constants
        private const string QUESTION_READALLFILTERED = "dbo.Question_ReadAllFiltered";
        private const string QUESTION_COUNTALLQUESTIONS = "dbo.Question_CountAllQuestions";
        private const string QUESTION_COUNTALLFILTEREDQUESTIONS = "dbo.Question_CountAllFilteredQuestions";
        #endregion Constants

        #region Contructor
        public QuestionViewRepository(RepositoryContext context)
            : base(context)
        {

        }
        #endregion Contructor

        #region Methods
        /// <summary>
        /// This method will return the total number of questions from the database
        /// </summary>
        /// <returns></returns>
        public int CountAllQuestions()
        {
            return Convert.ToInt32(ExecuteScalarCommand(QUESTION_COUNTALLQUESTIONS, null));
        }

        /// <summary>
        /// This method will return the number of questions that respect the selected fitlers
        /// </summary>
        /// <param name="categoryUid"></param>
        /// <param name="levelUid"></param>
        /// <param name="tagUids"></param>
        /// <param name="typeUids"></param>
        /// <returns></returns>
        public int CountAllFilteredQuestions(Guid categoryUid, Guid levelUid, string tagUids, string typeUids)
        {
            SqlParameter[] param = {
               categoryUid != Guid.Empty ? new SqlParameter("@Category", SqlDbType.UniqueIdentifier){Value = categoryUid} 
                                          : new SqlParameter("@Category", SqlDbType.UniqueIdentifier){Value = null},
               levelUid != Guid.Empty ? new SqlParameter("@Level", SqlDbType.UniqueIdentifier){Value = levelUid}
                                      : new SqlParameter("@Level", SqlDbType.UniqueIdentifier){Value = null},
               !string.IsNullOrEmpty(tagUids) ? new SqlParameter("@Tag", SqlDbType.NVarChar,500){Value = tagUids}
                                              : new SqlParameter("@Tag", SqlDbType.NVarChar,500){Value = null},
               !string.IsNullOrEmpty(typeUids) ? new SqlParameter("@Type", SqlDbType.NVarChar,500){Value = typeUids}
                                               : new SqlParameter("@Type", SqlDbType.NVarChar,500){Value = null},
            };
            return Convert.ToInt32(ExecuteScalarCommand(QUESTION_COUNTALLFILTEREDQUESTIONS, param));
        }

        public List<QuestionView> ReadAllViewFiltered(Guid categoryUid, Guid levelUid, string tagUids, string typeUids, string sortExpression, int rowsPerPage, int pageNumber)
        {
            SqlParameter[] param = {
               categoryUid != Guid.Empty ? new SqlParameter("@Category", SqlDbType.UniqueIdentifier){Value = categoryUid} 
                                          : new SqlParameter("@Category", SqlDbType.UniqueIdentifier){Value = null},
               levelUid != Guid.Empty ? new SqlParameter("@Level", SqlDbType.UniqueIdentifier){Value = levelUid}
                                      : new SqlParameter("@Level", SqlDbType.UniqueIdentifier){Value = null},
               !string.IsNullOrEmpty(tagUids) ? new SqlParameter("@Tag", SqlDbType.NVarChar,500){Value = tagUids}
                                              : new SqlParameter("@Tag", SqlDbType.NVarChar,500){Value = null},
               !string.IsNullOrEmpty(typeUids) ? new SqlParameter("@Type", SqlDbType.NVarChar,500){Value = typeUids}
                                               : new SqlParameter("@Type", SqlDbType.NVarChar,500){Value = null},
               !string.IsNullOrEmpty(sortExpression) ? new SqlParameter("@SortExpression", SqlDbType.NVarChar,500){Value = sortExpression}
                                                     : new SqlParameter("@SortExpression", SqlDbType.NVarChar,500){Value = null},

                new SqlParameter("@PageNumber", SqlDbType.Int){Value = pageNumber},
                new SqlParameter("@RowspPage", SqlDbType.Int){Value = rowsPerPage}
            };
            return ExecuteReadCommand(QUESTION_READALLFILTERED, ReaderToModel, param);
        }

        protected override QuestionView ReaderToModel(System.Data.SqlClient.SqlDataReader reader)
        {
            if (reader != null && reader.HasRows)
            {
                return new QuestionView
                {
                    QuestionGUID = reader.GetGuid(0),
                    Text = reader.GetString(1),
                    QuestionType = (QuestionType)reader.GetInt16(2),
                    CategoryName = reader.GetString(3),
                    LevelName = reader.GetString(4),
                    TagName = reader.GetString(5)
                };
            }
            return null;
        }
        #endregion Methods
    }
}
