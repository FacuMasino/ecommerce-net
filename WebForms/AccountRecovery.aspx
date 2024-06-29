<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AccountRecovery.aspx.cs" Inherits="WebForms.AccountRecovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

    <div class="container">
        <div class="row justify-content-center my-md-5 my-3">
            <div class="col-md-4 col-12 p-md-0 px-4">
                <div class="mb-4 text-center">
                    <h2 class="fs-3">Recupera tu cuenta</h2>
                </div>
                <div class="mb-3">
                    <label for="UsrTaxCode" class="form-label">Dni / Cuit / Cuil </label>
                    <input type="number" class="form-control" id="UsrTaxCode" placeholder="Ingresa los datos con los que te registraste">
                </div>
                <button type="button" class="btn btn-dark w-100">
                    Recuperar información de inicio de sesión
                </button>
            </div>

        </div>
    </div>
</asp:Content>
