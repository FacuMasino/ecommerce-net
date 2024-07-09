<%@ Page Title="" Language="C#" MasterPageFile="AdminMP.Master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="WebForms.Admin.Dashboard" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="d-flex flex-column container-1200 mx-auto gap-3">
        <h2 class="mb-4">Resumen</h2>
        <div class="row">
            <div class="col-md-3 mb-4">
                <div class="card h-100">
                    <div class="card-body d-flex flex-column justify-content-between align-items-center">
                        <div class="overview-icon mx-auto">
                            <i class="bi bi-rocket"></i>
                        </div>
                        <h5 class="card-title text-center">Ventas Concretadas</h5>
                        <p class="overview-value"><%:FinishedOrders%></p>
                    </div>
                </div>
            </div>
            <div class="col-md-3 mb-4">
                <div class="card h-100">
                    <div class="card-body d-flex flex-column justify-content-between align-items-center">
                        <div class="overview-icon mx-auto">
                            <i class="bi bi-box-seam"></i>
                        </div>
                        <h5 class="card-title text-center">Total Productos</h5>
                        <p class="overview-value"><%:ActiveProducts%></p>
                    </div>
                </div>
            </div>
            <div class="col-md-3 mb-4">
                <div class="card h-100">
                    <div class="card-body d-flex flex-column justify-content-between align-items-center">
                        <div class="overview-icon mx-auto">
                            <i class="bi bi-list-check"></i>
                        </div>
                        <h5 class="card-title text-center">Productos Vendidos</h5>
                        <p class="overview-value"><%:SoldProducts%></p>
                    </div>
                </div>
            </div>
            <div class="col-md-3 mb-4">
                <div class="card h-100">
                    <div class="card-body d-flex flex-column justify-content-between align-items-center">
                        <div class="overview-icon mx-auto">
                            <i class="bi bi-truck"></i>
                        </div>
                        <h5 class="card-title text-center">Productos Enviados</h5>
                        <p class="overview-value"><%:ShippedProducts%></p>
                    </div>
                </div>
            </div>
        </div>
        <!-- Listado Mas Vendidos -->
        <h2 class="fs-4 m-0 mb-3">Los más vendidos</h2>
        <div class="table-responsive card">
            <table class="table table-hover">
                <thead class="table-light">
                    <tr>
                        <th scope="col">Producto</th>
                        <th scope="col">Categoría</th>
                        <th scope="col">Precio</th>
                        <th scope="col">Ventas</th>
                    </tr>
                </thead>
                <tbody>
                    <% if (TotalTopSelling == 0)
                        {
                    %>
                    <tr>
                        <th scope="row" class="text-center fs-6 p-4" colspan="4">Ops! No hay productos para
                            mostrar.<br />
                        </th>
                    </tr>
                    <%}
                        else
                        {
                    %>
                    <asp:Repeater runat="server" ID="TopSellingProductsRpt">
                        <ItemTemplate>
                            <tr>
                                <th scope="row">
                                    <img src="<%#Eval("Images[0].Url")%>" class="img-table rounded" />
                                    <a class="text-black"
                                        href="/Product.aspx?id=<%#Eval("Id")%>"
                                        target="_blank">
                                        <%#Eval("Brand")%> - <%#Eval("name")%>
                                    </a>
                                </th>
                                <td class="align-middle" title="<%#Helper.GetCategoriesList((List<Category>)Eval("Categories"))%>">
                                    <%#Eval("Categories[0]")%>
                                    <%#Helper.PrintCategoriesCount(Eval("Categories"))%>
                                </td>
                                <td class="align-middle">$ <%#((decimal)Eval("Price")).ToString("F2")%></td>
                                <td class="align-middle"><%#Eval("TotalQuantitySold")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%} %>
                </tbody>

            </table>
        </div>
        <!-- Listado Mas Visitados -->
        <h2 class="fs-4 m-0 mb-3">Los más visitados</h2>
        <div class="table-responsive card">
            <table class="table table-hover">
                <thead class="table-light">
                    <tr>
                        <th scope="col">Producto</th>
                        <th scope="col">Categoría</th>
                        <th scope="col">Precio</th>
                        <th scope="col">Stock</th>
                        <th scope="col">Visitas</th>
                    </tr>
                </thead>
                <tbody>
                    <% if (TotalTopVisited == 0)
                        {
                    %>
                    <tr>
                        <th scope="row" class="text-center fs-6 p-4" colspan="4">Ops! No hay productos para
                            mostrar.<br />
                        </th>
                    </tr>
                    <%}
                        else
                        {
                    %>
                    <asp:Repeater runat="server" ID="TopVisitedProductsRpt">
                        <ItemTemplate>
                            <tr>
                                <th scope="row">
                                    <img src="<%#Eval("Images[0].Url")%>" class="img-table rounded" />
                                    <a class="text-black"
                                        href="/Product.aspx?id=<%#Eval("Id")%>"
                                        target="_blank">
                                        <%#Eval("Brand")%> - <%#Eval("name")%>
                                    </a>
                                </th>
                                <td class="align-middle" title="<%#Helper.GetCategoriesList((List<Category>)Eval("Categories"))%>">
                                    <%#Eval("Categories[0]")%>
                                    <%#Helper.PrintCategoriesCount(Eval("Categories"))%>
                                </td>
                                <td class="align-middle">$ <%#((decimal)Eval("Price")).ToString("F2")%></td>
                                <td class="align-middle"><%#Eval("stock")%></td>
                                <td class="align-middle"><%#Eval("TotalVisits")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%} %>
                </tbody>

            </table>
        </div>
    </div>
</asp:Content>
