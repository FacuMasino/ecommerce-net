using System;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms
{
    public partial class Account : System.Web.UI.Page
    {
        // ATTRIBUTES

        User _user;
        private UsersManager _usersManager;
        private ProvincesManager _provincesManager;

        // CONSTRUCT

        public Account()
        {
            _usersManager = new UsersManager();
            _provincesManager = new ProvincesManager();
        }

        // METHODS

        private void FetchUser()
        {
            _user = (User)Session["user"];
        }

        private void MapObjToControls()
        {
            if (_user != null)
            {
                GreetingLbl.Text = "¡Hola " + _user.FirstName + "!";
                LastNameTxt.Text = _user.LastName;
                FirstNameTxt.Text = _user.FirstName;
                TaxCodeTxt.Text = _user.TaxCode;
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

        private void BindProvincesDDL()
        {
            ProvincesDDL.DataSource = _provincesManager.List(1);
            //ProvincesDDL.DataTextField = "Name";
            //ProvincesDDL.DataValueField = "Id";
            ProvincesDDL.DataBind();
            ProvincesDDL.SelectedIndex = 0;
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchUser();
                BindProvincesDDL();
                MapObjToControls();
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
