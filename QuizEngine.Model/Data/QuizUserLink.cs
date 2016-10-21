
using System;
namespace QuizEngine.Model.Data
{
    public class QuizUserLink
    {
        #region Properties
        public Guid QuizUID { get; set; }
        public Guid UserUID { get; set; }
        public decimal? Result { get; set; }
        public DateTime QuizDate { get; set; }
        public bool OnlineOrDownloaded { get; set;}
        public bool? IsTaken { get; set; }
        #endregion
    }
}
