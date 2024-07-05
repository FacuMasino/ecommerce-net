<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="WebForms.Login" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="row justify-content-center my-md-5 my-3">
            <div class="col-md-4 col-12 p-md-0 px-4">
                <div class="mb-4 text-center">
                    <h2 class="fs-3">Inicio de Sesión</h2>
                </div>
                <div class="mb-3">
                    <label for="UsrEmail" class="form-label">Correo Electrónico</label>
                    <asp:TextBox runat="server" TextMode="email" class="form-control" ID="UsrEmail" placeholder="Ingresa el mail con el que te registraste" />
                </div>
                <div class="mb-3">
                    <label for="UsrPass" class="form-label">Contraseña</label>
                    <asp:TextBox runat="server" TextMode="Password" class="form-control" ID="UsrPass" placeholder="Contraseña" />
                </div>
                <div class="mb-3">
                    <asp:Button Text="Ingresar" class="btn btn-primary w-100" ID="LoginButton" OnClick="LoginButton_Click" runat="server" />
                    <a href="AccountRecovery.aspx" class="nav-link px-2 text-body-secondary">¿Olvidaste
                        tus datos de inicio?</a>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
