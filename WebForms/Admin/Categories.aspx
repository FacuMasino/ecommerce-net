<%@ Page Title="Categorías" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="WebForms.Admin.Categories" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="d-flex flex-column container-800 mx-auto gap-3">

        <!-- Título y botón Nueva Categoría -->

        <div class="d-flex align-items-center justify-content-between">
            <h1 class="fs-4 m-0">Categorías</h1>
            <asp:Button ID="NewCategoryBtn" runat="server" Text="Nueva categoría" OnClick="NewCategoryBtn_Click" CssClass="btn btn-dark" />
        </div>

        <!-- Buscador -->

        <asp:Panel ID="SearchPanel" runat="server" CssClass="input-group mb-3" DefaultButton="searchBtn">
            <asp:TextBox
                CssClass="form-control"
                ID="SearchTextBox"
                runat="server"
                Text=""
                placeholder="Buscar por nombre" />
            <asp:LinkButton
                Text='<i class="bi bi-search"></i>'
                ID="SearchBtn"
                CssClass="btn rounded-end btn-outline-secondary"
                runat="server"
                OnClick="SearchBtn_Click" />
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
                    <asp:Repeater runat="server" ID="CategoriesListRpt" OnItemCommand="CategoriesListRpt_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td scope="row">

                                    <!-- Nombre -->

                                    <asp:Label
                                        ID="CategoryNameLbl"
                                        runat="server"
                                        Text='<%#Eval("Name")%>'
                                        CssClass="text-black">
                                    </asp:Label>

                                    <!-- Editar nombre (oculto) -->

                                    <asp:TextBox
                                        ID="EditCategoryNameTxt"
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
                                            ID="EditCategoryBtn"
                                            runat="server" />

                                        <!-- Guardar (oculto) -->

                                        <asp:LinkButton
                                            Text='<i class="bi bi-check"></i>'
                                            CssClass="p-0 text-black fs-5 d-none"
                                            CommandName="Save"
                                            CommandArgument='<%#Eval("Id")%>'
                                            ID="SaveCategoryBtn"
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
                                            ID="DeleteCategoryBtn"
                                            runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
