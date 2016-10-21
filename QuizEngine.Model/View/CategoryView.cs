using System;

namespace QuizEngine.Model.View
{
    public class CategoryView
    {
        #region Members
        private Guid _categoryUID;
        private string _categoryName;
        private int _numberOfUsingQuestions;
        #endregion Members

        #region Constructors
        public CategoryView()
        {

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

        public int NumberOfUsingQuestions
        {
            get { return _numberOfUsingQuestions; }
            set { _numberOfUsingQuestions = value; }
        }
        #endregion Properties
    }
}
