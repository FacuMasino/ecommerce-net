<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="WebForms.User" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="row justify-content-center my-md-5 my-3">
        <div class="col-md-4 col-12 p-md-0 px-4">
            <div class="mb-4 text-center">
               <h2 class="fs-3">Crear una cuenta</h2>
            </div>
            <div class="mb-3">
                <label for="UsrEmail" class="form-label">Correo Electrónico</label>
                <input type="email" class="form-control" id="UsrEmail" placeholder="tucorreo@correo.com">
            </div>
            <div class="mb-3">
                <label for="UsrPass" class="form-label">Contraseña</label>
                <input type="password" class="form-control" id="UsrPass" placeholder="Contraseña">
            </div>
            <div class="mb-3">
                <label for="UsrPassCheck" class="form-label">Confirmar Contraseña</label>
                <input type="password" class="form-control" id="UsrPassCheck" placeholder="Repite tu contarseña">
                <div class="d-flex flex-column px-2">
                    <span id="passwordHelp" class="form-text fw-bold">Requisitos
                    </span>
                    <ul class="form-text">
                        <li>Debe tener entre 8-20 caracteres.
                        </li>
                        <li>Debe contar con al menos 1 mayúscula y 1 minúscula.
                        </li>
                        <li>Debe tener al menos 1 número
                        </li>
                    </ul>
                </div>
            </div>
            <button type="button" class="btn btn-dark w-100">
                Crear cuenta
            </button>
        </div>
    </div>
</asp:Content>
