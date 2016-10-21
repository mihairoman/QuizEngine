using QuizEngine.Model.Data;
using QuizEngine.Model.Data.View;
using QuizEngine.Model.View;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;

namespace QuizEngine.UI
{
    /// <summary>
    /// Class Business
    /// </summary>
    public class CategoryUI : UIObject
    {
        #region Constructors
        public CategoryUI(UIContext context) : base(context) { }
        #endregion

        #region Methods

        public List<Category> ReadAll()
        {
            return _context.Business.Objects.Category.ReadAll();
        }

		public List<CategoryView> ReadAllView()
		{
			return _context.Business.Objects.Category.ReadAllView();
		}

        public Category ReadByID(Guid category)
        {
            return _context.Business.Objects.Category.ReadByID(category); ;
        }

        public void Update(Category category)
        {
            _context.Business.Objects.Category.Update(category);
        }

        public void Insert(Category category)
        {
            if (category.CategoryUID.CompareTo(Guid.Empty) == 0)
                category.CategoryUID = Guid.NewGuid();
            _context.Business.Objects.Category.Insert(category);
        }

        public void Delete(Category category)
        {
            _context.Business.Objects.Category.Delete(category);
        }
        #endregion Methods
    }
}
