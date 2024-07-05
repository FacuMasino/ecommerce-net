<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AccessDenied.aspx.cs" Inherits="WebForms.AccessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="d-flex flex-column align-items-center py-4 my-4">
            <div class="col-6 text-center">
                <h5 class="text-align-center">Ups! No tenés permisos para ingresar a esta sección.
                </h5>
                <p>Por favor inicia sesión como administrador</p>
                <a href="Login.aspx" class="btn btn-primary text-center" type="button">Iniciar sesión</a>
                <img src="Content/img/policy-rafiki.svg" class="img-fluid object-fit-cover h-75" />
            </div>
            <div class="col-6 text-center">
            </div>
        </div>
    </div>
</asp:Content>
