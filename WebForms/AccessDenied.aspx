<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AccessDenied.aspx.cs" Inherits="WebForms.AccessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="row flex-column align-items-center py-2">
            <div class="col-6 text-center mb-4">
                <img src="Content/img/policy-rafiki.svg" class="img-fluid object-fit-cover h-75" />
                <h5 class="text-align-center">Ups! No tenés permisos para ingresar a esta sección.
                </h5>
                <p>Por favor inicia sesión como administrador</p>
                <a href="Login.aspx?redirect=admin" class="btn btn-primary text-center" type="button">Iniciar sesión</a>
            </div>
        </div>
    </div>
</asp:Content>
