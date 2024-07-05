﻿using System;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms
{
    public partial class Account : System.Web.UI.Page
    {
        private UsersManager _usersManager = new UsersManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                User _user = (User)Session["user"];

                if (_user != null)
                {
                    /* if (UsrNameTitleTxt.Text != "")
                     {
                         UsrNameTitleTxt.Text = _user.Username;
                     }*/
                    UsrGreetingLbl.Text = _user.FirstName;
                    UsrSurnameTxt.Text = _user.LastName;
                    UsrNameTxt.Text = _user.FirstName;
                    UsrDocumentTxt.Text = _user.TaxCode;
                    UsrEmailTxt.Enabled = false;
                    UsrEmailTxt.Text = _user.Email;
                    UsrAdreTxt.Text = _user.Address.StreetName;
                    UsrNmberTxt.Text = _user.Address.StreetNumber;
                    UsrDptTxt.Text = _user.Address.Flat;
                    UsrCityTxt.Text = _user.Address.City.Name;
                    UsrPCTxt.Text = _user.Address.City.ZipCode;

                    UsrPhoneTxt.Text = _user.Phone;
                    BirthDateTxt.Text = _user.Birth.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
