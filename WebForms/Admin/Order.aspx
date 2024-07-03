<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true"
    CodeBehind="Order.aspx.cs" Inherits="WebForms.Admin.OrderPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="row container-1200 mx-auto">
        <div class="d-flex justify-content-between align-items-center">
            <h1 class="fs-4 mb-3">Pedido #0001</h1>
            <button class="btn btn-dark">Cancelar</button>
        </div>
        <!--- Detalles del pedido --->
        <section class="col-md-7 col d-flex flex-column gap-3">
            <div class="d-flex justify-content-between">
                <h2 class="fs-4 m-0">Detalles del pedido</h2>
                <span class="text-small">3 de Julio de 2024</span>
            </div>
            <!-- Info productos y estado -->
            <div class="bg-white py-2 border-1 border rounded">
                <div class="d-flex justify-content-between align-items-center border-bottom px-3 py-2">
                    <h3 class="fs-5 m-0 fw-normal">Productos</h3>
                    <span class="text-small bg-body-secondary rounded border py-0 px-2">Pago en proceso</span>
                </div>
                <!-- Lista de productos -->
                <div class="d-flex flex-column gap-2 px-3 py-2 border-bottom">
                    <!-- Repeater para cada row -->
                    <div class="row">
                        <div class="col-2">
                            <img class="w-100 object-fit-contain rounded"
                                src="https://ik.imagekit.io/tpce16/products/iphone-15-pro-a-2f70988805588cc27816964316066050-1024-1024.png?updatedAt=1718288634185" />
                        </div>
                        <div class="col-7">
                            <p>Marca - Modelo</p>
                            <span class="text-small">1x $ P.Unit</span>
                        </div>
                        <div class="col-3 text-end">
                            <p class="pe-2">$ Total</p>
                        </div>
                    </div>
                    <!-- Fin Repeater -->
                    <div class="row">
                        <div class="col-2">
                            <img class="w-100 object-fit-contain rounded"
                                src="https://ik.imagekit.io/tpce16/products/iphone-15-pro-a-2f70988805588cc27816964316066050-1024-1024.png?updatedAt=1718288634185" />
                        </div>
                        <div class="col-7">
                            <p>Marca - Modelo</p>
                            <span class="text-small">1x $ P.Unit</span>
                        </div>
                        <div class="col-3 text-end">
                            <p class="pe-2">$ Total</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-2">
                            <img class="w-100 object-fit-contain rounded"
                                src="https://ik.imagekit.io/tpce16/products/iphone-15-pro-a-2f70988805588cc27816964316066050-1024-1024.png?updatedAt=1718288634185" />
                        </div>
                        <div class="col-7">
                            <p>Marca - Modelo</p>
                            <span class="text-small">1x $ P.Unit</span>
                        </div>
                        <div class="col-3 text-end">
                            <p class="pe-2">$ Total</p>
                        </div>
                    </div>
                </div>
                <!-- Fin lista productos -->
                <!-- Administrar estado -->
                <div class="px-3 py-2">
                    <p class="fs-5 mb-3 fw-normal">Administrar estado</p>
                    <button class="btn btn-dark">Boton al siguiente estado</button>
                </div>
            </div>
            <!-- Fin info productos y estado -->

            <!-- Info Pago-->
            <div class="bg-white py-2 border-1 border rounded">
                <div class="d-flex align-items-center border-bottom px-3 py-2">
                    <h3 class="fs-5 m-0 fw-normal">Pago</h3>
                </div>
                <div class="d-flex flex-column gap-2">
                    <div class="bg-body-tertiary">
                        <div class="px-3 py-2 d-flex align-items-center justify-content-between ">
                            <span class="fw-bold">Total: </span>
                            <span>$ Precio Total</span>
                        </div>
                    </div>
                    <div>
                        <div class="px-3 py-2 d-flex align-items-center justify-content-between ">
                            <span class="fw-bold">Medio de pago: </span>
                            <span>A Convenir</span>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Fin info Pago-->

        </section>
        <section class="col-md-5 col d-flex flex-column gap-3">
            <h2 class="fs-4 m-0">Información</h2>
            <!-- Cliente info -->
            <div class="bg-white py-2 border-1 border rounded">
                <div class="d-flex align-items-center border-bottom px-3 py-2">
                    <h3 class="fs-5 m-0 fw-normal">Cliente</h3>
                </div>
                <div class="px-3 py-2">
                    <div class="d-flex flex-column gap-2">
                        <span class="fw-bold">Nombre - Apellido</span>
                        <span><a href="mailto:Email@email.com" target="_blank">Email@email.com</a></span>
                        <span>Tel. de contacto: No posee</span>
                    </div>
                </div>
            </div>
            <!-- Dir envio -->
            <div class="bg-white py-2 border-1 border rounded">
                <div class="d-flex align-items-center border-bottom px-3 py-2">
                    <h3 class="fs-5 m-0 fw-normal">Dirección de Envío</h3>
                </div>
                <div class="px-3 py-2">
                    <div class="d-flex flex-column gap-2">
                        <span>Siempreviva 742</span>
                        <span class="text-muted">CP: Numero</span>
                        <span class="text-muted">Springfield, EEUU</span>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
