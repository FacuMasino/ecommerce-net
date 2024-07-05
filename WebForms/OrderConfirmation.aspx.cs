using System;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms
{
    public partial class OrderConfirmation : System.Web.UI.Page
    {
        // ATTRIBUTES

        private Order _order;
        private ShoppingCart _shoppingCart;
        private OrdersManager _ordersManager;
        private ProvincesManager _provincesManager;
        private CitiesManager _citiesManager;
        private PaymentTypesManager _paymentTypesManager;

        // PROPERTIES

        // CONSTRUCT

        public OrderConfirmation()
        { 
            _order = new Order();
            _shoppingCart = new ShoppingCart();
            _ordersManager = new OrdersManager();
            _provincesManager = new ProvincesManager();
            _citiesManager = new CitiesManager();
            _paymentTypesManager = new PaymentTypesManager();
        }

        // METHODS

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

        private void SetPerson()
        {
            _order.User.FirstName = FirstNameTxt.Text;
            _order.User.LastName = LastNameTxt.Text;
            _order.User.Email = EmailTxt.Text;
        }

        private void SetAddress()
        {
            _order.DeliveryAddress.Flat = FlatTxt.Text;
            _order.DeliveryAddress.StreetName = StreetNameTxt.Text;
            _order.DeliveryAddress.StreetNumber = StreetNumberTxt.Text;
            _order.DeliveryAddress.City.Name = CityTxt.Text;
            _order.DeliveryAddress.City.ZipCode = ZipCodeTxt.Text;
            _order.DeliveryAddress.City.Id = _citiesManager.GetId(_order.DeliveryAddress.City);
            _order.DeliveryAddress.Province.Name = (string)ProvincesDDL.SelectedValue;
            _order.DeliveryAddress.Province.Id = _provincesManager.GetId(_order.DeliveryAddress.Province);
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

            _order.PaymentType.Id = _paymentTypesManager.GetId(_order.PaymentType);
        }

        private void SetOrder()
        {
            SetPerson();

            if (DeliveryRB.Checked)
            {
                SetAddress();
            }

            SetPaymentType();
        }

        private void MapControls()
        {
            TotalLbl.Text = _shoppingCart.Total.ToString();
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
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

        protected void SubmitOrder_Click(object sender, EventArgs e)
        {
            SetOrder();
            _order.Id = _ordersManager.Add(_order);
        }

        protected void DeliveryRB_CheckedChanged(object sender, EventArgs e)
        {
            AddressPnl.Visible = !AddressPnl.Visible;
        }

        protected void PickupRB_CheckedChanged(object sender, EventArgs e)
        {
            AddressPnl.Visible = !AddressPnl.Visible;
        }
    }
}