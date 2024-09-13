using System;
using System.Collections.Generic;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace WebForms
{
    public partial class Signup : BasePage
    {
        // ATTRIBUTES

        private User _user;
        private UsersManager _usersManager;
        private RolesManager _rolesManager;
        private EmailManager _emailManager;
        private List<InputWrapper> _inputValidations;
        private bool _passwordsMatch;

        // CONSTRUCT

        public Signup()
        {
            _user = new User();
            _usersManager = new UsersManager();
            _rolesManager = new RolesManager();
            _emailManager = new EmailManager();
            _inputValidations = new List<InputWrapper>();
            _passwordsMatch = true;
        }

        // PROPERTIES

        public bool PasswordsMatch
        {
            get { return _passwordsMatch; }
        }

        // METHODS

        private void AddValidations()
        {
            _inputValidations.Add(new InputWrapper(UsrFirstNameTxt, typeof(string), 2, 30));
            _inputValidations.Add(new InputWrapper(UsrLastnameTxt, typeof(string), 2, 30));
            _inputValidations.Add(new InputWrapper(UsrPassTxt, typeof(string), 8, 20, false, false, true));
        }

        private void SendWelcomeEmail()
        {
            EmailMessage<WelcomeEmail> welcomeEmail = Helper.ComposeWelcomeEmail(
                _user,
                Helper.EcommerceName,
                Helper.EcommerceUrl
            );

            _emailManager.SendEmail(welcomeEmail);
        }

        private bool ValidateSignup()
        {
            if (UsrPassTxt.Text != UsrPassCheckTxt.Text)
            {
                _passwordsMatch = false;
            }

            return Validator.RunValidations(_inputValidations) && _passwordsMatch;
        }

        public bool IsValidInput(string controlId)
        {
            InputWrapper auxIW = _inputValidations.Find(ctl => ctl.Control.ID == controlId);
            
            if (auxIW != null && auxIW.IsValid)
            {
                return true;
            }
            
            return false;
        }

        private void MapControlsToObj()
        {
            _user.Email = UsrEmailTxt.Text;
            _user.FirstName = UsrFirstNameTxt.Text;
            _user.LastName = UsrLastnameTxt.Text;
            _user.Password = UsrPassTxt.Text;
            _user.Phone = PhoneTxt.Text;
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            AddValidations();
        }

        protected void BtnSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                MapControlsToObj();

                if (!ValidateSignup())
                {
                    return;
                }

                _user.PersonId = _usersManager.GetPersonId(_user.Email);
                _user.UserId = _usersManager.GetUserId(_user.PersonId);

                if (0 < _user.UserId)
                {
                    throw new Exception("Ya existe un usuario registrado con el email ingresado.");
                }
                else
                {
                    _user.UserId = _usersManager.Add(_user);

                    if (0 < _user.UserId)
                    {
                        _rolesManager.UpdateRelations(_user);
                        Session["SuccessSignup"] = true;
                        SendWelcomeEmail();
                        Response.Redirect("SuccessSignup.aspx", false);
                    }
                    else
                    {
                        throw new Exception("No se pudo realizar el registro");
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("SignUpError.aspx");
            }
        }
    }
}
