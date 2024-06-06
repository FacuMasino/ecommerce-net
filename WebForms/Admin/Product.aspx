<%@ Page Title="Nuevo Producto" Language="C#" MasterPageFile="AdminMP.Master" AutoEventWireup="true"
    CodeBehind="Product.aspx.cs" Inherits="WebForms.Admin.ProductAdmin" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="d-flex flex-column gy-4 container-800 mx-auto gap-3">
        <% /* Agregar condición para que sea Editar/Nuevo
Y CAMBIAR PAGE TITLE */
        %>
        <h1 class="fs-4">Nuevo Producto</h1>
        <!-- Sección Info Básica -->
        <div class="d-flex flex-column border-1 border rounded p-3">
            <h2 class="fs-5">Información Básica</h2>
            <div class="mb-3">
                <label for="ProductName" class="form-label">Nombre</label>
                <input type="email" class="form-control" id="ProductName" placeholder="Ingresa el nombre de tu producto">
            </div>
            <div class="row justify-content-between gx-4 mb-3">
                <div class="col-md-6 col-12 mb-md-0 mb-3">
                    <label for="ProductBrandDDL" class="form-label">Marca</label>
                    <asp:DropDownList ID="ProductBrandDDL" CssClass="form-select" runat="server" />
                </div>
                <div class="col-md-6 col-12 mb-md-0 mb-3">
                    <label for="ProductCategoryDDL" class="form-label">Categoría</label>
                    <asp:DropDownList ID="ProductCategoryDDL" CssClass="form-select" runat="server" />
                </div>
            </div>
            <div class="mb-3">
                <label for="ProductDescription" class="form-label">Descripción</label>
                <textarea class="form-control" id="ProductDescription"
                    placeholder="Ingrese una breve descripción de hasta 300 caracteres"
                    rows="3"></textarea>
            </div>
        </div>
        <!-- Sección Imágenes -->
        <div class="d-flex flex-column border-1 border rounded p-3">
            <h2 class="fs-5">Imágenes</h2>
            <div class="mb-3">
                <label for="ProductImage" class="form-label">Agregar nueva</label>
                <div class="d-flex gap-3">
                    <input type="email" class="form-control" id="ProductImage" placeholder="http://urldelaimagen.com/producto.png">
                    <button class="btn btn-dark" type="button">Agregar</button>
                </div>
                <span class="form-text fw-bold">Tip: </span>
                <span class="form-text">Para un mejor resultado utilizar imágenes PNG transparentes
                    de 1024x1024px</span>
            </div>
            <hr />
            <div class="row row-cols-1 row-cols-md-4 g-4">
                <!-- Colocar en un repeater -->
                <div class="col">
                    <div class="card h-100 border-dashed">
                        <img src="https://ik.imagekit.io/tpce16/products/S10-01-1024x1024.png?updatedAt=1717372633354"
                            class="card-img-top" alt="Imagen de Galaxy S10" onerror="this.src='Content/img/placeholder.jpg'">
                        <div class="card-body border-1 border-top d-flex justify-content-end align-items-center">
                            <!-- convertir en asp buttons -->
                            <button href="#" class="btn py-0 fs-5"><i class="bi bi-trash"></i></button>
                            <button href="#" class="btn py-0 fs-5"><i class="bi bi-pencil-square"></i></button>
                        </div>
                    </div>
                </div>
                <!-- Fin Repeater -->
                <div class="col">
                    <div class="card h-100 border-dashed">
                        <img src="https://ik.imagekit.io/tpce16/products/prod-171-c1c8ff3e46f669939c15718532203779-1024-1024.jpg?updatedAt=1717374050896"
                            alt="Imagen de Galaxy S10"
                            class="card-img-top" onerror="this.src='Content/img/placeholder.jpg'">
                        <div class="card-body border-1 border-top d-flex justify-content-end align-items-center">
                            <!-- convertir en asp buttons -->
                            <button href="#" class="btn py-0 fs-5"><i class="bi bi-trash"></i></button>
                            <button href="#" class="btn py-0 fs-5"><i class="bi bi-pencil-square"></i></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Sección Precio -->
        <div class="d-flex flex-column border-1 border rounded p-3">
            <h2 class="fs-5">Precio</h2>
            <div class="mb-3">
                <label for="ProductPrice" class="form-label">Precio de venta</label>
                <input type="number" class="form-control" id="ProductSellPrice" placeholder="$ 0.00">
            </div>
            <div class="mb-3">
                <div class="d-flex justify-content-between gap-3">
                    <div class="col">
                        <label for="ProductCost" class="form-label">Costo</label>
                        <input type="number" class="form-control" id="ProductCost" placeholder="$ 0.00">
                    </div>
                    <div class="col">
                        <label for="ProductReturns" class="form-label">Ganancia</label>
                        <input type="number" class="form-control" id="ProductReturns" placeholder="0 %" disabled>
                    </div>
                </div>
                <span class="form-text">El costo del producto no se muestra al público.</span>
            </div>
        </div>
        <div class="d-flex gap-3 justify-content-end w-100">
            <a class="btn btn-outline-secondary" href="Products.aspx">Cancelar</a>
            <button class="btn btn-dark" type="button">Guardar Producto</button>
        </div>
    </div>
</asp:Content>
