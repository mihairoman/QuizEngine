
using System;
namespace QuizEngine.Model.View
{
    public class QuizUserView
    {
        #region Properties
        public Guid QuizUID { get; set; }
        public string LevelName { get; set; }
        public string CategoryName { get; set; }
        public int QuestionNumber { get; set; }
        #endregion
    }
}
