using QuizEngine.Business.Core;
using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using QuizEngine.Model.View;
using QuizEngine.Repository;
using System;
using System.Collections.Generic;

namespace QuizEngine.Business
{
    /// <summary>
    /// Class Business
    /// </summary>
    public class CategoryBusiness : BusinessObject
    {
        #region Constructors
        public CategoryBusiness(BusinessContext context) : base(context) { }
        #endregion

        #region Methods

        public List<Category> ReadAll()
        {
            return _context.Repository.Objects.Category.ReadAll();
        }

		public List<CategoryView> ReadAllView()
		{
			return _context.Repository.Objects.CategoryView.ReadAll();
		}

        public Category ReadByID(Guid category)
        {
            return _context.Repository.Objects.Category.ReadByID(category); ;
        }

        public void Update(Category category)
        {
            _context.Repository.Objects.Category.Update(category);
        }

        public void Insert(Category category)
        {
            _context.Repository.Objects.Category.Insert(category);
        }

        public void Delete(Category category)
        {
            _context.Repository.Objects.Category.Delete(category);
        }
        #endregion Methods
    }
}
