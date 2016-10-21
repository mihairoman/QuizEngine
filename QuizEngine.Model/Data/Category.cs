using System;

namespace QuizEngine.Model.Data
{
    /// <summary>
    /// Class Categories 
    /// </summary>
    [Serializable]
    public class Category
    {

        #region Members
        private Guid _categoryUID;
        private string _categoryName;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Instantiates a <see cref="QuizEngine.Model.Data.Category"/>.
        /// </summary>
        public Category()
        {

        }
        /// <summary>
        /// Instantiates a <see cref="QuizEngine.Model.Data.Category"/>.
        /// </summary>
        /// <param name="categoryUID"></param>
        /// <param name="categoryName"></param>
        public Category(Guid categoryUID, string categoryName)
        {
            _categoryUID = categoryUID;
            _categoryName = categoryName;
        }
        #endregion Constructors

        #region Properties
        public Guid CategoryUID
        {
            get { return _categoryUID; }
            set { _categoryUID = value; }
        }

        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }
        #endregion Properties
    }
}
