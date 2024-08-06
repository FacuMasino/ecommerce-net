<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="WebForms.Admin.Users" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="d-flex flex-column container-800 mx-auto gap-3">
        <div class="d-flex align-items-center justify-content-between">
            <h1 class="fs-4 m-0">Usuarios</h1>
        </div>

        <!-- Buscador -->

        <asp:Panel ID="SearchPanel" runat="server" CssClass="input-group mb-3" DefaultButton="searchBtn">
            <asp:TextBox
                CssClass="form-control"
                ID="SearchTextBox"
                runat="server"
                Text=""
                onKeyUp="checkSearchBtn();"
                placeholder="Buscar usuario" />
            <asp:LinkButton
                Text='<i class="bi bi-search"></i>'
                ID="SearchBtn"
                CssClass="btn rounded-end btn-outline-secondary"
                runat="server"
                ClientIDMode="Static"
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
                        <th scope="col">Apellido</th>
                        <th scope="col">Usuario</th>
                        <th scope="col">Email</th>
                        <th scope="col">Roles</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="UsersRpt" OnItemDataBound="UsersRpt_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label
                                        ID="FirstNameLbl"
                                        runat="server"
                                        Text='<%#Eval("FirstName")%>'
                                        CssClass="text-black">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label
                                        ID="LastNameLbl"
                                        runat="server"
                                        Text='<%#Eval("LastName")%>'
                                        CssClass="text-black">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label
                                        ID="UsernameLbl"
                                        runat="server"
                                        Text='<%#Eval("Username")%>'
                                        CssClass="text-black">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label
                                        ID="EmailLbl"
                                        runat="server"
                                        Text='<%#Eval("Email")%>'
                                        CssClass="text-black">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Repeater ID="RolesRpt" runat="server" OnItemDataBound="RolesRpt_ItemDataBound" OnItemCommand="RolesRpt_ItemCommand">
                                        <ItemTemplate>
                                            <asp:LinkButton
                                                ID="RoleBtn"
                                                runat="server"
                                                CommandName="RoleClick"
                                                CommandArgument='<%# Eval("Id") %>'
                                                ToolTip='<%# Eval("Name") %>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
