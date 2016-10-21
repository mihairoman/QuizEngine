using QuizEngine.ClassicWebForms.WebServices;
using QuizEngine.Model.Data;
using QuizEngine.Model.View;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace QuizEngine.ClassicWebForms.Webservices
{
    /// <summary>
    /// Summary description for CategoriesWebservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CategoryService : BaseWebservice
    {
        #region Members
        private JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();
        #endregion Members

        #region Methods
		[WebMethod]
		public string ReadAll()
		{
			List<CategoryView> categoryList = _context.Objects.Category.ReadAllView();
			return _javaScriptSerializer.Serialize(categoryList);
		}

        [WebMethod]
        public string InsertCategory(string category)
        {
            Category c = _javaScriptSerializer.Deserialize<Category>(category);
            _context.Objects.Category.Insert(c);
            return null;
        }

        [WebMethod]
        public string UpdateCategory(string category)
        {
            Category c = _javaScriptSerializer.Deserialize<Category>(category);
            _context.Objects.Category.Update(c);
            return null;
        }

		[WebMethod]
		public string DeleteCategory(string category)
		{
			Category c = _javaScriptSerializer.Deserialize<Category>(category);
            try
            {
                _context.Objects.Category.Delete(c);
            }
            catch (Exception e)
            {
                return "error";
            }
			return null;
		}
        #endregion Methods
    }
}
