using System;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms
{
    public partial class Account : System.Web.UI.Page
    {
        // ATTRIBUTES

        User _user;
        private UsersManager _usersManager = new UsersManager();

        // METHODS

        private void FetchUser()
        {
            _user = (User)Session["user"];
        }

        private void MapControls()
        {
            if (_user != null)
            {
                GreetingLbl.Text = "¡Hola " + _user.FirstName + "!";
                LastNameTxt.Text = _user.LastName;
                FirstNameTxt.Text = _user.FirstName;
                TaxCodeTxt.Text = _user.TaxCode;
                EmailTxt.Enabled = false;
                EmailTxt.Text = _user.Email;
                StreetNameTxt.Text = _user.Address.StreetName;
                StreetNumberTxt.Text = _user.Address.StreetNumber;
                FlatTxt.Text = _user.Address.Flat;
                CityTxt.Text = _user.Address.City.Name;
                ZipCodeTxt.Text = _user.Address.City.ZipCode;

                PhoneTxt.Text = _user.Phone;
                BirthTxt.Text = _user.Birth.ToString("yyyy-MM-dd");
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
