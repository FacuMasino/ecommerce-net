<%@ Page Title="Realizar Pedido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="OrderConfirmation.aspx.cs" Inherits="WebForms.OrderConfirmation" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <h2 class="fs-4 mt-4 mb-3 ps-md-4 ps-2">Completá los datos y realizá tu pedido</h2>
        <div class="row g-0 my-5">
            <section class="col-md-7 col-12 px-md-4 px-2">
                <h2 class="fs-5 mb-3">Información de contacto</h2>

                <!-- Nombre y apellido -->

                <div class="row mb-3">
                    <div class="col <%:IsValidInput("FirstNameTxt") ? "":"invalid"%>">
                        <asp:TextBox
                            runat="server"
                            ID="FirstNameTxt"
                            CssClass="form-control"
                            placeholder="Nombre"
                            required>
                        </asp:TextBox>
                        <div class="invalid-feedback">
                            Campo Inválido, ingrese un nombre.
                        </div>
                    </div>
                    <div class="col <%:IsValidInput("LastNameTxt") ? "":"invalid"%>">
                        <asp:TextBox
                            runat="server"
                            ID="LastNameTxt"
                            CssClass="form-control"
                            placeholder="Apellido"
                            required>
                        </asp:TextBox>
                        <div class="invalid-feedback">
                            Campo Inválido, ingrese un apellido.
                        </div>
                    </div>
                </div>

                <!-- Email -->

                <div class="mb-3 <%:IsValidInput("EmailTxt") ? "":"invalid"%>">
                    <asp:TextBox
                        runat="server"
                        ID="EmailTxt"
                        CssClass="form-control"
                        placeholder="tucorreo@electronico.com"
                        TextMode="Email"
                        required>
                    </asp:TextBox>
                    <div class="invalid-feedback">
                        Campo Inválido, ingrese un correo electrónico.
                    </div>
                </div>

                <!-- Método de pago -->

                <div class="mb-3">
                    <h2 class="fs-5 mb-3">Método de pago</h2>
                    <div class="d-flex gap-2">
                        <asp:RadioButton
                            runat="server"
                            ID="CashRB"
                            Checked="true"
                            GroupName="PaymentType"
                            Text="Efectivo" />
                    </div>
                    <div class="d-flex gap-2">
                        <asp:RadioButton
                            runat="server"
                            ID="MercadoPagoRB"
                            Checked="false"
                            GroupName="PaymentType"
                            Text="Mercado Pago" />
                    </div>
                    <div class="d-flex gap-2">
                        <asp:RadioButton
                            runat="server"
                            ID="BankTransferRB"
                            Checked="false"
                            GroupName="PaymentType"
                            Text="Transferencia bancaria" />
                    </div>
                </div>

                <!-- Forma de entrega -->

                <div class="mb-3">
                    <h2 class="fs-5 mb-3">Forma de entrega</h2>
                    <div class="d-flex gap-2">
                        <asp:RadioButton
                            runat="server"
                            ID="PickupRB"
                            Checked="true"
                            GroupName="Delivery"
                            Text="Retiro en sucursal"
                            OnCheckedChanged="PickupRB_CheckedChanged"
                            AutoPostBack="true" />
                    </div>
                    <div class="d-flex gap-2">
                        <asp:RadioButton
                            runat="server"
                            ID="DeliveryRB"
                            Checked="false"
                            GroupName="Delivery"
                            Text="Envío a domicilio"
                            OnCheckedChanged="DeliveryRB_CheckedChanged"
                            AutoPostBack="true" />
                    </div>
                </div>

                <!-- Dirección de entrega -->

                <asp:Panel ID="AddressPnl" runat="server" Visible="false">
                    <div class="mb-3">
                        <h2 class="fs-5 mb-3">Dirección de entrega</h2>
                    </div>
                    <div class="mb-3">
                        <asp:DropDownList
                            runat="server"
                            ID="ProvincesDDL"
                            CssClass="form-select"
                            required>
                        </asp:DropDownList>
                    </div>
                    <div class="row mb-3">
                        <div class="col <%:IsValidInput("CityTxt") ? "":"invalid"%>">
                            <asp:TextBox
                                runat="server"
                                ID="CityTxt"
                                CssClass="form-control"
                                placeholder="Localidad o ciudad"
                                required>
                            </asp:TextBox>
                            <div class="invalid-feedback">
                                Campo Inválido, ingrese la localidad o ciudad.
                            </div>
                        </div>
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="ZipCodeTxt"
                                CssClass="form-control"
                                placeholder="Código postal (opcional)">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col <%:IsValidInput("StreetNameTxt") ? "":"invalid"%>">
                            <asp:TextBox
                                runat="server"
                                ID="StreetNameTxt"
                                CssClass="form-control"
                                placeholder="Calle"
                                required>
                            </asp:TextBox>
                            <div class="invalid-feedback">
                                Campo Inválido, ingrese la calle.
                            </div>
                        </div>
                        <div class="col <%:IsValidInput("StreetNumberTxt") ? "":"invalid"%>">
                            <asp:TextBox
                                runat="server"
                                ID="StreetNumberTxt"
                                CssClass="form-control"
                                placeholder="1234"
                                required>
                            </asp:TextBox>
                            <div class="invalid-feedback">
                                Campo Inválido, ingrese el número.
                            </div>
                        </div>
                        <div class="col">
                            <asp:TextBox
                                runat="server"
                                ID="FlatTxt"
                                CssClass="form-control"
                                placeholder="Depto. (opcional)">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox
                            runat="server"
                            ID="DetailsTxt"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="3"
                            Columns="40"
                            placeholder="Indicaciones adicionales (opcional)">
                        </asp:TextBox>
                    </div>
                </asp:Panel>

                <!-- Realizar pedido -->

                <div class="mb-3">
                    <asp:Button
                        runat="server"
                        ID="SubmitOrder"
                        CssClass="btn btn-dark w-100"
                        Text="Realizar pedido"
                        OnClick="SubmitOrder_Click" />
                </div>
            </section>

            <section class="col-md-5 col-12 order-md-last order-first border-md-start border-sm-bottom px-md-4 px-2 mb-md-0 mb-3">

                <!-- Lista de productos -->

                <h2 class="fs-5 mb-3">Tus productos</h2>
                <ul class="list-group list-group-lg list-group-flush mb-auto">

                    <asp:Repeater ID="ProductSetsRpt" runat="server" OnItemDataBound="ProductSetsRpt_ItemDataBound">
                        <ItemTemplate>
                            <li class="list-group-item border-bottom">
                                <div class="row">
                                    <div class="col-2">
                                        <asp:Image
                                            runat="server"
                                            ID="ImageLbl"
                                            CssClass="w-100 object-fit-contain rounded"
                                            ImageUrl="https://cdn-icons-png.flaticon.com/512/3868/3868869.png" />
                                    </div>
                                    <div class="col-7">
                                        <asp:Label
                                            runat="server"
                                            ID="BrandLbl"
                                            Text='<%#Eval("Brand")%>'>
                                        </asp:Label>

                                        <asp:Label
                                            runat="server"
                                            ID="NameLbl"
                                            Text='<%#Eval("Name")%>'>
                                        </asp:Label>

                                        <p class="text-small mb-0">
                                            x <%#Eval("Quantity")%> unidades
                                        </p>

                                        <p class="text-small">
                                            Precio unitario: $ <%#Eval("Price")%>
                                        </p>
                                    </div>
                                    <div class="col-3 text-end">
                                        <p class="pe-2">
                                            $<%#Eval("Subtotal")%>
                                        </p>
                                    </div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>

                <!-- Total -->

                <div class="d-flex w-100 align-items-end flex-column mt-4">
                    <h2 class="fs-4">Total</h2>
                    <asp:Label
                        runat="server"
                        ID="TotalLbl"
                        CssClass="fs-5">
                    </asp:Label>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
