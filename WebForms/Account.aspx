<%@ Page Title="Mis Datos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Account.aspx.cs" Inherits="WebForms.Account" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="d-flex flex-column">
            <div class="mt-4">
                <asp:Label
                    ID="GreetingLbl"
                    runat="server" />
            </div>
            <ul class="nav align-self-md-end align-self-start">
                <li class="nav-item">
                    <a
                        class="nav-link fs-5 fw-bold ps-0 text-black active"
                        aria-current="page"
                        href="#">
                        Mis Datos
                    </a>
                </li>
                <li class="nav-item">
                    <a
                        class="nav-link fs-5 ps-0 text-black"
                        href="Orders.aspx">
                        Mis Pedidos
                    </a>
                </li>
            </ul>
            <div class="row g-0 border-top">
                <div class="col-md col-12 p-4">
                    <h2 class="fs-5 mb-3">Información personal</h2>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="FirstNameTxt"
                                CssClass="form-control"
                                placeholder="Nombres" />
                        </div>
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="LastNameTxt"
                                CssClass="form-control"
                                placeholder="Apellidos" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="TaxCodeTxt"
                                CssClass="form-control"
                                TextMode="number"
                                placeholder="DNI" />
                        </div>
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="BirthTxt"
                                CssClass="form-control"
                                TextMode="date"
                                placeholder="Fecha de Nacimiento" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="EmailTxt"
                                CssClass="form-control"
                                TextBox="email"
                                placeholder="tucorreo@correo.com" />
                        </div>
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="PhoneTxt"
                                CssClass="form-control"
                                TextMode="number"
                                placeholder="Teléfono" />
                        </div>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox
                            runat="server"
                            ID="UsernameTxt"
                            CssClass="form-control"
                            placeholder="Alias" />
                    </div>
                    <h2 class="fs-5 mb-3">Seguridad</h2>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="CurrentPasswordTxt"
                                CssClass="form-control"
                                TextMode="Password"
                                placeholder="Contraseña actual" />
                        </div>
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="NewPasswordTxt"
                                CssClass="form-control"
                                TextMode="Password"
                                placeholder="Nueva contraseña" />
                        </div>
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="ConfirmPasswordTxt"
                                CssClass="form-control"
                                TextMode="Password"
                                placeholder="Confirmar contraseña" />
                        </div>
                    </div>
                </div>
                <div class="col-md col-12 p-4 border-md-start">
                    <h2 class="fs-5 mb-3">Dirección para tus envíos</h2>
                    <div class="mb-3">
                        <asp:DropDownList
                            ID="ProvincesDDL"
                            runat="server"
                            CssClass="form-select"
                            required>
                        </asp:DropDownList>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="CityTxt"
                                CssClass="form-control"
                                placeholder="Localidad o ciudad" />
                        </div>
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="ZipCodeTxt"
                                CssClass="form-control"
                                placeholder="Código Postal" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="StreetNameTxt"
                                CssClass="form-control"
                                placeholder="Calle" />
                        </div>
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="StreetNumberTxt"
                                CssClass="form-control"
                                TextMode="number"
                                placeholder="1234" />
                        </div>
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="FlatTxt"
                                CssClass="form-control"
                                placeholder="Depto. (opcional)" />
                        </div>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox
                            runat="server"
                            ID="DetailsTxt"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="5"
                            Columns="40"
                            placeholder="Indicaciones adicionales">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="d-flex justify-content-end pe-4 mb-3">
                <asp:Button
                    runat="server"
                    ID="SaveBtn"
                    CssClass="btn px-4 btn-dark me-2"
                    OnClick="SaveBtn_Click"
                    Text="Guardar" />
            </div>
        </div>
    </div>
</asp:Content>
