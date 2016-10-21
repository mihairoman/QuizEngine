using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuizEngine.ClassicWebForms.Pages
{
    public partial class WelcomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie myCookie = Request.Cookies["currentUser"];
            myCookie.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Add(myCookie);

            if (HttpContext.Current.Request.Cookies["currentUser"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
        }
    }
}