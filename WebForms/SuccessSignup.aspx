<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SuccessSignup.aspx.cs" Inherits="WebForms.SuccessSignup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="row flex-column align-items-center py-2">
            <div class="col-6 text-center mb-4 d-flex flex-column gap-2">
                <h5 class="text-align-center my-3">Tu cuenta fue creada con éxito!</h5>
                <img src="Content/img/success-signup.svg" class="img-fluid object-fit-cover mb-4 half-vh" />
                <div>
                    <p class="mb-1">Ya puedes iniciar sesión.</p>
                    <p>Te enviamos un email de bienvenida, si no lo ves verifica tu carpeta de Spam.
                    </p>
                </div>
                <a href="Login.aspx" class="btn btn-primary text-center" type="button">Iniciar sesión</a>
            </div>
        </div>
    </div>
</asp:Content>
