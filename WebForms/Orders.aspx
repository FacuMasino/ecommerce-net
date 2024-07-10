<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Orders.aspx.cs" Inherits="WebForms.Orders" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="d-flex flex-column">
            <div class="mt-4">
                <asp:Label ID="GreetingLbl" runat="server" Text="Label" CssClass="fs-4 fw-bold"></asp:Label>
            </div>
            <ul class="nav align-self-end">
                <li class="nav-item">
                    <a class="nav-link fs-5 ps-0 text-black" href="Account.aspx">Mis Datos</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link fs-5  fw-bold ps-0 text-black active" aria-current="page" href="#">Mis Pedidos</a>
                </li>
            </ul>
            <div class="row g-0 border-top">
                <div class="col py-4">
                    <h2 class="fs-5 mb-3">Administrá y seguí tus pedidos</h2>
                    <div class="table-responsive">
                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Fecha</th>
                                    <th scope="col">Importe</th>
                                    <th scope="col">Acciones</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="OrdersRpt" runat="server">
                                    <ItemTemplate>
                                        <tr>

                                            <!-- Número de pedido -->

                                            <th scope="row">001</th>

                                            <!-- Fecha (de creación) -->

                                            <td scope="row">
                                                <asp:Label
                                                    ID="DateLbl"
                                                    runat="server"
                                                    Text='<%#Eval("CreationDate", "{0:dd/MM/yyyy}")%>'
                                                    CssClass="text-black">
                                                </asp:Label>
                                            </td>

                                            <!-- Importe -->

                                            <td>$ 123.456,00</td>

                                            <!-- Acciones -->

                                            <td>
                                                <div class="d-flex flex-md-row flex-column gap-2">

                                                    <!-- Editar -->

                                                    <asp:LinkButton
                                                        Text='<i class="bi bi-pencil-square"></i>'
                                                        CssClass="p-0 text-black fs-5"
                                                        CommandName="Edit"
                                                        CommandArgument='<%#Eval("Id")%>'
                                                        ID="EditOrderBtn"
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
            </div>
        </div>
    </div>
</asp:Content>
