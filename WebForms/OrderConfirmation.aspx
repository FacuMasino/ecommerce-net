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
                    <div class="col">
                        <asp:TextBox
                            ID="FirstNameTxt"
                            runat="server"
                            Text="Nombre"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                    <div class="col">
                        <asp:TextBox
                            ID="LastNameTxt"
                            runat="server"
                            Text="Apellido"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>

                <!-- Email -->

                <div class="mb-3">
                    <asp:TextBox
                        ID="EmailTxt"
                        runat="server"
                        Text="tucorreo@electronico"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <!-- Forma de entrega -->

                <div class="mb-3">
                    <h2 class="fs-5 mb-3">Forma de Entrega</h2>
                    <div class="d-flex gap-2">
                        <asp:RadioButton
                            ID="DeliveryRB"
                            runat="server"
                            Checked="true"
                            GroupName="Delivery"
                            Text="Envío a domicilio"
                            OnCheckedChanged="DeliveryRB_CheckedChanged"
                            AutoPostBack="true"
                            />
                    </div>
                    <div class="d-flex gap-2">
                        <asp:RadioButton
                            ID="PickupRB"
                            runat="server"
                            Checked="false"
                            GroupName="Delivery"
                            Text="Retiro en sucursal"
                            OnCheckedChanged="PickupRB_CheckedChanged"
                            AutoPostBack="true"
                            />
                    </div>
                </div>

                <!-- Dirección de entrega -->

                <asp:Panel ID="AddressPnl" runat="server">
                    <div class="mb-3">
                        <h2 class="fs-5 mb-3">Dirección de entrega</h2>
                    </div>

                    <div class="mb-3">
                        <asp:DropDownList
                            ID="ProvincesDDL"
                            runat="server"
                            CssClass="form-select">
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox
                            ID="CityTxt"
                            runat="server"
                            Text="Ciudad"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>

                    <div class="row mb-3">
                        <div class="col">
                            <asp:TextBox
                                ID="StreetNameTxt"
                                runat="server"
                                Text="Calle"
                                CssClass="form-control">
                            </asp:TextBox>
                        </div>
                        <div class="col">
                            <asp:TextBox
                                ID="StreetNumberTxt"
                                runat="server"
                                Text="1234"
                                CssClass="form-control">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:TextBox
                                ID="ZipCodeTxt"
                                runat="server"
                                Text="Código Postal"
                                CssClass="form-control">
                            </asp:TextBox>
                        </div>
                        <div class="col">
                            <asp:TextBox
                                ID="FlatTxt"
                                runat="server"
                                Text="Depto/Lote"
                                CssClass="form-control">
                            </asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>

                <!-- Método de pago -->

                <div class="mb-3">
                    <h2 class="fs-5 mb-3">Método de pago</h2>
                    <div class="d-flex gap-2">
                        <asp:RadioButton
                            ID="CashRB"
                            runat="server"
                            Checked="true"
                            GroupName="PaymentType"
                            Text="Efectivo"
                            />
                    </div>
                    <div class="d-flex gap-2">
                        <asp:RadioButton
                            ID="MercadoPagoRB"
                            runat="server"
                            Checked="false"
                            GroupName="PaymentType"
                            Text="Mercado Pago"
                             />
                    </div>
                    <div class="d-flex gap-2">
                        <asp:RadioButton
                            ID="BankRB"
                            runat="server"
                            Checked="false"
                            GroupName="PaymentType"
                            Text="Transferencia bancaria"
                             />
                    </div>
                </div>

                <!-- Realizar pedido -->

                <div class="mb-3">
                    <asp:Button
                        ID="SubmitOrder"
                        runat="server"
                        Text="Realizar pedido"
                        OnClick="SubmitOrder_Click"
                        CssClass="btn btn-dark w-100" />
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
                                            ID="ImageLbl"
                                            runat="server"
                                            ImageUrl="https://cdn-icons-png.flaticon.com/512/3868/3868869.png"
                                            CssClass="w-100 object-fit-contain rounded" />
                                    </div>
                                    <div class="col-7">
                                        <asp:Label
                                            ID="BrandLbl"
                                            runat="server"
                                            Text='<%#Eval("Brand")%>'>
                                        </asp:Label>

                                        <asp:Label
                                            ID="NameLbl"
                                            runat="server"
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
                        ID="TotalLbl"
                        runat="server"
                        CssClass="fs-5"
                        Text="$97.000,00">
                    </asp:Label>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
