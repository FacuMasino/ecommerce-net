﻿<%@ Page Title="Nuevo Producto" Language="C#" MasterPageFile="AdminMP.Master" AutoEventWireup="true"
    CodeBehind="Product.aspx.cs" Inherits="WebForms.Admin.ProductAdmin" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="d-flex flex-column container-800 mx-auto gap-3">
        <%
            if (CurrentProduct.Name != null || !IsEditing)
            {
        %>
        <h1 class="fs-4"><%: IsEditing ? "Editar" : "Nuevo" %> Producto</h1>
        <!-- Sección Info Básica -->
        <div class="d-flex flex-column border-1 border rounded p-3">
            <h2 class="fs-5">Información Básica</h2>
            <div class="mb-3">
                <label for="ProductName" class="form-label">Nombre</label>
                <input type="text" class="form-control" id="ProductName" runat="server" placeholder="Ingresa el nombre de tu producto">
            </div>
            <div class="row justify-content-between gx-4 mb-3">
                <div class="col-md-6 col-12 mb-md-0 mb-3">
                    <label for="ProductBrandDDL" class="form-label">Marca</label>
                    <asp:DropDownList ID="ProductBrandDDL" CssClass="form-select" runat="server" />
                </div>
                <div class="col-md-6 col-12 mb-md-0 mb-3">
                    <label for="ProductCode" class="form-label">SKU</label>
                    <asp:TextBox class="form-control" ID="ProductCode" placeholder="AAA-BBB-100"
                        runat="server" />
                </div>
            </div>
            <div class="mb-3">
                <label for="ProductDescription" class="form-label">Descripción</label>
                <textarea class="form-control" id="ProductDescription"
                    placeholder="Ingrese una breve descripción de hasta 300 caracteres"
                    rows="3" maxlength="300" runat="server"></textarea>
            </div>
        </div>

        <!-- Sección Categorías -->
        <div class="d-flex flex-column border-1 border rounded p-3">
            <h2 class="fs-5">Categorías</h2>
            <div class="mb-3">
                <div class="d-flex gap-3">
                    <asp:DropDownList ID="CategoriesDdl" CssClass="form-select" runat="server" />
                    <asp:Button ID="AddCategoryBtn" class="btn btn-dark" OnClick="AddCategoryBtn_Click"
                        runat="server" Text="Agregar" />
                </div>
            </div>
            <%
                if (CurrentProduct.Categories.Count > 0)
                {
            %>
            <hr />
            <div class="row row-cols-1 row-cols-md-4 gx-4 my-3">
                <asp:Repeater ID="ProductCategoriesRpt" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card p-3">
                                <div class="d-flex justify-content-between align-items-center">
                                    <div>
                                        <h6 class="m-0 text-center"><%#Eval("Name")%></h6>
                                    </div>
                                    <div class="card-body p-0 ms-3 d-flex justify-content-end align-items-center">
                                        <button href="#" class="btn p-0 fs-5"><i class="bi h-fit d-flex bi-trash"></i></button>
                                        <!-- hack: convertir en asp buttons -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <%
                }
            %>
        </div>

        <!-- Sección Imágenes -->
        <div class="d-flex flex-column border-1 border rounded p-3">
            <h2 class="fs-5">Imágenes</h2>
            <div class="mb-3">
                <label for="ProductImage" class="form-label">Agregar nueva</label>
                <div class="d-flex gap-3">
                    <asp:TextBox TextMode="Url" class="form-control" ID="ProductImageUrl" runat="server"
                        placeholder="http://urldelaimagen.com/producto.png" />
                    <asp:Button ID="AddImageBtn" class="btn btn-dark" OnClick="AddImageBtn_Click" runat="server"
                        Text="Agregar" />
                </div>
                <span class="form-text fw-bold">Tip: </span>
                <span class="form-text">Para un mejor resultado, utilizar imágenes PNG transparentes
                    de 1024x1024px</span>
            </div>
            <% if (CurrentProduct.Images.Count > 0)
                // Repeater condicional, solo si hay imágenes
                {
            %>
            <hr />
            <div class="row row-cols-1 row-cols-md-4 g-4">
                <!-- Imagenes -->
                <asp:Repeater ID="ProductImagesRPT" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card h-100 border-dashed">
                                <img src="<%#Eval("Url")%>"
                                    class="card-img-top" alt="Imagen de Galaxy S10" onerror="this.src='Content/img/placeholder.jpg'">
                                <div class="card-body border-1 border-top d-flex justify-content-end align-items-center">
                                    <asp:LinkButton Text='<i class="bi bi-trash"></i>' CssClass="text-decoration-none text-black fs-5 py-0" CommandArgument='<%#Eval("Id")%>' ID="RemoveImgLnkButton" OnClick="RemoveImgLnkButton_Click" runat="server" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <%
                }
            %>
        </div>

        <!-- Sección Precio -->
        <div class="d-flex flex-column border-1 border rounded p-3">
            <h2 class="fs-5">Precio y Stock</h2>
            <div class="mb-3">
                <div class="d-flex justify-content-between gap-3">
                    <div class="col">
                        <label for="ProductPrice" class="form-label">Precio de venta</label>
                        <asp:TextBox pattern="-?[0-9]+[\,.]*[0-9]+" class="form-control" ID="ProductPrice"
                            placeholder="0,00"
                            OnTextChanged="CalcReturns_TextChanged" step=".01" AutoPostBack="true" runat="server" />
                    </div>
                    <div class="col">
                        <label for="ProductStock" class="form-label">Cantidad disponible</label>
                        <asp:TextBox TextMode="Number" class="form-control" ID="ProductStock" placeholder="0"
                            runat="server" />
                    </div>
                </div>
            </div>
            <div class="mb-3">
                <div class="d-flex justify-content-between gap-3">
                    <div class="col">
                        <label for="ProductCost" class="form-label">Costo</label>
                        <asp:TextBox pattern="-?[0-9]+[\,.]*[0-9]+" CssClass="form-control" ID="ProductCost"
                            placeholder="0,00" OnTextChanged="CalcReturns_TextChanged" AutoPostBack="true"
                            step="any" runat="server" />
                    </div>
                    <div class="col">
                        <label for="ProductReturns" class="form-label">Ganancia por venta</label>
                        <asp:TextBox CssClass="form-control" ID="ProductReturns" placeholder="0 %" Enabled="false"
                            runat="server" />
                    </div>
                </div>
                <span class="form-text">El costo del producto no se muestra al público.</span>
            </div>
        </div>
        <div class="d-flex gap-3 justify-content-between w-100">
            <Asp:Button ID="DeleteProductBtn" OnClick="DeleteProductBtn_Click" CssClass="btn btn-outline-danger" runat="server" Text="Eliminar" />
            <div>
                <a class="btn btn-outline-secondary" href="Products.aspx">Cancelar</a>
                <Asp:Button CssClass="btn btn-dark" Text="Guardar Producto" runat="server" />
            </div>
        </div>
        <%
            }
            else
            {
        %>
        <div class="col-md-8 col align-self-center text-center">
            <h5 class="fs-4 text-align-center mb-3">Ups! El producto que intentás editar no existe<br /> o ya no está disponible.
            </h5>
            <p class="fs-5">Intentá elegir otro desde la lista</p>
            <img src="/Content/img/Empty-cuate.svg" class="img-fluid object-fit-cover h-75" />
            <a href="Products.aspx" class="btn btn-dark text-center" type="button">Ver Productos</a>
        </div>
        <div class="col-md-8 col text-center">
        </div>
        <%
            }
        %>
    </div>
</asp:Content>
