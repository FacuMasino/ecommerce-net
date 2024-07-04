<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="OrderStatus.aspx.cs" Inherits="WebForms.OrderStatus" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container mt-5">
        <h2 class="mb-3 text-center">Detalle del pedido #001</h2>
        <div class="row border-2 border-top border-black py-2">
            <div class="col-8">
                <div class="row border-2 border-bottom border-black">
                    <div class="col-md-6">
                        <h5>Cliente</h5>
                        <p>
                            Juan Perez<br>
                            DNI 20200111
                        </p>
                    </div>
                    <div class="col-md-6">
                        <h5>Entrega</h5>
                        <p>
                            Envío a domicilio<br>
                            Buenos Aires<br>
                            Av. Alem 1200
                        </p>
                    </div>
                </div>

                <div class="row border-2 border-bottom border-black mt-4">
                    <div class="col-md-6">
                        <h5>Forma de pago</h5>
                        <p>Mercadopago</p>
                    </div>
                    <div class="col-md-6">
                        <h5>Resumen de la compra</h5>
                        <table class="table">
                            <tr>
                                <td>Subtotal</td>
                                <td class="text-end">$ 0.00</td>
                            </tr>
                            <tr>
                                <td>Descuento</td>
                                <td class="text-end">$ 0.00</td>
                            </tr>
                            <tr>
                                <td>Impuestos</td>
                                <td class="text-end">$ 0.00</td>
                            </tr>
                            <tr>
                                <th>TOTAL</th>
                                <th class="text-end">$ 0.00</th>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="row mt-4">
                    <div class="col-md-6">
                        <h5>Estado de la compra</h5>
                        <ul class="list-unstyled">
                            <li><i class="bi bi-check-circle-fill text-success"></i>Pago Aceptado</li>
                            <li><i class="bi bi-check-circle-fill text-success"></i>En Preparación</li>
                            <li><i class="bi bi-check-circle-fill text-success"></i>En Camino</li>
                            <li><i class="bi bi-check-circle-fill text-success"></i>Entregado</li>
                        </ul>
                    </div>
                </div>

            </div>
            <div class="col-4">
                <h5>Productos</h5>
                <!-- Inicio repeater -->
                <div class="d-flex p-2">
                    <div class="row py-2 border-bottom border-black">
                        <div class="col-3">
                            <img class="w-100 object-fit-contain rounded"
                                src="https://ik.imagekit.io/tpce16/products/iphone-15-pro-a-2f70988805588cc27816964316066050-1024-1024.png?updatedAt=1718288634185" />
                        </div>
                        <div class="col-5">
                            <p class="mb-1">Marca</p>
                            <p class="mb-1">Nombre</p>
                            <span class="text-small">1x $ P.Unit</span>
                        </div>
                        <div class="col-4 text-end">
                            <p class="pe-2">$ Total</p>
                        </div>
                    </div>
                </div>
                <!-- Fin repeater -->
                <div class="d-flex p-2">
                    <div class="row py-2 border-bottom border-black">
                        <div class="col-3">
                            <img class="w-100 object-fit-contain rounded"
                                src="https://ik.imagekit.io/tpce16/products/iphone-15-pro-a-2f70988805588cc27816964316066050-1024-1024.png?updatedAt=1718288634185" />
                        </div>
                        <div class="col-5">
                            <p class="mb-1">Marca</p>
                            <p class="mb-1">Nombre</p>
                            <span class="text-small">1x $ P.Unit</span>
                        </div>
                        <div class="col-4 text-end">
                            <p class="pe-2">$ Total</p>
                        </div>
                    </div>
                </div>
                <div class="d-flex p-2">
                    <div class="row py-2 border-bottom border-black">
                        <div class="col-3">
                            <img class="w-100 object-fit-contain rounded"
                                src="https://ik.imagekit.io/tpce16/products/iphone-15-pro-a-2f70988805588cc27816964316066050-1024-1024.png?updatedAt=1718288634185" />
                        </div>
                        <div class="col-5">
                            <p class="mb-1">Marca</p>
                            <p class="mb-1">Nombre</p>
                            <span class="text-small">1x $ P.Unit</span>
                        </div>
                        <div class="col-4 text-end">
                            <p class="pe-2">$ Total</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
