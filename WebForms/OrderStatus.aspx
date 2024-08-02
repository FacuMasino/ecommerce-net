<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="OrderStatus.aspx.cs" Inherits="WebForms.OrderStatus" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="row container-1200 mx-auto">
        <div class="d-flex flex-column mb-4">
            <asp:Label ID="OrderIdLbl" runat="server" CssClass="fs-3 m-0 fw-bold"></asp:Label>
            <asp:Label ID="OrderCreationDateLbl" runat="server" CssClass="fs-7"></asp:Label>
        </div>

        <!--- Detalles del pedido --->

        <section class="col-md-7 col d-flex flex-column gap-3">
            <div class="d-flex justify-content-between">
                <h2 class="fs-4 m-0">Detalles del pedido</h2>
            </div>

            <!-- Productos -->

            <div class="bg-white py-2 border-1 border rounded">
                <div class="d-flex justify-content-between align-items-center border-bottom px-3 pb-2">
                    <h3 class="fs-5 m-0 fw-normal">Productos</h3>
                </div>

                <div class="d-flex flex-column gap-2 px-3 py-2">

                    <!-- Lista de productos -->

                    <asp:Repeater ID="ProductSetsRpt" runat="server" OnItemDataBound="ProductSetsRpt_ItemDataBound">
                        <ItemTemplate>
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
                                        Precio unitario: $ <%#((decimal)Eval("Price")).ToString("F2")%>
                                    </p>
                                </div>
                                <div class="col-3 text-end">
                                    <p class="pe-2">
                                        $<%#((decimal)Eval("Subtotal")).ToString("F2")%>
                                    </p>
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
                            <span class="fw-bold">Total:</span>
                            <asp:Label ID="TotalLbl" runat="server" CssClass="fs-7"></asp:Label>
                        </div>
                    </div>
                    <div>
                        <div class="px-3 py-2 d-flex align-items-center justify-content-between ">
                            <span class="fw-bold">Medio de pago: </span>
                            <asp:Label ID="PaymentTypeLbl" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Estado de orden -->

            <div class="bg-white py-2 border-1 border rounded mb-3">
                <div class="d-flex justify-content-between align-items-center border-bottom px-3 pb-2">
                    <h3 class="fs-5 m-0 fw-normal">Estado de orden</h3>
                </div>
                <div class="px-3 py-2">
                    <ul class="list-unstyled">
                        <li>
                            <i class="bi bi-check-circle-fill text-success"></i>
                            <asp:Label
                                ID="OrderGeneratedLbl"
                                runat="server">
                            </asp:Label>
                        </li>

                        <asp:Repeater ID="AcceptedStatusesRpt" runat="server">
                            <ItemTemplate>
                                <li>
                                    <i class="bi bi-check-circle-fill text-success"></i>
                                    <asp:Label
                                        ID="AcceptedStatusLbl"
                                        runat="server"
                                        Text='<%#Eval("AcceptedText")%>'>
                                    </asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>

                        <li>
                            <asp:Label
                                ID="OrderStatusIco"
                                runat="server">
                            </asp:Label>
                            <asp:Label
                                ID="OrderStatusLbl"
                                runat="server">
                            </asp:Label>
                        </li>
                    </ul>
                </div>
                <div class="d-flex flex-column gap-2">
                    <div class="px-3 py-2 d-flex align-items-center justify-content-between">
                        <asp:Button ID="TransitionButton" runat="server" CssClass="btn btn-primary w-auto" />
                    </div>
                </div>
            </div>
        </section>

        <!-- Información del cliente -->

        <section class="col-md-5 col d-flex flex-column gap-3">
            <h2 class="fs-4 m-0">Información del cliente</h2>

            <!-- Dirección de envío -->

            <div class="bg-white py-2 border-1 border rounded">
                <div class="d-flex align-items-center border-bottom px-3 py-2">
                    <h3 class="fs-5 m-0 fw-normal">Dirección de envío</h3>
                </div>
                <div class="px-3 py-2">
                    <div class="d-flex flex-column gap-2">
                        <div>
                            <asp:Label ID="StreetNameLbl" runat="server" CssClass="fw-bold"></asp:Label>
                            <asp:Label ID="StreetNumberLbl" runat="server" CssClass="fw-bold"></asp:Label>
                        </div>
                        <asp:Label ID="FlatLbl" runat="server"></asp:Label>
                        <asp:Label ID="CityLbl" runat="server"></asp:Label>
                        <asp:Label ID="ProvinceLbl" runat="server"></asp:Label>
                        <asp:Label ID="DetailsLbl" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
