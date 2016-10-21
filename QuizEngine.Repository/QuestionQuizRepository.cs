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
    public class QuestionQuizRepository : BaseRepository<QuestionViewForQuiz>
    {
        #region Constants
        private const string QUESTIONS_READBYDIFFICULTY = "dbo.Levels_ReadByDifficulty";
        #endregion Constants

        #region Contructor
        public QuestionQuizRepository(RepositoryContext context)
            : base(context)
        {

        }
        #endregion Contructor

        #region Methods

        public List<QuestionViewForQuiz> ReadByDifficulty(Int16 level, Int16 number, Int16 levelminus, Int16 levelplus, Int16 othernumber, string taglist, string category, string questionlist)
        {
            SqlParameter[] param = {
               new SqlParameter("@Difficulty", SqlDbType.SmallInt){Value = level},
               new SqlParameter("@Number", SqlDbType.SmallInt){Value = number},
               new SqlParameter("@Difminus", SqlDbType.SmallInt){Value = levelminus},
               new SqlParameter("@Difplus", SqlDbType.SmallInt){Value = levelplus},
               new SqlParameter("@Num", SqlDbType.SmallInt){Value = othernumber},
               new SqlParameter("@TagList", SqlDbType.NVarChar,50){Value = taglist},
               new SqlParameter("@Category", SqlDbType.NVarChar,50){Value = category},
               new SqlParameter("@Questionlist", SqlDbType.NVarChar,50){Value = questionlist}
            };

            return ExecuteReadCommand(QUESTIONS_READBYDIFFICULTY, ReaderToModel, param);
        }

        protected override QuestionViewForQuiz ReaderToModel(System.Data.SqlClient.SqlDataReader reader)
        {
            string _selectedType;
            QuestionType type = (QuestionType)reader.GetInt16(2);

                QuestionType questionType = (QuestionType)Enum.Parse(typeof(QuestionType), type.ToString());
                _selectedType = questionType.ToString();

            if (reader != null && reader.HasRows)
            {
                return new QuestionViewForQuiz
                {
                    QuestionGUID = reader.GetGuid(0),
                    Text = reader.GetString(1),
                    QuestionType = _selectedType,
                    CategoryGUID = reader.GetGuid(3),
                    LevelGUID = reader.GetGuid(4),
                    TagName = reader.GetString(5)
                };
            }
            return null;

        #endregion Methods
        }

    }
}
