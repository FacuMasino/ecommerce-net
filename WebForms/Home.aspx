﻿<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="WebForms.Home" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <%// Evita errores de intellisense (https://stackoverflow.com/a/31886588/10302170) %>
    <%=""%>
    <div class="row py-5">
        <div class="col-md-3 col-12">
            <ul class="nav flex-column">
                <li class="nav-item">
                    <asp:Panel ID="searchPanel" runat="server" CssClass="input-group mb-3" DefaultButton="searchBtn">
                        <asp:TextBox CssClass="form-control" ID="searchTextBox" runat="server" Text="" placeholder="Buscar producto"
                            required />
                        <asp:LinkButton Text='<i class="bi bi-search"></i>' ID="searchBtn" CssClass="btn btn-outline-primary rounded-end"
                            runat="server" OnClick="searchBtn_Click" />
                        <div class="invalid-feedback">
                            Ingrese al menos 2 caracteres.    
                        </div>
                    </asp:Panel>
                </li>
                <!-- Filtro de categorías -->
                <li class="nav-item">
                    <a class="nav-link fs-lg text-reset border-primary rounded-top border-2 border-bottom mb-6 px-2 fw-500 bg-body-tertiary"
                        data-bs-toggle="collapse" href="#categoriesCollapse" aria-expanded="true">Categorias
                    </a>
                    <div class="collapse show" id="categoriesCollapse">
                        <ul class="list-group py-1 list-group-flush">
                            <!-- Productos sin filtro -->
                            <li class="list-group-item pb-0 ps-2 border-0">
                                <a href="Home.aspx" class="text-decoration-none text-black">Todas
                                </a>
                            </li>
                            <%
                                foreach (Category category in Categories)
                                {
                            %>
                            <!-- Item Categoría -->
                            <li class="list-group-item pb-0 ps-2 border-0">
                                <a href="Home.aspx?catId=<%:category.Id%>" class="text-decoration-none text-black"><%:category.Name%>
                                </a>
                            </li>
                            <%
                                }
                            %>
                            <!-- Productos sin categoría -->
                            <li class="list-group-item pb-0 ps-2 border-0">
                                <a href="Home.aspx?catId=-1" class="text-decoration-none text-black">Sin Categoría
                                </a>
                            </li>
                        </ul>
                    </div>
                </li>
                <!-- Filtro de Marcas -->
                <li class="nav-item">
                    <a class="nav-link fs-lg text-reset border-primary rounded-top border-2 border-bottom mb-6 px-2 fw-500 bg-body-tertiary"
                        data-bs-toggle="collapse" href="#brandsCollapse" aria-expanded="true">Marcas
                    </a>
                    <div class="collapse" id="brandsCollapse">
                        <ul class="list-group py-1 list-group-flush">
                            <!-- Productos sin filtro -->
                            <li class="list-group-item pb-0 ps-2 border-0">
                                <a href="Home.aspx" class="text-decoration-none text-black">Todas
                                </a>
                            </li>
                            <%
                                foreach (Brand brand in Brands)
                                {
                            %>
                            <!-- Item Marca -->
                            <li class="list-group-item pb-0 ps-2 border-0">
                                <a href="Home.aspx?brandId=<%:brand.Id%>" class="text-decoration-none text-black"><%:brand.Name%>
                                </a>
                            </li>
                            <%
                                }
                            %>
                            <!-- Artículos sin Marca -->
                            <li class="list-group-item pb-0 ps-2 border-0">
                                <a href="Home.aspx?brandId=-1" class="text-decoration-none text-black">Sin Marca
                                </a>
                            </li>
                        </ul>
                    </div>
                </li>
            </ul>
        </div>
        <div class="col-md-9 col-12 mt-md-0 mt-3">
            <%
                if (Products.Count > 0)
                {
            %>
            <div class='row row-cols-2 row-cols-md-4 g-4'>
                <%
                    foreach (Product product in Products)
                    {
                        string imageUrl = "Content/img/placeholder.jpg";
                        string category = product.Categories[0].Name;

                        if (0 < product.Images.Count)
                        {
                            imageUrl = product.Images[0].Url;
                        }
                %>

                <div class='col'>
                    <div class="card shadow-sm shadow-hover h-100">
                        <img onclick="location.href='Product.aspx?id=<%:product.Id%>'" src="<%:imageUrl%>" class="card-img-top cursor-pointer" alt="Imagen de <%:product.Name%>" onerror="this.src='Content/img/placeholder.jpg'">
                        <div class="card-body d-flex flex-column">
                            <span class="mb-2 text-muted"><%:category.Length == 0 ? "Sin Categoría" : category%></span>
                            <h5 class="card-title fs-6 mb-0"><%:product.Name%></h5>
                            <small class="text-body-secondary mb-2"><%:product.Brand.ToString() %></small>
                            <p class="card-subtitle mb-0 mt-auto text-muted pe-3 fw-bold">
                                $<%:product.Price.ToString("0.00")%>
                            </p>
                        </div>
                        <div class="text-center p-2 bg-primary rounded-bottom">
                            <a href='<%= "Cart.aspx?id=" + product.Id %>' class="py-0 text-white fw-bold text-decoration-none">
                                <i class="bi fs-5 bi-cart-plus"></i>
                                Agregar al carrito
                            </a>
                        </div>
                    </div>
                </div>
                <%
                    } // Fin foreach
                    if (Products.Count != TotalProducts) // Si la cantidad es distinta, está filtrando
                    { // Mostrar un link para volver a ver todos
                %>
            </div>
            <div class="col text-center mt-4">
                <a href="Home.aspx" class="text-black">Ver Todos los productos</a>
            </div>
            <%
                    }
                }
                else // Mensaje si no hay productos que mostrar
                {
            %>
            <div class="col">
                <div class="d-flex flex-column align-items-center">
                    <div class="col-6 w-50">
                        <h5 class="text-align-center">Ups! Parece que el producto que buscas no existe...
                        </h5>
                        <img src="Content/img/Empty-Pana.svg" class="img-fluid object-fit-cover h-50">
                    </div>
                    <div class="col-6 text-center">
                        <p>¡Puedes ver todos los productos que tenemos para vos!</p>
                        <a href="Home.aspx" class="btn btn-dark text-center" type="button">Ver Productos</a>
                    </div>
                </div>
            </div>
            <%} %>
        </div>
    </div>
    </div>
</asp:Content>
