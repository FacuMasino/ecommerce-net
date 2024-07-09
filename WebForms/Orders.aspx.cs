using System;
using System.EnterpriseServices;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms
{
    public partial class Orders : System.Web.UI.Page
    {
        // ATTRIBUTES

        private User _user;

        // CONSTRUCT

        public Orders()
        {
            _user = new User();
        }

        private void MapControls()
        {
            WelcomeTxt.Text = "Hola " + _user.FirstName + "!";
        }

        // METHODS

        private void FetchUser()
        {
            _user = (User)Session["user"];

            if (_user == null)
            {
                _user = new User();
            }
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchUser();
                MapControls();
            }
        }
    }
}