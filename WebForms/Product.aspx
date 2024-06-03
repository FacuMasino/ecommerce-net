<%@ Page Title="Detalles del producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="WebForms.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="CSS/Style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <%
        if (0 < _product.Id)
        {
    %>
    <div class="d-flex mt-auto flex-column align-items-center justify-content-center">
        <h4 class="text-align-center">Detalles del artículo</h4>
        <div class="row">
            <div class="col-md-7">
                <div id="carouselExampleIndicators" class="carousel slide h-100">
                    <div class="carousel-inner h-100">
                        <%
                            string category = _product.Category.ToString() == "" ? "Sin Categoría" : _product.Category.ToString();
                            foreach (DomainModelLayer.Image image in _product.Images)
                            {
                        %>
                        <div class="carousel-item h-100 active">
                            <img src="<%: image.Url%>" class="object-fit-contain d-block" alt="Imagen de <%:_product.Name%>" width="300px" height="100%" onerror="this.src='Content/img/placeholder.jpg'" />
                        </div>
                        <%
                            }
                        %>
                    </div>
                    <% if (_product.Images.Count > 1)
                        {
                    %>
                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon text-black" aria-hidden="true"></span>
                        <span class="visually-hidden">Anterior</span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Siguiente</span>
                    </button>
                    <%} %>
                </div>
            </div>
            <div class="col-md-5 card mb-3">
                <div class="card-body d-flex flex-column justify-content-between h-100">
                    <div class="mb-4">
                        <p class="mb-0"><small class="text-body-secondary"><%:category%></small></p>
                        <h5 class="card-title mb-0"><%:_product.Name%></h5>
                        <p class="card-text"><small class="text-body-secondary"><%:_product.Brand.ToString()%></small></p>
                        <p class="card-text"><%:_product.Description.ToString()%></p>
                    </div>
                    <div class="d-flex justify-content-between border-1 border-bottom pb-2">
                        <p class="card-text fw-bold fs-5 align-self-end mb-0">$<%:_product.Price.ToString("0.00")%></p>
                        <div class="itemcount bg-body-tertiary">
                            <asp:LinkButton Text='<i class="bi bi-dash"></i>' CssClass="itemcount-control minus bg-body-tertiary text-decoration-none text-black fs-5 px-2" CommandArgument='<%#Eval("_product.Id")%>' ID="RemoveLnkButton" OnClick="RemoveLnkButton_Click" runat="server" />
                            <input type="number" class="itemcount-control bg-body-tertiary" value="<%:GetCartQty()%>" disabled>
                            <asp:LinkButton Text='<i class="bi bi-plus"></i>' CssClass="itemcount-control plus bg-body-tertiary text-decoration-none text-black fs-5 px-2"  CommandArgument='<%#Eval("_product.Id")%>' ID="AddLnkButton" OnClick="AddLnkButton_Click" runat="server" />
                        </div>
                    </div>
                    <div class="d-flex flex-column justify-content-around h-100">
                        <a href="Cart.aspx?Id=<%=_product.Id%>" class="btn btn-dark">Agregar al carrito </a>
                        <div class="d-flex flex-column">
                            <p class="card-text mb-1">Formas de pago:</p>
                            <ul>
                                <li>Mercadopago
                                </li>
                                <li>Transferencia bancaria
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>

    <%
        }
    %>
</asp:Content>
