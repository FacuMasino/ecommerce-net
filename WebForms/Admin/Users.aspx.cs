using System;
using System.Collections.Generic;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        // ATTRIBUTES

        private List<User> _users;
        private UsersManager _usersManager;

        // CONSTRUCT

        public Users()
        {
            _usersManager = new UsersManager();
            FetchUsers();
        }

        // METHODS

        private void FetchUsers()
        {
            _users = _usersManager.List();
        }

        private void BindUsersRpt()
        {
            UsersRpt.DataSource = _users;
            UsersRpt.DataBind();
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchUsers();
                BindUsersRpt();
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {

        }
    }
}