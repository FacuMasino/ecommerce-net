using System;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms
{
    public partial class Login : System.Web.UI.Page
    {
        private User _user;
        private Person _person;
        private UsersManager _usersManager = new UsersManager();
        private bool errorlog = false;
        private string _redirectTo = "Home.aspx";

        private void CheckRequest()
        {
            foreach (string key in Request.QueryString.AllKeys)
            {
                switch (key)
                {
                    case "redirect":
                        if (Request.QueryString[key] == "admin")
                        {
                            _redirectTo = "/admin";
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckRequest();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                _user = new User();
                _user.Email = UsrEmail.Text;
                _user.Password = UsrPass.Text;

                if (_usersManager.Login(_user))
                {
                    Session.Add("user", _user);
                    Response.Redirect(_redirectTo, false);
                }
                else
                {
                    errorlog = true;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("ErrorLogin.aspx");
            }
        }

        public bool IncorrectData()
        {
            ///// SI EL MAIL NO EXISTE EN LA BASE
            if (errorlog == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
