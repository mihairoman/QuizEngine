using QuizEngine.Webservice;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace QuizEngine.Webservice
{
	/// <summary>
	/// Summary description for LevelsWebservice
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[System.Web.Script.Services.ScriptService]
	public class LevelService : BaseWebservice
	{
		#region Members
		private JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();
		#endregion Members

		#region Methods
		[WebMethod]
		public string ReadAll()
		{
			List<Level> levelList = _context.Objects.Level.ReadAll();
			return _javaScriptSerializer.Serialize(levelList);
		}

		[WebMethod]
		public string InsertLevel(string level)
		{
			Level l = _javaScriptSerializer.Deserialize<Level>(level);
			_context.Objects.Level.Insert(l);
			return null;
		}

		[WebMethod]
		public string UpdateLevel(string level)
		{
			Level l = _javaScriptSerializer.Deserialize<Level>(level);
			_context.Objects.Level.Update(l);
			return null;
		}

		[WebMethod]
		public string DeleteLevel(string level)
		{
            Level l = _javaScriptSerializer.Deserialize<Level>(level); 
            try
            {
                _context.Objects.Level.Delete(l);
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
