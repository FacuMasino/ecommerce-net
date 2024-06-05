<%@ Page Title="Productos" Language="C#" MasterPageFile="AdminMP.Master" AutoEventWireup="true"
    CodeBehind="Products.aspx.cs" Inherits="WebForms.Admin.Products" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="d-flex flex-column gy-4 container-800 mx-auto gap-3">
        <div class="d-flex align-items-center justify-content-between">
            <h1 class="fs-4 m-0">Productos</h1>
            <a class="btn btn-dark" href="Product.aspx">Nuevo producto</a>
        </div>
        <!-- BUSCADOR -->
        <asp:Panel ID="SearchPanel" runat="server" CssClass="input-group mb-3" DefaultButton="searchBtn">
            <asp:TextBox CssClass="form-control" ID="SearchTextBox" runat="server" Text="" placeholder="Buscar por nombre, marca, etc"
                required />
            <asp:LinkButton Text='<i class="bi bi-search"></i>' ID="SearchBtn" CssClass="btn rounded-end btn-outline-secondary"
                runat="server" OnClick="SearchBtn_Click" />
            <div class="invalid-feedback">
                Ingrese al menos 2 caracteres.    
            </div>
        </asp:Panel>
        <!-- Listado -->
        <div class="table-responsive card">
            <table class="table table-hover">
                <thead class="table-light">
                    <tr>
                        <th scope="col">Producto</th>
                        <th scope="col">Categoría</th>
                        <th scope="col">Precio</th>
                        <th scope="col">Stock</th>
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="ProductListRepeater">
                        <ItemTemplate>
                            <tr>
                                <th scope="row">
                                    <a  class="text-black"
                                        href="/Product.aspx?id=<%#Eval("Id")%>" 
                                        target="_blank">
                                        <%#Eval("Brand")%> - <%#Eval("name")%>
                                    </a>
                                </th>
                                <td><%#Eval("Category")%></td>
                                <td>$ <%#Eval("Price")%></td>
                                <td><%#Eval("Stock")%></td>
                                <td>Test</td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>

            </table>
        </div>
    </div>
</asp:Content>
