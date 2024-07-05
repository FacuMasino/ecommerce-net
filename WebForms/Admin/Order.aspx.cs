using System;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms.Admin
{
    public partial class OrderPage : System.Web.UI.Page
    {
        // ATTRIBUTES

        private Order _order;
        private OrdersManager _ordersManager;
        private OrderStatusesManager _orderStatusesManager;
        private ShoppingCart _shoppingCart;
        private ProductsManager _productsManager;

        // CONSTRUCT

        public OrderPage()
        {
            _order = new Order();
            _ordersManager = new OrdersManager();
            _orderStatusesManager = new OrderStatusesManager();
            _shoppingCart = new ShoppingCart();
            _productsManager = new ProductsManager();
        }

        // METHODS

        private void FetchProducts()
        {
            _shoppingCart.ProductSets = _productsManager.List<ProductSet>(false, false, _order.Id);
        }

        private void BindProductSetsRpt()
        {
            ProductSetsRpt.DataSource = _shoppingCart.ProductSets;
            ProductSetsRpt.DataBind();
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

        private void MapControls()
        {
            OrderIdLbl.Text = "Orden #" + _order.Id.ToString();
            OrderCreationDateLbl.Text = "Generada el " + _order.CreationDate.ToString("dd-MM-yyyy");
            TotalLbl.Text = "$" + _shoppingCart.Total.ToString();
            PaymentTypeLbl.Text = _order.PaymentType.Name;
            DistributionChannelLbl.Text = "Canal de distribución: " + _order.DistributionChannel.Name;

            if (_order.User.Username != null)
            {
                UsernameLbl.Text = "Nombre de usuario: " + _order.User.Username;
            }
            else
            {
                UsernameLbl.Text = "Usuario no registrado";
            }

            FirstNameLbl.Text = _order.User.FirstName;
            LastNameLbl.Text = _order.User.LastName;
            PhoneLbl.Text = "Tel.: " + _order.User.Phone;
            EmailLbl.Text = "Email: " + _order.User.Email;

            if (_order.DeliveryAddress.ToString() != "")
            {
                StreetNameLbl.Text = _order.DeliveryAddress.StreetName;
                StreetNumberLbl.Text = _order.DeliveryAddress.StreetNumber;
                FlatLbl.Text = "Departamento: " + _order.DeliveryAddress.Flat;
                CityLbl.Text = "Ciudad: " + _order.DeliveryAddress.City;
                ProvinceLbl.Text = "Provincia: " + _order.DeliveryAddress.Province;
                DetailsLbl.Text = "Detalles: " + _order.DeliveryAddress.Details;
            }
            else
            {
                StreetNameLbl.Text = "Pedido solicitado sin envío";
            }
        }

        //  EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchOrder();
                BindOrderStatusesDDL();
                FetchProducts();
                BindProductSetsRpt();
                MapControls();
            }
        }

        protected void OrderStatusesDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            // hack : agregar validacion que le avise al admin que cambiar el estado arbitrariamente es peligroso.

            FetchOrder();
            _order.OrderStatus.Id = Convert.ToInt32(OrderStatusesDDL.SelectedValue);
            _ordersManager.UpdateOrderStatus(_order.Id, _order.OrderStatus.Id);
        }

        protected void ProductSetsRpt_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Image imageLbl = e.Item.FindControl("ImageLbl") as System.Web.UI.WebControls.Image;

                ProductSet productSet = (ProductSet)e.Item.DataItem;

                if (0 < productSet.Images.Count)
                {
                    imageLbl.ImageUrl = productSet.Images[0].Url;
                }
            }
        }
    }
}