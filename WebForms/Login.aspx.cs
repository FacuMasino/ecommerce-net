﻿using System;
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
        private bool errorlog = false;

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
                    Response.Redirect("Home.aspx", false);
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
