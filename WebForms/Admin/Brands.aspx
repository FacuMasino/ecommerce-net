<%@ Page Title="Marcas" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Brands.aspx.cs" Inherits="WebForms.Admin.Brands" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="d-flex flex-column container-800 mx-auto gap-3">

        <!-- Título y botón Nueva Marca -->

        <div class="d-flex align-items-center justify-content-between">
            <h1 class="fs-4 m-0">Marcas</h1>
            <asp:Button ID="NewBrandBtn" runat="server" Text="Nueva marca" OnClick="NewBrandBtn_Click" CssClass="btn btn-dark" />
        </div>

        <!-- Buscador -->

        <asp:Panel ID="SearchPanel" runat="server" CssClass="input-group mb-3" DefaultButton="searchBtn">
            <asp:TextBox
                CssClass="form-control"
                ID="SearchTextBox"
                runat="server"
                Text=""
                onKeyUp="checkSearchBtn();"
                placeholder="Buscar por nombre" />
            <asp:LinkButton
                Text='<i class="bi bi-search"></i>'
                ID="SearchBtn"
                CssClass="btn rounded-end btn-outline-secondary"
                runat="server"
                OnClick="SearchBtn_Click"
                ClientIDMode="Static" />
            <div class="invalid-feedback">
                Ingrese al menos 2 caracteres.    
            </div>
        </asp:Panel>

        <!-- Listado -->

        <div class="table-responsive card">
            <table class="table table-hover">
                <thead class="table-light">
                    <tr>
                        <th scope="col">Nombre</th>
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <% if (TotalBrands == 0)
                        {
                    %>
                    <tr>
                        <th scope="row" class="text-center fs-6 p-4" colspan="2">Ops! No hay marcas para mostrar.<br />
                        </th>
                    </tr>
                    <%}
                        else
                        {
                    %>
                    <asp:Repeater runat="server" ID="BrandsListRpt" OnItemCommand="BrandsListRpt_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td scope="row">

                                    <!-- Nombre -->

                                    <asp:Label
                                        ID="BrandNameLbl"
                                        runat="server"
                                        Text='<%#Eval("Name")%>'
                                        CssClass="text-black">
                                    </asp:Label>

                                    <!-- Editar nombre (oculto) -->

                                    <asp:TextBox
                                        ID="EditBrandNameTxt"
                                        runat="server"
                                        Text='<%#Eval("Name")%>'
                                        CssClass="form-control d-none" />
                                </td>
                                <td>
                                    <div class="d-flex gap-2">

                                        <!-- Editar -->

                                        <asp:LinkButton
                                            Text='<i class="bi bi-pencil-square"></i>'
                                            CssClass="p-0 text-black fs-5"
                                            CommandName="Edit"
                                            CommandArgument='<%#Eval("Id")%>'
                                            ID="EditBrandBtn"
                                            runat="server" />

                                        <!-- Guardar (oculto) -->

                                        <asp:LinkButton
                                            Text='<i class="bi bi-check"></i>'
                                            CssClass="p-0 text-black fs-5 d-none"
                                            CommandName="Save"
                                            CommandArgument='<%#Eval("Id")%>'
                                            ID="SaveBrandBtn"
                                            runat="server" />

                                        <!-- Cancelar (oculto) -->

                                        <asp:LinkButton
                                            Text='<i class="bi bi-x"></i>'
                                            CssClass="p-0 text-black fs-5 d-none"
                                            CommandName="Cancel"
                                            CommandArgument='<%#Eval("Id")%>'
                                            ID="CancelEditBtn"
                                            runat="server" />

                                        <!-- Eliminar -->

                                        <asp:LinkButton
                                            Text='<i class="bi bi-trash3"></i>'
                                            CssClass="p-0 text-black fs-5"
                                            CommandName="Delete"
                                            CommandArgument='<%#Eval("Id")%>'
                                            ID="DeleteBrandBtn"
                                            runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%} %>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
