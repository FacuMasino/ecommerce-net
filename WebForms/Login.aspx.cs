using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms
{
    public partial class Login : System.Web.UI.Page
    {
        private User _user;
        private Person _person;
        private UsersManager _userManager = new UsersManager();

        protected void Page_Load(object sender, EventArgs e) { }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                _user = new User();
                _user.Email = UsrEmail.Text;
                _user.Password = UsrPass.Text;

                if (_userManager.Login(_user))
                {
                    Session.Add("user", _user);
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    Session.Add("error", "Mail o Pass incorrectos");
                    Response.Redirect("ErrorLogin.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("ErrorLogin.aspx");
            }
        }
    }
}
