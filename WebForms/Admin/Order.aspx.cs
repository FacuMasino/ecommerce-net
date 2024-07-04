using BusinessLogicLayer;
using DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms.Admin
{
    public partial class OrderPage : System.Web.UI.Page
    {
        // ATTRIBUTES

        private Order _order;
        private OrdersManager _ordersManager;
        private OrderStatusesManager _orderStatusesManager;
        private List<Product> _products;
        private ProductsManager _productsManager;

        // CONSTRUCT

        public OrderPage()
        {
            _order = new Order();
            _ordersManager = new OrdersManager();
            _orderStatusesManager = new OrderStatusesManager();
            _products = new List<Product>();
            _productsManager = new ProductsManager();
        }

        // METHODS

        private void FetchProducts()
        {
            _products = _productsManager.List(false, false, _order.Id);
        }

        private void BindProductsRpt()
        {
            ProductsRpt.DataSource = _products;
            ProductsRpt.DataBind();
        }

        private void FetchOrder()
        {
            int orderId;
            string param = Request.QueryString["orderId"];

            if (!string.IsNullOrEmpty(param) && int.TryParse(param, out orderId))
            {
                _order = _ordersManager.Read(orderId);
            }
        }

        private void BindOrderStatusesDDL()
        {
            OrderStatusesDDL.DataSource = _orderStatusesManager.List(_order.DistributionChannel.Id);
            OrderStatusesDDL.DataTextField = "Name";
            OrderStatusesDDL.DataValueField = "Id";
            OrderStatusesDDL.DataBind();
            OrderStatusesDDL.SelectedValue = _order.OrderStatus.Id.ToString();
        }

        //  EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchOrder();
                BindOrderStatusesDDL();
                FetchProducts();
                BindProductsRpt();
            }
        }

        protected void OrderStatusesDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            // hack : agregar validacion que le avise al admin que cambiar el estado arbitrariamente es peligroso.

            FetchOrder();
            _order.OrderStatus.Id = Convert.ToInt32(OrderStatusesDDL.SelectedValue);
            _ordersManager.UpdateOrderStatus(_order.Id, _order.OrderStatus.Id);
        }
    }
}


/*
 
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

 */