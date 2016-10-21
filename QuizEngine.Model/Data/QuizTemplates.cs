
using System;
namespace QuizEngine.Model.Data
{
    public class QuizTemplates
    {
        #region Properties
        public Guid QuizTemplateUID { get; set; }
        public string TypeName { get; set; }
        public  TimeSpan? Time {get; set;}
        #endregion
    }
}
