using QuizEngine.Library;
using System;
namespace QuizEngine.Model.View
{
    /// <summary>
    /// Model used for display question information
    /// </summary>
    public class QuestionView
    {
        public Guid QuestionGUID { get; set; }
        public string Text { get; set; }
        public QuestionType QuestionType { get; set; }
        public string CategoryName { get; set; }
        public string LevelName { get; set; }
        public string TagName { get; set; }
    }
}
