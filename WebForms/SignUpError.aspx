<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignUpError.aspx.cs" Inherits="WebForms.SignUpError" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

     <div class="container">
     <div class="d-flex flex-column align-items-center py-4 my-4">

         <div class="col-6">
             <h5 class="text-align-center">¡Ups! No pudimos crear la cuenta debido a un error, intenta más tarde...   </h5>
             <asp:Label Text="text" ID="lblMsjeError" runat="server" />

             <img src="Content/img/Thinking face-rafiki.svg" class="img-fluid object-fit-cover h-75" />
         </div>
     </div>
 </div>










</asp:Content>
