<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Orders.aspx.cs" Inherits="WebForms.Orders" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="d-flex flex-column">
        <div class="mt-4">
            <h2 class="fs-4 fw-bold">Hola Nombre!</h2>
            <p class="fs-5">Bienvenido a tu cuenta.</p>
        </div>
        <ul class="nav align-self-end">
            <li class="nav-item">
                <a class="nav-link fs-5 ps-0 text-black" href="Account.aspx">Mis Datos</a>
            </li>
            <li class="nav-item">
                <a class="nav-link fs-5  fw-bold ps-0 text-black active" aria-current="page" href="#">
                    Mis Pedidos</a>
            </li>
        </ul>
        <div class="row g-0 border-top">
            <div class="col py-4">
                <h2 class="fs-5 mb-3">Administrá y seguí tus pedidos</h2>
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th scope="col">Nro.</th>
                            <th scope="col">Productos</th>
                            <th scope="col">Cantidad</th>
                            <th scope="col">Importe</th>
                            <th scope="col">Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <!-- Repeater -->
                        <tr>
                            <th scope="row">001</th>
                            <td title="Samsung - Galaxy S10, Sony - PlayStation 5">Samsung - Galaxy S10, Sony -
                                PlayStation 5
                            </td>
                            <td>2</td>
                            <td>$ 894245.00</td>
                            <td>
                                <div class="d-flex gap-2">
                                    <!-- Ver detalle -->
                                    <a href="OrderStatus.aspx?order=1111>" class="p-0 text-black">Ver detalle
                                    </a>
                                    <!-- Cancelar -->
                                    <a href="#" class="p-0 text-black">Cancelar    
                                    </a>
                                </div>
                            </td>
                        </tr>
                        <!-- Fin Repeater -->
                        <tr>
                            <th scope="row">001</th>
                            <td title="Samsung - Galaxy S10, Sony - PlayStation 5">Samsung - Galaxy S10, Sony -
                                PlayStation 5
                            </td>
                            <td>2</td>
                            <td>$ 894245.00</td>
                            <td>
                                <div class="d-flex gap-2">
                                    <!-- Ver detalle -->
                                    <a href="OrderStatus.aspx?order=1111>" class="p-0 text-black">Ver detalle
                                    </a>
                                    <!-- Cancelar -->
                                    <a href="#" class="p-0 text-black">Cancelar    
                                    </a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">001</th>
                            <td title="Samsung - Galaxy S10, Sony - PlayStation 5">Samsung - Galaxy S10, Sony -
                                PlayStation 5
                            </td>
                            <td>2</td>
                            <td>$ 894245.00</td>
                            <td>
                                <div class="d-flex gap-2">
                                    <!-- Ver detalle -->
                                    <a href="OrderStatus.aspx?order=1111>" class="p-0 text-black">Ver detalle
                                    </a>
                                    <!-- Cancelar -->
                                    <a href="#" class="p-0 text-black">Cancelar    
                                    </a>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
