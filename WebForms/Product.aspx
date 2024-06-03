<%@ Page Title="Detalles del producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="WebForms.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
     <link href="CSS/Style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
     <%
     if (0 < _product.Id)
     {
 %>
 <div class="d-flex mt-auto flex-column align-items-center justify-content-center">
     <h4 class="text-align-center">Detalles del artículo</h4>
     <div class="col-7">
         <div class="card mb-3">
             <div class="row g-0">
                 <div class="col-md-4">
                     <div id="carouselExampleIndicators" class="carousel slide h-100">
                         <div class="carousel-inner h-100">
                             <%
                                 string category = _product.Category.ToString() == "" ? "Sin Categoría" : _product.Category.ToString();
                                 foreach (DomainModelLayer.Image image in _product.Images)
                                 {
                             %>
                             <div class="carousel-item h-100 active">
                                 <img src="<%: image.Url%>" class="object-fit-cover rounded-start d-block" alt="Imagen de <%:_product.Name%>" width="300px" height="100%"  onerror="this.src='Content/img/placeholder.jpg'" />
                             </div>
                             <%
                                 }
                             %>
                         </div>
                         <% if (_product.Images.Count > 1)
                             {
                         %>
                         <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
                             <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                             <span class="visually-hidden">Previous</span>
                         </button>
                         <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
                             <span class="carousel-control-next-icon" aria-hidden="true"></span>
                             <span class="visually-hidden">Next</span>
                         </button>
                         <%} %>
                     </div>
                 </div>
                 <div class="col-md-8">
                     <div class="card-body d-flex flex-column justify-content-between h-100">
                         <div >
                             <p class="mb-0"><small class="text-body-secondary"><%:category%></small></p>
                             <h5 class="card-title mb-0"><%:_product.Name%></h5>
                             <p class="card-text"><small class="text-body-secondary"><%:_product.Brand.ToString()%></small></p>
                             <p class="card-text"><%:_product.Description.ToString()%></p>
                         </div>

                           <p class="card-text  fw-bold fs-5 align-self-end mt-auto">$<%:_product.Price.ToString("0.00")%></p>
                         <a href ="Cart.aspx?Id=<%=_product.Id%>" class="btn btn-dark">Agregar al carrito </a> 
                           
                           
                           
                         
                     </div>
                 </div>
             </div>
         </div>
     </div>
 </div>

 <%
     }
 %>
</asp:Content>
