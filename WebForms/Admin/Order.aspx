<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true"
    CodeBehind="Order.aspx.cs" Inherits="WebForms.Admin.OrderPage" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="row container-1200 mx-auto">
        <div class="d-flex justify-content-between align-items-center">
            <h1 class="fs-4 mb-3">Pedido #0001</h1>
            <span>3 de Julio de 2024</span>
        </div>

        <!--- Detalles del pedido --->

        <section class="col-md-7 col d-flex flex-column gap-3">
            <div class="d-flex justify-content-between">
                <h2 class="fs-4 m-0">Detalles del pedido</h2>
            </div>

            <!-- Productos -->

            <div class="bg-white py-2 border-1 border rounded">
                <div class="d-flex justify-content-between align-items-center border-bottom px-3 py-2">
                    <h3 class="fs-5 m-0 fw-normal">Productos</h3>
                </div>

                <div class="d-flex flex-column gap-2 px-3 py-2 border-bottom">

                    <!-- Lista de productos -->

                    <asp:Repeater ID="ProductsRpt" runat="server">
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-2">
                                    <img class="w-100 object-fit-contain rounded"
                                        src="https://ik.imagekit.io/tpce16/products/iphone-15-pro-a-2f70988805588cc27816964316066050-1024-1024.png?updatedAt=1718288634185" />
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

                                    <span class="text-small">1x $ P.Unit</span>
                                </div>
                                <div class="col-3 text-end">
                                    <p class="pe-2">$ Total</p>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>

            <!-- Información de pago -->

            <div class="bg-white py-2 border-1 border rounded">
                <div class="d-flex align-items-center border-bottom px-3 py-2">
                    <h3 class="fs-5 m-0 fw-normal">Pago</h3>
                </div>
                <div class="d-flex flex-column gap-2">
                    <div class="bg-body-tertiary">
                        <div class="px-3 py-2 d-flex align-items-center justify-content-between ">
                            <span class="fw-bold">Total: </span>
                            <span>$ Precio Total</span>
                        </div>
                    </div>
                    <div>
                        <div class="px-3 py-2 d-flex align-items-center justify-content-between ">
                            <span class="fw-bold">Medio de pago: </span>
                            <span>A Convenir</span>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Administrar estado -->
            <div class="bg-white py-2 border-1 border rounded">
                <div class="d-flex align-items-center border-bottom px-3 py-2">
                    <h3 class="fs-5 m-0 fw-normal">Administrar estado</h3>
                </div>
                <div class="px-3 py-2">
                    <asp:DropDownList
                        ID="OrderStatusesDDL"
                        runat="server"
                        OnSelectedIndexChanged="OrderStatusesDDL_SelectedIndexChanged"
                        AutoPostBack="true"
                        CssClass="dropdown btn btn-secondary dropdown-toggle w-100">
                    </asp:DropDownList>
                </div>
            </div>
        </section>

        <!-- Información del cliente -->

        <section class="col-md-5 col d-flex flex-column gap-3">
            <h2 class="fs-4 m-0">Información del cliente</h2>

            <!-- Cliente -->

            <div class="bg-white py-2 border-1 border rounded">
                <div class="d-flex align-items-center border-bottom px-3 py-2">
                    <h3 class="fs-5 m-0 fw-normal">Cliente</h3>
                </div>
                <div class="px-3 py-2">
                    <div class="d-flex flex-column gap-2">
                        <span class="fw-bold">Nombre - Apellido</span>
                        <span><a href="mailto:Email@email.com" target="_blank">Email@email.com</a></span>
                        <span>Tel. de contacto: No posee</span>
                    </div>
                </div>
            </div>

            <!-- Dirección de envío -->

            <div class="bg-white py-2 border-1 border rounded">
                <div class="d-flex align-items-center border-bottom px-3 py-2">
                    <h3 class="fs-5 m-0 fw-normal">Dirección de envío</h3>
                </div>
                <div class="px-3 py-2">
                    <div class="d-flex flex-column gap-2">
                        <span>Siempreviva 742</span>
                        <span class="text-muted">CP: Numero</span>
                        <span class="text-muted">Springfield, EEUU</span>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
