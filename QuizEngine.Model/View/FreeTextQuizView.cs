using System;

namespace QuizEngine.Model.View
{
    /// <summary>
    /// Model used for display free text Quizes on Admin page 
    /// </summary>
    public class FreeTextQuizView
    {   
        public Guid UserUID { get; set; }
        public Guid QuizUID { get; set; }
        public string UserName { get; set; }
        public DateTime QuizDate { get; set; }
        public string CategoryName { get; set; }
        public string LevelName { get; set; }
        public string SortExpresion { get;set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }
    }
}
