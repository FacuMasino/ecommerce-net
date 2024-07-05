<%@ Page Title="Mis Datos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Account.aspx.cs" Inherits="WebForms.Account" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="d-flex flex-column">
            <div class="mt-4">

                <h2 class="fs-4 fw-bold">Hola Nombre!</h2>
                <p class="fs-5">Bienvenido a tu cuenta.</p>
            </div>
            <ul class="nav align-self-md-end align-self-start">
                <li class="nav-item">
                    <a class="nav-link fs-5 fw-bold ps-0 text-black active" aria-current="page" href="#">Mis Datos</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link fs-5 ps-0 text-black" href="Orders.aspx">Mis Pedidos</a>
                </li>
            </ul>
            <div class="row g-0 border-top">
                <div class="col-md col-12 p-4">
                    <h2 class="fs-5 mb-3">Información Personal</h2>
                    <div class="mb-3 ">
                        <asp:TextBox runat="server" TextBoxMode="email" class="form-control" ID="UsrEmailTxt" placeholder="tucorreo@correo.com" />
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:TextBox runat="server" TextBoxMode="text" class="form-control" ID="UsrNameTxt" placeholder="Nombres" />
                        </div>
                        <div class="col">
                            <asp:TextBox runat="server" TextBoxMode="text" class="form-control" ID="UsrSurnameTxt" placeholder="Apellidos" />
                        </div>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox runat="server" TextBoxMode="number" class="form-control" ID="UsrDocumentTxt" placeholder="DNI" />
                    </div>
                    <div class="mb-3">
                        <asp:TextBox runat="server" TextBoxMode="date" class="form-control" ID="BirthDateTxt" placeholder="Fecha de Nacimiento"/>
                    </div>
                    <h2 class="fs-5 mb-3">Seguridad</h2>
                    <button type="button" class="btn btn-dark">Cambiar Contraseña</button>
                </div>
                <div class="col-md col-12 p-4 border-md-start">
                    <h2 class="fs-5 mb-3">Datos para tus envíos</h2>
                    <div class="mb-3">
                        <asp:TextBox runat="server" TextBoxMode="text" class="form-control" ID="UsrAdreTxt" placeholder="Domicilio" />
                    </div>

                    <div class="row mb-3">
                        <div class="col">
                            <asp:TextBox runat="server" TextBoxMode="number" class="form-control" ID="UsrNmberTxt" placeholder="Numero" />
                        </div>
                        <div class="col">
                            <asp:TextBox runat="server" class="form-control" ID="UsrDptTxt" placeholder="Piso/Depto/Oficina" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:TextBox runat="server" TextBoxMode="text" class="form-control" ID="UsrPCTxt" placeholder="Código Postal" />
                        </div>
                        <div class="col">
                            <asp:TextBox runat="server" TextBoxMode="text" class="form-control" ID="UsrCityTxt" placeholder="Ciudad" />
                        </div>
                    </div>
                    <div class="mb-3">
                        <select class="form-select" aria-label="Selección por defecto">
                            <option selected>Provincia</option>
                            <option value="1">One</option>
                            <option value="2">Two</option>
                            <option value="3">Three</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox runat="server" TextBoxMode="number" class="form-control" ID="UsrPhoneTxt" placeholder="Teléfono" />
                    </div>
                </div>
            </div>
            <div class="d-flex justify-content-end pe-4 mb-3">
                <button type="button" class="btn px-4 btn-dark">Guardar</button>
            </div>
        </div>
    </div>
</asp:Content>
