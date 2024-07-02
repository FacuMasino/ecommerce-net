<%@ Page Title="" Language="C#" MasterPageFile="AdminMP.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="WebForms.Admin.Orders" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="d-flex flex-column container-800 mx-auto gap-3">

        <!-- Título -->

        <div class="d-flex align-items-center justify-content-between">
            <h1 class="fs-4 m-0">Pedidos</h1>
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
                        <th scope="col">Cliente</th>
                        <th scope="col">Estado</th>
                        <th scope="col">Fecha</th>
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="OrdersListRpt">
                        <ItemTemplate>
                            <tr>
                                <td scope="row">

                                    <!-- Cliente -->

                                    <asp:Label
                                        ID="CategoryNameLbl"
                                        runat="server"
                                        Text='<%#Eval("User")%>'
                                        CssClass="text-black">
                                    </asp:Label>
                                </td>
                                <td>

                                    <!-- Estado -->

                                    <asp:Label
                                        ID="StatusLbl"
                                        runat="server"
                                        Text='<%#Eval("OrderStatus")%>'
                                        CssClass="text-black">
                                    </asp:Label>
                                </td>
                                <td>

                                    <!-- Fecha -->

                                    <asp:Label
                                        ID="DateLbl"
                                        runat="server"
                                        Text='<%#Eval("CreationDate", "{0:dd/MM/yyyy}")%>'
                                        CssClass="text-black">
                                    </asp:Label>
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
