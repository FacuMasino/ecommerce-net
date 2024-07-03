<%@ Page Title="Página de error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorLogin.aspx.cs" Inherits="WebForms.ErrorLogin" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <h1>Ingreso incorrecto, vuelve a intentar</h1>
    <asp:Label Text="text"  ID="lblMsjeError" runat="server" />
</asp:Content>
