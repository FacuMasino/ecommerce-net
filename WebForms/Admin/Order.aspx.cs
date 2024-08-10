using System;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;

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
        private EmailManager _emailManager;
        private User _sessionUser;
        private UsersManager _usersManager;

        // CONSTRUCT

        public OrderPage()
        {
            _order = new Order();
            _ordersManager = new OrdersManager();
            _orderStatusesManager = new OrderStatusesManager();
            _shoppingCart = new ShoppingCart();
            _productsManager = new ProductsManager();
            _emailManager = new EmailManager();
            _sessionUser = new User();
            _usersManager = new UsersManager();
        }

        // METHODS

        private void FetchUser()
        {
            _sessionUser = (User)Session["user"];
        }

        private void BindAcceptedStatusesRpt()
        {
            AcceptedStatusesRpt.DataSource = _orderStatusesManager.List(_order.DistributionChannel.Id, _order.OrderStatus.Id);
            AcceptedStatusesRpt.DataBind();
        }

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

        private bool UserHasTransitionRole()
        {
            bool userHasTransitionRole = _usersManager.UserHasRole(_sessionUser, _order.OrderStatus.Role);

            if (_order.OrderStatus.Role.Id == (int)RolesManager.Roles.CustomerRoleId)
            {
                userHasTransitionRole = false;
            }

            return userHasTransitionRole;
        }

        private void MapOrderStatusIcoLbl()
        {
            if (_order.OrderStatus.Id == 5 || _order.OrderStatus.Id == 9) // hack : ids hardcodiados tienen que coincidir con DB
            {
                OrderStatusIco.CssClass = "bi bi-check-circle-fill text-success";
            }
            else
            {
                OrderStatusIco.CssClass = "bi bi-clock text-warning";
            }

            OrderStatusLbl.Text = _order.OrderStatus.Name;
        }

        private void MapTransitionBtn()
        {
            if (UserHasTransitionRole())
            {
                TransitionBtn.Visible = true;
                TransitionBtn.Text = _order.OrderStatus.TransitionText;
            }
            else
            {
                TransitionBtn.Visible = false;
            }
        }

        private void MapOrderData()
        {
            OrderIdLbl.Text = "Orden #" + _order.Id.ToString();
            OrderCreationDateLbl.Text = "Generada el " + _order.CreationDate.ToString("dd-MM-yyyy");
        }

        private void MapPaymentMethod()
        {
            TotalLbl.Text = "$" + _shoppingCart.Total.ToString("F2");
            PaymentTypeLbl.Text = _order.PaymentType.Name;
        }

        private void MapOrderStatus(bool isPostBack = false)
        {
            MapOrderStatusIcoLbl();
            MapTransitionBtn();
            TransitionRoleLbl.Text = "Responsabilidad de: " + _order.OrderStatus.Role.Name;

            if (!isPostBack)
            {
                OrderGeneratedLbl.Text = "Orden generada";
                OrderStatusesDDL.Visible = _usersManager.UserHasRole(_sessionUser, (int)RolesManager.Roles.PlusRoleId);
                DistributionChannelLbl.Text = _order.DistributionChannel.Name;
            }
        }

        private void MapUserData()
        {
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
        }

        private void MapAddress()
        {
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

        private void MapControls()
        {
            MapOrderData();
            MapPaymentMethod();
            MapOrderStatus();
            MapUserData();
            MapAddress();
        }

        public void SendShippingEmail()
        {
            if (_order.OrderStatus.Id == 3)
            {
                EmailMessage<OrderShippingEmail> shippingEmail = Helper.ComposeShippingEmail(
                    _order.User,
                    Helper.EcommerceName,
                    _order.Id.ToString(),
                    $"https://localhost:44324/Admin/order.aspx?id={_order.Id}",
                    "NoImplementado"
                );

                _emailManager.SendEmail(shippingEmail);
            }
        }

        //  EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchUser();
                FetchOrder();
                FetchProducts();
                BindOrderStatusesDDL();
                BindProductSetsRpt();
                BindAcceptedStatusesRpt();
                MapControls();
            }
        }

        protected void OrderStatusesDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FetchUser();

            if (_usersManager.UserHasRole(_sessionUser, (int)RolesManager.Roles.PlusRoleId))
            {
                FetchOrder();
                _order.OrderStatus.Id = Convert.ToInt32(OrderStatusesDDL.SelectedValue);
                _ordersManager.UpdateOrderStatus(_order.Id, _order.OrderStatus.Id);
                SendShippingEmail();
                FetchOrder();
                BindAcceptedStatusesRpt();
                MapOrderStatus(true);
            }
        }

        protected void ProductSetsRpt_ItemDataBound(
            object sender,
            System.Web.UI.WebControls.RepeaterItemEventArgs e
        )
        {
            if (
                e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.AlternatingItem
            )
            {
                System.Web.UI.WebControls.Image imageLbl =
                    e.Item.FindControl("ImageLbl") as System.Web.UI.WebControls.Image;

                ProductSet productSet = (ProductSet)e.Item.DataItem;

                if (0 < productSet.Images.Count)
                {
                    imageLbl.ImageUrl = productSet.Images[0].Url;
                }
            }
        }

        protected void TransitionButton_Click(object sender, EventArgs e)
        {
            FetchUser();
            FetchOrder();

            if (UserHasTransitionRole())
            {
                int nextStatusId = _orderStatusesManager.GetNextStatusId(_order.DistributionChannel.Id, _order.OrderStatus.Id);
                _ordersManager.UpdateOrderStatus(_order.Id, nextStatusId);
                SendShippingEmail();
                FetchOrder();
                BindOrderStatusesDDL();
                BindAcceptedStatusesRpt();
                MapOrderStatus(true);
            }
        }
    }
}
