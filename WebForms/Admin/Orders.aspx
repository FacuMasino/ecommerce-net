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
                        <th scope="col">Fecha</th>
                        <th scope="col">Cliente</th>
                        <th scope="col">Distribución</th>
                        <th scope="col">Estado</th>
                        <th scope="col">Datos de entrega</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="OrdersListRpt" OnItemDataBound="OrdersListRpt_ItemDataBound">
                        <ItemTemplate>
                            <tr>

                                <!-- Fecha (de creación) -->

                                <td scope="row">
                                    <asp:Label
                                        ID="DateLbl"
                                        runat="server"
                                        Text='<%#Eval("CreationDate", "{0:dd/MM/yyyy}")%>'
                                        CssClass="text-black">
                                    </asp:Label>
                                </td>

                                <!-- Cliente -->

                                <td>
                                    <asp:Label
                                        ID="CategoryNameLbl"
                                        runat="server"
                                        Text='<%#Eval("User")%>'
                                        CssClass="text-black">
                                    </asp:Label>
                                </td>

                                <!-- Canal de distribución -->

                                <td>
                                    <asp:Label
                                        ID="DistributionChannelLbl"
                                        runat="server"
                                        Text='<%#Eval("DistributionChannel")%>'
                                        CssClass="text-black">
                                    </asp:Label>
                                </td>

                                <!-- Estado -->

                                <td>
                                    <asp:DropDownList
                                        ID="OrderStatusesDDL"
                                        runat="server"
                                        OnSelectedIndexChanged="OrderStatusesDDL_SelectedIndexChanged"
                                        AutoPostBack="true"
                                        CssClass="dropdown btn btn-secondary dropdown-toggle w-100">
                                    </asp:DropDownList>
                                </td>

                                <!-- Datos de entrega -->

                                <td>
                                    <div class="d-flex gap-2">

                                        <!-- Dirección -->

                                        <asp:Label
                                            ID="DeliveryAddressLbl"
                                            runat="server"
                                            Text='<%#Eval("DeliveryAddress")%>'
                                            CssClass="text-black">
                                        </asp:Label>

                                        <!-- Fecha (de entrega) -->

                                        <asp:Label
                                            ID="DeliveryDateLbl"
                                            runat="server"
                                            Text='<%#Eval("DeliveryDate", "{0:dd/MM/yyyy}")%>'
                                            CssClass="text-black">
                                        </asp:Label>

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
