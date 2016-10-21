using System;

namespace QuizEngine.Model.Data
{
    /// <summary>
    /// Class Tags
    /// </summary>
    public class Tag
    {
        #region Constructors
        public Tag()
        {

        }
        /// <summary>
        /// Instantiates a <see cref="QuizEngine.Model.Data.Tag"/>.
        /// </summary>
        /// <param name="tagUID"></param>
        /// <param name="tagName"></param>
        public Tag(Guid tagUID, string tagName)
        {
            TagUID = tagUID;
            TagName = tagName;
        }
        #endregion Constructors

        #region Properties

        public Guid TagUID { get; set; }

        public string TagName { get; set; }

        #endregion Properties
    }
}
