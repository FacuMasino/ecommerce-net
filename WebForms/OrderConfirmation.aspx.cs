using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;
using WebForms.Admin;

namespace WebForms
{
    public partial class OrderConfirmation : System.Web.UI.Page
    {
        // ATTRIBUTES

        private User _user;
        private Order _order;
        private ShoppingCart _shoppingCart;
        private OrdersManager _ordersManager;
        private CountriesManager _countriesManager;
        private ProvincesManager _provincesManager;
        private CitiesManager _citiesManager;
        private AddressesManager _addressesManager;
        private PaymentTypesManager _paymentTypesManager;
        private DistributionChannelsManager _distributionChannelsManager;
        private OrderStatusesManager _orderStatusesManager;
        private List<InputWrapper> _inputValidations;

        // CONSTRUCT

        public OrderConfirmation()
        {
            _user = new User();
            _order = new Order();
            _shoppingCart = new ShoppingCart();
            _ordersManager = new OrdersManager();
            _countriesManager = new CountriesManager();
            _provincesManager = new ProvincesManager();
            _citiesManager = new CitiesManager();
            _addressesManager = new AddressesManager();
            _paymentTypesManager = new PaymentTypesManager();
            _distributionChannelsManager = new DistributionChannelsManager();
            _orderStatusesManager = new OrderStatusesManager();
            _inputValidations = new List<InputWrapper>();
        }

        // METHODS

        private void LoadInputValidations()
        {
            _inputValidations.Add(new InputWrapper(FirstNameTxt, typeof(string), 3, 30));
            _inputValidations.Add(new InputWrapper(LastNameTxt, typeof(string), 3, 30));
            _inputValidations.Add(new InputWrapper(EmailTxt, typeof(string), 6, 30));

            if (AddressPnl.Visible)
            {
                _inputValidations.Add(new InputWrapper(CityTxt, typeof(string), 3, 30));
                _inputValidations.Add(new InputWrapper(StreetNameTxt, typeof(string), 3, 30));
                _inputValidations.Add(new InputWrapper(StreetNumberTxt, typeof(string), 1, 30));
            }
        }

        public bool IsValidInput(string controlId)
        {
            InputWrapper auxIW = _inputValidations.Find(ctl => ctl.Control.ID == controlId);
            
            if (auxIW != null && auxIW.IsValid)
            {
                return true;
            }

            return false;
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
            ProvincesDDL.DataTextField = "Name";
            ProvincesDDL.DataValueField = "Id";
            ProvincesDDL.DataBind();
            ProvincesDDL.SelectedIndex = 0;
        }

        private void TogglePersonalFields(bool enabled)
        {
            FirstNameTxt.Enabled = enabled;
            LastNameTxt.Enabled = enabled;
            EmailTxt.Enabled = enabled;
        }

        private void ToggleAddressFields(bool enabled)
        {
            ProvincesDDL.Enabled = enabled;
            CityTxt.Enabled = enabled;
            ZipCodeTxt.Enabled = enabled;
            StreetNameTxt.Enabled = enabled;
            StreetNumberTxt.Enabled = enabled;
            FlatTxt.Enabled = enabled;
            DetailsTxt.Enabled = enabled;
        }

        private void MapAddress()
        {
            if (DeliveryRB.Checked)
            {
                _order.DeliveryAddress.Country.Id = (int)CountriesManager.Ids.ArgentinaId;
                _order.DeliveryAddress.Country = _countriesManager.Read(_order.DeliveryAddress.Country.Id);
                _order.DeliveryAddress.Province.Id = Convert.ToInt32(ProvincesDDL.SelectedValue);
                _order.DeliveryAddress.Province = _provincesManager.Read(_order.DeliveryAddress.Province.Id);
                _order.DeliveryAddress.City.Name = CityTxt.Text;
                _order.DeliveryAddress.City.ZipCode = !string.IsNullOrEmpty(ZipCodeTxt.Text) ? ZipCodeTxt.Text : null;
                _order.DeliveryAddress.City.Id = _citiesManager.GetId(_order.DeliveryAddress.City);
                _order.DeliveryAddress.StreetName = StreetNameTxt.Text;
                _order.DeliveryAddress.StreetNumber = StreetNumberTxt.Text;
                _order.DeliveryAddress.Flat = !string.IsNullOrEmpty(FlatTxt.Text) ? FlatTxt.Text : null;
                _order.DeliveryAddress.Details = !string.IsNullOrEmpty(DetailsTxt.Text) ? DetailsTxt.Text : null;
            }
            else
            {
                _order.DeliveryAddress = null;
            }
        }

        private void MapPerson()
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

