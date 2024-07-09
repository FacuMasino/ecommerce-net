<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Signup.aspx.cs" Inherits="WebForms.Signup" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="row justify-content-center my-md-5 my-3">
        <div class="col-md-4 col-12 p-md-0 px-4">
            <div class="mb-4 text-center">
                <h2 class="fs-3">Crear una cuenta</h2>
            </div>
            <div class="mb-3">
                <label for="UsrEmailTxt" class="form-label">Correo Electrónico</label>
                <asp:TextBox runat="server" TextMode="email" class="form-control" ID="UsrEmailTxt"
                    placeholder="tucorreo@correo.com" required/>
            </div>

            <div class="mb-3 <%:IsValidInput("UsrFirstNameTxt") ? "":"invalid"%>">
                <label for="UsrFirstNameTxt" class="form-label">Nombre</label>
                <asp:TextBox runat="server" class="form-control" ID="UsrFirstNameTxt" placeholder="Nombre" />
                <div class="invalid-feedback">
                    El nombre debe tener entre 2 y 30 caracteres
                </div>
            </div>

            <div class="mb-3 <%:IsValidInput("UsrLastnameTxt") ? "":"invalid"%>">
                <label for="UsrLastnameTxt" class="form-label">Apellido</label>
                <asp:TextBox runat="server" class="form-control" ID="UsrLastnameTxt" placeholder="Apellido" />
                <div class="invalid-feedback">
                    El apellido debe tener entre 2 y 30 caracteres
                </div>
            </div>

            <div class="mb-3 <%:!PasswordsMatch ? "invalid":""%>">
                <label for="UsrPass" class="form-label">Contraseña</label>
                <asp:TextBox runat="server" TextMode="Password" class="form-control" ID="UsrPassTxt"
                    placeholder="Contraseña" />
                <div class="invalid-feedback">
                    Las contraseñas no coinciden.
                </div>
            </div>

            <div class="mb-3 <%:IsValidInput("UsrPassTxt") ? "":"invalid"%>">
                <label for="UsrPassCheck" class="form-label">Confirmar Contraseña</label>
                <asp:TextBox runat="server" TextMode="Password" class="form-control" ID="UsrPassCheckTxt"
                    placeholder="Repite tu contarseña" />
                <div class="invalid-feedback">
                    La contraseña no cumple con los requisitos mínimos.    
                </div>
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
            <asp:Button runat="server" class="btn btn-dark w-100" Text="Crear Cuenta" ID="BtnSignUp"
                OnClick="BtnSignUp_Click" />
        </div>
    </div>
</asp:Content>

