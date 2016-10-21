using QuizEngine.Library;
using System;

namespace QuizEngine.Model.View
{
    /// <summary>
    /// Model used to display question information by chosen difficulty
    /// </summary>
    public class QuestionViewForQuiz
    {
        public Guid QuestionGUID { get; set; }
        public string Text { get; set; }
        public string QuestionType { get; set; }
        public Guid CategoryGUID { get; set; }
        public Guid LevelGUID { get; set; }
        public string TagName { get; set; }
    }
}
