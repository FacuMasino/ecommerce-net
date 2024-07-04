<%@ Page Title="Página de error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorLogin.aspx.cs" Inherits="WebForms.ErrorLogin" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

    <div class="container">
        <div class="d-flex flex-column align-items-center py-4 my-4">

            <div class="col-6">
                <h5 class="text-align-center">¡Ingreso incorrecto, vuelve a intentar!...   </h5>
                <asp:Label Text="text" ID="lblMsjeError" runat="server" />

                <img src="Content/img/Thinking face-rafiki.svg" class="img-fluid object-fit-cover h-75" />
            </div>
        </div>
    </div>



    <h1></h1>

</asp:Content>
