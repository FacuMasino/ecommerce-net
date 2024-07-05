using System;
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

        protected void Page_Load(object sender, EventArgs e) { }

        protected void BtnSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                _user.Email = UsrEmailTxt.Text;
                _user.FirstName = UsrNameTxt.Text;
                _user.LastName = UsrSurNmTxt.Text;

                ///SI LAS PASS NO SON IGUALES EN LOS DOS CAMPOS
                if (UsrPassTxt.Text == UsrPassCheckTxt.Text)
                {
                    _user.Password = UsrPassTxt.Text;
                }
                else
                {
                    Session.Add("error", "Las contraseñas no coinciden, vuelva a cargar la info");
                }

                ////SI EL MAIL YA ESTÁ REGISTRADO - desarrollar funcion de busqueda en la base

                /*if ()
                {
                    Session.Add("error", "Mail no disponible para registrarse");

                }
          
               */


                //// SI EL REGISTRO FUE EXITOSO


                if (_usersManager.Add(_user) != 0)
                {
                    /// FALTA GENERAR NOTIFICACION EXITOSA
                    Helper.ComposeWelcomeEmail(_user, Helper.EcommerceName, Helper.EcommerceUrl);
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    Session.Add("error", "No se pudo realizar el registro");
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
