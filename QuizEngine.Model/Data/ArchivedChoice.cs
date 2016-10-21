using System;

namespace QuizEngine.Model.Data
{
    /// <summary>
    /// The ArchivedChoice model 
    /// <see cref="QuizEngine.Models.Choice"/>
    /// </summary>
    /// 
    //[Serializable]
    public class ArchivedChoice
    {
        #region Properties
        public Guid ChoiceID { get; set; }
        public Guid QuestionID { get; set; }
        public string AnswerText { get; set; }
        public decimal? Value { get; set; }
        public bool? IsCorrect { get; set;}
        public int? ChoicePosition { get; set; }
        #endregion      
    }
}
