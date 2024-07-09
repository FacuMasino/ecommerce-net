using System;
using System.Collections.Generic;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace WebForms
{
    public partial class Signup : BasePage
    {
        User _user = new User();
        UsersManager _usersManager = new UsersManager();
        EmailManager _emailManager = new EmailManager();
        List<InputWrapper> _inputValidations = new List<InputWrapper>();
        private bool _passwordsMatch = true;

        // Verificar que las contraseñas coincidan
        public bool PasswordsMatch
        {
            get { return _passwordsMatch; }
        }

        private void AddValidations()
        {
            _inputValidations.Add(new InputWrapper(UsrFirstNameTxt, typeof(string), 2, 30));
            _inputValidations.Add(new InputWrapper(UsrLastnameTxt, typeof(string), 2, 30));
            _inputValidations.Add(
                new InputWrapper(UsrPassTxt, typeof(string), 8, 20, false, false, true)
            );
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

        private bool Validate()
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
                return true;
            return false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AddValidations();
        }

        protected void BtnSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                _user.Email = UsrEmailTxt.Text;
                _user.FirstName = UsrFirstNameTxt.Text;
                _user.LastName = UsrLastnameTxt.Text;
                _user.Password = UsrPassTxt.Text;

                if (!Validate())
                    return;

                ////SI EL MAIL YA ESTÁ REGISTRADO - desarrollar funcion de busqueda en la base

                /*if ()
                {
                    Session.Add("error", "Mail no disponible para registrarse");

                }
          
               */

                if (_usersManager.Add(_user) != 0)
                {
                    Session["SuccessSignup"] = true;
                    SendWelcomeEmail();
                    Response.Redirect("SuccessSignup.aspx", false);
                }
                else
                {
                    throw new Exception("No se pudo realizar el registro");
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
