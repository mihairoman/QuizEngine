using System;
namespace QuizEngine.Model.View
{
    /// <summary>
    /// Contain the UserName , LevelName, and CategoryName properties, used for displaying relevant info at the UI.
    /// </summary>
    public class QuizView
    {
        #region Properties
        public string UserName { get; set; }
        public Guid QuizUID { get; set; }
        public Guid UserUID { get; set; }
        public decimal? Result { get; set;}
        public string CategoryName { get; set; }
        public string LevelName { get; set; }
        public DateTime QuizDate { get; set; }
        public bool OnlineOrDownloanded { get; set; }
        public bool? IsTaken { get; set; }
        public int LevelDifficulty { get; set; }
        public TimeSpan? Time { get; set; }
        #endregion
    }
}
