using System;
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

        // PROPERTIES

        // CONSTRUCT

        public OrderConfirmation()
        { 
            _order = new Order();
            _shoppingCart = new ShoppingCart();
            _ordersManager = new OrdersManager();
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

        private void MapControls()
        {
            //hack
        }

        private void SetOrder()
        {
            //hack
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchShoppingCart();
                BindProductSetsRpt();
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
            _ordersManager.Add(_order);
        }
    }
}