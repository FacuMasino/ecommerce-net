<%@ Page Title="Tu Carrito" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="WebForms.Cart" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="CSS/Style.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="d-flex flex-column align-items-center py-4 my-4">
        <%
            if (0 < ProductsCart.Count())
            {
        %>
        <div class="row gx-4 w-100 px-5 justify-content-center">
            <div class="col-8">
                <ul class="list-group list-group-lg list-group-flush border-top mb-auto">
                    <asp:Repeater OnItemDataBound="CartRepeater_ItemDataBound" runat="server" ID="CartRepeater">
                        <ItemTemplate>
                            <li class="list-group-item">
                                <div class="row">
                                    <div class="col-4">
                                        <a href="Product.aspx?id=<%#Eval("id")%>">
                                            <img class="img-fluid" id="articleImage" runat="server" onerror="this.src='Content/img/placeholder.jpg'">
                                        </a>
                                    </div>
                                    <div class="col-8 d-flex flex-column justify-content-between py-2">
                                        <div class="d-flex mb-2 fw-bold">
                                            <a class="text-body text-decoration-none" href="Product.aspx?id=<%#Eval("id")%>"><%#Eval("brand")%> - <%#Eval("name")%></a>
                                            <span class="text-muted ms-auto"> <%# Eval("price", "{0:C}")%></span>
                                         

                                            

                                        </div>
                                        <p class="text-muted mb-auto">Subtotal <%# Eval("subtotal", "{0:C}")%></p>
                                        <div class="d-inline-flex align-items-center justify-content-between w-100">
                                            <div class="itemcount bg-body-tertiary">
                                                <asp:LinkButton Text='<i class="bi bi-dash"></i>' CssClass="itemcount-control minus bg-body-tertiary text-decoration-none text-black fs-5 px-2" CommandArgument='<%#Eval("Id")%>' ID="removeLnkButton" OnClick="removeLnkButton_Click" runat="server" />
                                                <input type="number" class="itemcount-control bg-body-tertiary" value="<%#Eval("amount") %>" disabled>
                                                <asp:LinkButton Text='<i class="bi bi-plus"></i>' CssClass="itemcount-control plus bg-body-tertiary text-decoration-none text-black fs-5 px-2" CommandArgument='<%#Eval("Id")%>' ID="addLnkButton" OnClick="addLnkButton_Click" runat="server" />
                                            </div>
                                            <asp:LinkButton Text="X Eliminar" CssClass="text-muted text-decoration-none" CommandArgument='<%#Eval("Id")%>' ID="deleteLnkButton" OnClick="deleteLnkButton_Click" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="col-4">
                <div class="p-4 mb-4 bg-body-tertiary">
                    <ul class="list-group custom-list-group list-group-sm list-group-flush bg-body-tertiary">
                        <li class="list-group-item d-flex px-0 bg-transparent">
                            <span>Subtotal</span> <span class="ms-auto fs-sm">$ <%:ProductsCart.GetTotal() .ToString("0.00")%></span>
                        </li>
                        <li class="list-group-item d-flex px-0 bg-transparent">
                            <span>Iva 21%</span> <span class="ms-auto fs-sm">$<%:(ProductsCart.GetTotal() * (decimal)0.21).ToString("0.00")%></span>
                        </li>
                        <li class="list-group-item d-flex px-0 fs-lg fw-bold bg-transparent">
                            <span>Total</span> <span class="ms-auto fs-sm">$ <%:(ProductsCart.GetTotal() * (decimal)1.21).ToString("0.00")%></span>
                        </li>
                        <li class="list-group-item fs-sm text-center text-gray-500 bg-transparent">Costos de envío calculados al momento del pago *
                        </li>
                    </ul>
                </div>
                <a class="btn w-100 btn-dark mb-2" href="#">Ir a Pagar</a>
                <a class="mb-2 w-100 d-block text-black" href="Home.aspx">Volver a la tienda</a>
            </div>
        </div>
        <%
            }
            else
            {
        %>


        <div class="col-6">
            <h5 class="text-align-center">Todavía no agregaste ningún artículo a tu carrito...</h5>
            <img src="Content/img/add-to-cart.svg" class="img-fluid object-fit-cover h-75" />
        </div>
        <div class="col-6 text-center">
            <p>¡Elegí los productos que desees desde nuestra tienda!</p>
            <a href="Home.aspx" class="btn btn-primary text-center" type="button">Ir a la tienda</a>
        </div>


        <%

            }
        %>
    </div>
</asp:Content>
