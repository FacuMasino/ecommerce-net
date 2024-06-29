<%@ Page Title="Realizar Pedido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="OrderConfirmation.aspx.cs" Inherits="WebForms.OrderConfirmation" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <h2 class="fs-4 mt-4 mb-3 ps-md-4 ps-2">Completá los datos y realizá tu pedido</h2>
        <div class="row g-0 my-5">
            <div class="col-md-7 col-12 px-md-4 px-2">
                <h2 class="fs-5 mb-3">Información de contacto</h2>
                <div class="mb-3">
                    <input type="email" class="form-control" id="UsrEmail" placeholder="tucorreo@correo.com">
                </div>
                <div class="mb-3">
                    <input type="number" class="form-control" id="UsrPhone" placeholder="Teléfono">
                </div>
                <div class="row mb-3">
                    <div class="col">
                        <input type="text" class="form-control" id="UsrName" placeholder="Nombres">
                    </div>
                    <div class="col">
                        <input type="text" class="form-control" id="UsrSurname" placeholder="Apellidos">
                    </div>
                </div>
                <div class="mb-3">
                    <input type="number" class="form-control" id="UsrDocument" placeholder="DNI">
                </div>
                <div class="mb-3">
                    <select class="form-select" aria-label="Selección por defecto">
                        <option selected>Provincia</option>
                        <option value="1">One</option>
                        <option value="2">Two</option>
                        <option value="3">Three</option>
                    </select>
                </div>
                <div class="row mb-3">
                    <div class="col">
                        <input type="text" class="form-control" id="UsrAddr" placeholder="Calle">
                    </div>
                    <div class="col">
                        <input type="number" class="form-control" id="UsrAddrNum" placeholder="123">
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col">
                        <input type="text" class="form-control" id="UsrFloor" placeholder="Piso">
                    </div>
                    <div class="col">
                        <input type="text" class="form-control" id="UsrFlat" placeholder="Depto/Oficina">
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col">
                        <input type="text" class="form-control" id="UsrPC" placeholder="Código Postal">
                    </div>
                    <div class="col">
                        <input type="text" class="form-control" id="UsrCity" placeholder="Ciudad">
                    </div>
                </div>
                <div class="mb-3">
                    <h2 class="fs-5 mb-3">Forma de Entrega</h2>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="ShipmentTypeOpt" id="ShipmentTypeOpt1">
                        <label class="form-check-label" for="ShipmentTypeOpt1">
                            Envío a domicilio
                        </label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="ShipmentTypeOpt" id="ShipmentTypeOpt2"
                            checked>
                        <label class="form-check-label" for="ShipmentTypeOpt2">
                            Retiro en sucursal
                        </label>
                    </div>
                </div>
                <div class="mb-3">
                    <h2 class="fs-5 mb-3">Forma de Pago</h2>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="PaymentTypeOpt" id="PaymentTypeOpt1">
                        <label class="form-check-label" for="PaymentTypeOpt1">
                            MercadoPago    
                        </label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="PaymentTypeOpt" id="PaymentTypeOpt2"
                            checked>
                        <label class="form-check-label" for="PaymentTypeOpt2">
                            Transferencia Bancaria 
                        </label>
                    </div>
                </div>
                <button class="btn btn-dark w-100" type="button">Realizar pedido</button>
            </div>
            <div class="col-md-5 col-12 order-md-last order-first border-md-start border-sm-bottom px-md-4 px-2 mb-md-0 mb-3">
                <h2 class="fs-5 mb-3">Tus productos</h2>
                <ul class="list-group list-group-lg list-group-flush mb-auto">

                    <li class="list-group-item border-bottom">
                        <div class="row align-items-center">
                            <div class="col-4">
                                <a href="Product.aspx?id=2">
                                    <img src="https://ik.imagekit.io/tpce16/products/motogplay7magen_1024x1024.png?updatedAt=1717373361655"
                                        class="img-fluid" onerror="this.src='/Content/img/placeholder.jpg'"
                                        alt="Imagen de Moto G Play 7ma Gen">
                                </a>
                            </div>
                            <div class="col-4">
                                <a class="text-body text-decoration-none" href="Product.aspx?id=2">Motorola - Moto G
                                    Play 7ma Gen
                                </a>
                            </div>
                            <div class="col-4">
                                <span class="text-muted ms-auto">$228,576.00</span>
                            </div>
                        </div>
                    </li>

                </ul>
                <div class="d-flex w-100 align-items-end flex-column mt-4">
                    <h2 class="fs-4">TOTAL</h2>
                    <span class="fs-5">$ 0000.00</span>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
