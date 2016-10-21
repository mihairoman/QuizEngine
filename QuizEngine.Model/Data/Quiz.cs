using System;

namespace QuizEngine.Model.Data
{
    public class Quiz
    {
        #region Properties
        public Guid QuizUID { get; set; }
        public Guid LevelUID { get; set; }
        public Guid CategoryUID { get; set; }
        public TimeSpan? Time { get; set; }
        #endregion
    }
}
