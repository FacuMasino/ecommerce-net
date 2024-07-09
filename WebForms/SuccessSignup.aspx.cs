using System;

namespace WebForms
{
    public partial class SuccessSignup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuccessSignup"] == null)
            {
                Response.Redirect("/");
            }

            Session.Remove("SuccessSignup");
        }
    }
}
