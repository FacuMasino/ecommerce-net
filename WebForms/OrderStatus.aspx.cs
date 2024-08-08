using System;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace WebForms
{
    public partial class OrderStatus : System.Web.UI.Page
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

        public OrderStatus()
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
            if (Session["user"] != null)
                _sessionUser = (User)Session["user"];
        }

        private void BindAcceptedStatusesRpt()
        {
            AcceptedStatusesRpt.DataSource = _orderStatusesManager.List(
                _order.DistributionChannel.Id,
                _order.OrderStatus.Id
            );
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
            string success = Request.QueryString["success"];

            if (!string.IsNullOrEmpty(param) && int.TryParse(param, out orderId))
            {
                _order = _ordersManager.Read(orderId);
            }

            if (!string.IsNullOrEmpty(success) && success.ToUpper() == "TRUE")
            {
                Notify("Su pedido fue realizado con éxito!");
            }
        }

        private void Notify(string message)
        {
            Site siteMP = (Site)this.Master;
            siteMP.ShowBsToast(message);
        }

        private void MapTransitionBtn()
        {
            bool userHasRole = _usersManager.UserHasRole(_sessionUser, _order.OrderStatus.Role);
            bool ifCustomerHasPermission = true;

            if (_usersManager.IsCustomer(_sessionUser) && _order.User.PersonId != _sessionUser.PersonId)
            {
                ifCustomerHasPermission = false;
            }

            if (userHasRole && ifCustomerHasPermission)
            {
                TransitionBtn.Visible = true;
                TransitionBtn.Text = _order.OrderStatus.TransitionText;
            }
            else
            {
                TransitionBtn.Visible = false;
            }
        }

        private void MapControls()
        {
            OrderGeneratedLbl.Text = "Orden generada";
            OrderStatusLbl.Text = _order.OrderStatus.Name;
            OrderIdLbl.Text = "Orden #" + _order.Id.ToString();
            OrderCreationDateLbl.Text = "Generada el " + _order.CreationDate.ToString("dd-MM-yyyy");
            TotalLbl.Text = "$" + _shoppingCart.Total.ToString("F2");
            PaymentTypeLbl.Text = _order.PaymentType.Name;
            MapTransitionBtn();

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

            if (_order.OrderStatus.Id == 5 || _order.OrderStatus.Id == 9) // hack : ids hardcodiados tienen que coincidir con DB
            {
                OrderStatusIco.CssClass = "bi bi-check-circle-fill text-success";
            }
            else
            {
                OrderStatusIco.CssClass = "bi bi-clock text-warning";
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
                BindProductSetsRpt();
                BindAcceptedStatusesRpt();
                MapControls();
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
            // hack : agregar confirmación

            FetchOrder();
            int nextStatusId = _orderStatusesManager.GetNextStatusId(
                _order.DistributionChannel.Id,
                _order.OrderStatus.Id
            );
            _ordersManager.UpdateOrderStatus(_order.Id, nextStatusId);
        }
    }
}
