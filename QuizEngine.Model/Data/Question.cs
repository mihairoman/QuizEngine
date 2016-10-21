using QuizEngine.Library;
using System;

namespace QuizEngine.Model.Data
{
    
    public class Question
    {
        /// <summary>
        /// Unique Identifier of the Question
        /// </summary>
        public Guid QuestionGUID { get; set; }

        public string Text { get; set; }

        public QuestionType QuestionType { get; set; }

        public Guid CategoryGUID { get; set; }

        public Guid LevelGUID { get; set; }
    }
}
