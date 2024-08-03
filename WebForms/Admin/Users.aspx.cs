using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        // ATTRIBUTES

        private User _user;
        private List<User> _users;
        private UsersManager _usersManager;
        private RolesManager _rolesManager;

        // CONSTRUCT

        public Users()
        {
            _usersManager = new UsersManager();
            _rolesManager = new RolesManager();
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

        protected void UsersRpt_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _user = (User)e.Item.DataItem;
                
                Repeater rpt = (Repeater)e.Item.FindControl("RolesRpt");
                rpt.DataSource = _rolesManager.List();
                rpt.DataBind();
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {

        }
    }
}