        private void MapPaymentType()
        {
            if (CashRB.Checked)
            {
                _order.PaymentType.Id = (int)PaymentTypesManager.Ids.CashId;
            }
            else if (MercadoPagoRB.Checked)
            {
                _order.PaymentType.Id = (int)PaymentTypesManager.Ids.MercadoPagoId;
            }
            else // if (BankTransferRB.Checked)
            {
                _order.PaymentType.Id = (int)PaymentTypesManager.Ids.BankTransferId;
            }

            _order.PaymentType = _paymentTypesManager.Read(_order.PaymentType.Id);
        }

        private void MapDistributionChannel()
        {
            if (CashRB.Checked)
            {
                if (DeliveryRB.Checked)
                {
                    _order.DistributionChannel.Id = (int)DistributionChannelsManager.Ids.CashDeliveryId;
                }
                else
                {
                    _order.DistributionChannel.Id = (int)DistributionChannelsManager.Ids.CashNoDeliveryId;
                }
            }
            else
            {
                if (DeliveryRB.Checked)
                {
                    _order.DistributionChannel.Id = (int)DistributionChannelsManager.Ids.NoCashDeliveryId;
                }
                else
                {
                    _order.DistributionChannel.Id = (int)DistributionChannelsManager.Ids.NoCashNoDeliveryId;
                }
            }

            _order.DistributionChannel = _distributionChannelsManager.Read(_order.DistributionChannel.Id);
        }

        private void MapOrderStatus()
        {
            if (_order.DistributionChannel.Id == 1)
            {
                _order.OrderStatus.Id = (int)OrderStatusesManager.Ids.ProcessingPaymentId;
            }
            else if (_order.DistributionChannel.Id == 2)
            {
                _order.OrderStatus.Id = (int)OrderStatusesManager.Ids.ProcessingPaymentId;
            }
            else if (_order.DistributionChannel.Id == 3)
            {
                _order.OrderStatus.Id = (int)OrderStatusesManager.Ids.PaymentAndWithdrawalPendingId;
            }
            else // if (_order.DistributionChannel.Id == 4)
            {
                _order.OrderStatus.Id = (int)OrderStatusesManager.Ids.DeliveryAndPaymentPendingId;
            }

            _order.OrderStatus = _orderStatusesManager.Read(_order.OrderStatus.Id);
        }

        private void MapOrder()
        {
            MapAddress();
            MapPerson();
            MapPaymentType();
            MapDistributionChannel();
            MapOrderStatus();
        }

        private void MapControls()
        {
            if (0 < _user.UserId)
            {
                FirstNameTxt.Text = _user.FirstName;
                LastNameTxt.Text = _user.LastName;
                EmailTxt.Text = _user.Email;
                ProvincesDDL.SelectedValue = _user.Address.Province.Id.ToString();
                CityTxt.Text = _user.Address.City.Name;
                ZipCodeTxt.Text = _user.Address.City.ZipCode;
                StreetNameTxt.Text = _user.Address.StreetName;
                StreetNumberTxt.Text = _user.Address.StreetNumber;
                FlatTxt.Text = _user.Address.Flat;
                DetailsTxt.Text = _user.Address.Details;
                TogglePersonalFields(false);
            }

            TotalLbl.Text = _shoppingCart.Total.ToString();
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

        protected void ProductSetsRpt_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            bool isStandardItem = e.Item.ItemType == ListItemType.Item;
            bool isAlternatingItem = e.Item.ItemType == ListItemType.AlternatingItem;

            if (isStandardItem || isAlternatingItem)
            {
                System.Web.UI.WebControls.Image imageLbl = e.Item.FindControl("ImageLbl") as System.Web.UI.WebControls.Image;

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
            ToggleAddressFields(AddressPnl.Visible);
        }

        protected void PickupRB_CheckedChanged(object sender, EventArgs e)
        {
            AddressPnl.Visible = !AddressPnl.Visible;
            ToggleAddressFields(AddressPnl.Visible);
        }

        protected void SubmitOrder_Click(object sender, EventArgs e)
        {
            if (!Validator.RunValidations(_inputValidations))
            {
                Notify("Por favor complete todos los campos");
                return;
            }

            FetchShoppingCart();
            MapOrder();

            try
            {
                _order.Id = _ordersManager.Add(_order, _shoppingCart.ProductSets);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Session.Remove("shoppingCart");
            Session.Remove("CurrentProductSets");

            if (_user.UserId == 0)
            {
                Response.Redirect($"OrderStatus.aspx?orderId={_order.Id}&success=true");
            }
            else
            {
                Response.Redirect("Orders.aspx");
            }
        }
    }
}
