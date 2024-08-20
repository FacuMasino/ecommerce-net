using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace WebForms
{
    public partial class OrderConfirmation : System.Web.UI.Page
    {
        // ATTRIBUTES

        private Order _order;
        private ShoppingCart _shoppingCart;
        private OrdersManager _ordersManager;
        private ProvincesManager _provincesManager;
        private AddressesManager _addressesManager;
        private PaymentTypesManager _paymentTypesManager;
        private DistributionChannelsManager _distributionChannelsManager;
        private OrderStatusesManager _orderStatusesManager;
        private User _user;

        private List<InputWrapper> _inputValidations;

        // CONSTRUCT

        public OrderConfirmation()
        {
            _order = new Order();
            _shoppingCart = new ShoppingCart();
            _ordersManager = new OrdersManager();
            _provincesManager = new ProvincesManager();
            _addressesManager = new AddressesManager();
            _paymentTypesManager = new PaymentTypesManager();
            _distributionChannelsManager = new DistributionChannelsManager();
            _orderStatusesManager = new OrderStatusesManager();
            _inputValidations = new List<InputWrapper>();
            _user = new User();
        }

        // METHODS

        private void LoadInputValidations()
        {
            _inputValidations.Add(new InputWrapper(FirstNameTxt, typeof(string), 3, 30));
            _inputValidations.Add(new InputWrapper(LastNameTxt, typeof(string), 3, 30));
            _inputValidations.Add(new InputWrapper(EmailTxt, typeof(string), 6, 30));
            _inputValidations.Add(new InputWrapper(CityTxt, typeof(string), 3, 30));
            _inputValidations.Add(new InputWrapper(StreetNameTxt, typeof(string), 3, 30));
            _inputValidations.Add(new InputWrapper(StreetNumberTxt, typeof(string), 1, 30));
            _inputValidations.Add(new InputWrapper(ZipCodeTxt, typeof(string), 3, 30));
        }

        public bool IsValidInput(string controlId)
        {
            InputWrapper auxIW = _inputValidations.Find(ctl => ctl.Control.ID == controlId);
            if (auxIW != null && auxIW.IsValid)
                return true;
            return false;
        }

        private void FetchShoppingCart()
        {
            _shoppingCart = (ShoppingCart)Session["shoppingCart"];
        }

        private void BindProductSetsRpt()
        {
            ProductSetsRpt.DataSource = _shoppingCart.ProductSets;
            ProductSetsRpt.DataBind();
        }

        private void BindProvincesDDL()
        {
            ProvincesDDL.DataSource = _provincesManager.List(1);
            //ProvincesDDL.DataTextField = "Name";
            //ProvincesDDL.DataValueField = "Id";
            ProvincesDDL.DataBind();
            ProvincesDDL.SelectedIndex = 0;
        }

        private void SetAddress()
        {
            if (DeliveryRB.Checked)
            {
                _order.DeliveryAddress.Flat = string.IsNullOrEmpty(FlatTxt.Text)
                    ? "-"
                    : FlatTxt.Text;
                _order.DeliveryAddress.StreetName = StreetNameTxt.Text;
                _order.DeliveryAddress.StreetNumber = StreetNumberTxt.Text;
                _order.DeliveryAddress.City.Name = CityTxt.Text;
                _order.DeliveryAddress.City.ZipCode = ZipCodeTxt.Text;
                _order.DeliveryAddress.Province.Name = ProvincesDDL.Text;
                _order.DeliveryAddress.Country.Name = "Argentina";
                _order.DeliveryAddress.Details = "";
            }
            else
            {
                _order.DeliveryAddress = null;
            }
        }

        private void SetPerson()
        {
            if (_user.UserId != 0)
            {
                _order.User = _user;
                return;
            }

            _order.User.FirstName = FirstNameTxt.Text;
            _order.User.LastName = LastNameTxt.Text;
            _order.User.Email = EmailTxt.Text;
        }

        private void SetPaymentType()
        {
            if (CashRB.Checked)
            {
                _order.PaymentType.Name = "Efectivo";
            }
            else if (MercadoPagoRB.Checked)
            {
                _order.PaymentType.Name = "Mercado Pago";
            }
            else
            {
                _order.PaymentType.Name = "Transferencia bancaria";
            }
        }

        private void SetDistributionChannel()
        {
            if (CashRB.Checked)
            {
                if (DeliveryRB.Checked)
                {
                    _order.DistributionChannel = _distributionChannelsManager.Read(4);
                }
                else
                {
                    _order.DistributionChannel = _distributionChannelsManager.Read(3);
                }
            }
            else
            {
                if (DeliveryRB.Checked)
                {
                    _order.DistributionChannel = _distributionChannelsManager.Read(1);
                }
                else
                {
                    _order.DistributionChannel = _distributionChannelsManager.Read(2);
                }
            }
        }

        private void SetOrderStatus()
        {
            if (_order.DistributionChannel.Id == 1)
            {
                _order.OrderStatus = _orderStatusesManager.Read(1);
            }
            else if (_order.DistributionChannel.Id == 2)
            {
                _order.OrderStatus = _orderStatusesManager.Read(1);
            }
            else if (_order.DistributionChannel.Id == 3)
            {
                _order.OrderStatus = _orderStatusesManager.Read(6);
            }
            else // if (_order.DistributionChannel.Id == 4)
            {
                _order.OrderStatus = _orderStatusesManager.Read(10);
            }
        }

        private void SetOrder()
        {
            SetAddress();
            SetPerson();
            SetPaymentType();
            SetDistributionChannel();
            SetOrderStatus();
        }

        private void MapUserData()
        {
            FirstNameTxt.Text = _user.FirstName;
            LastNameTxt.Text = _user.LastName;
            EmailTxt.Text = _user.Email;

            StreetNameTxt.Text = _user.Address.StreetName;
            StreetNumberTxt.Text = _user.Address.StreetNumber;
            CityTxt.Text = _user.Address.City.Name;
            ZipCodeTxt.Text = _user.Address.City.ZipCode;
            ProvincesDDL.Text = _user.Address.Province.Name;

            setOnlyReadData();
        }

        private void setOnlyReadData()
        {
            FirstNameTxt.Enabled = false;
            LastNameTxt.Enabled = false;
            EmailTxt.Enabled = false;
        }

        private void MapControls()
        {
            if (_user.UserId != 0)
            {
                MapUserData();
            }
            TotalLbl.Text = _shoppingCart.Total.ToString();
        }

        private void Notify(string message)
        {
            Site siteMP = (Site)this.Master;
            siteMP.ShowBsToast(message);
        }

        private void FetchUser()
        {
            if (Session["user"] != null)
            {
                _user = (User)Session["user"];
            }
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadInputValidations();
            FetchUser();

            if (!IsPostBack)
            {
                FetchShoppingCart();
                BindProductSetsRpt();
                BindProvincesDDL();
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

        protected void DeliveryRB_CheckedChanged(object sender, EventArgs e)
        {
            AddressPnl.Visible = !AddressPnl.Visible;
        }

        protected void PickupRB_CheckedChanged(object sender, EventArgs e)
        {
            AddressPnl.Visible = !AddressPnl.Visible;
        }

        protected void SubmitOrder_Click(object sender, EventArgs e)
        {
            if (!Validator.RunValidations(_inputValidations))
            {
                Notify("Por favor complete todos los campos");
                return;
            }

            try
            {
                FetchShoppingCart(); // hack : agregar update panel para no perder los datos al hacer click en SubmitOrder
                SetOrder();
                _order.Id = _ordersManager.Add(_order, _shoppingCart.ProductSets);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Session.Remove("shoppingCart"); // Limpiar carrito
            Session.Remove("CurrentProductSets");

            if (_user.UserId == 0)
            {
                Response.Redirect($"OrderStatus.aspx?orderId={_order.Id}&success=true"); // Si no inició sesión, redirigir al detalle de orden
            }
            else
            {
                Response.Redirect("Orders.aspx"); // Si inició sesión, redirigir a las ordenes del usuario
            }
        }
    }
}
