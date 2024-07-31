using System;
using System.Collections.Generic;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms
{
    public partial class Orders : System.Web.UI.Page
    {
        // ATTRIBUTES

        private User _user;
        private List<Order> _orders;
        private OrdersManager _ordersManager;

        // CONSTRUCT

        public Orders()
        {
            _user = new User();
            _ordersManager = new OrdersManager();
        }

        // METHODS

        private void FetchUser()
        {
            _user = (User)Session["user"];
        }

        private void FetchOrders()
        {
            _orders = _ordersManager.List(_user.PersonId);
        }

        private void BindOrdersRpt()
        {
            OrdersRpt.DataSource = _orders;
            OrdersRpt.DataBind();
        }

        private void MapControls()
        {
            if (_user != null)
            {
                GreetingLbl.Text = "¡Hola " + _user.FirstName + "!";
            }
            else
            {
                GreetingLbl.Text = "¡Hola!";
            }
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchUser();
                FetchOrders();
                BindOrdersRpt();
                MapControls();
            }
        }

        protected void OrdersRpt_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("OrderStatus.aspx?orderId=" + orderId, false); // hack : renombrar OrderStatus.aspx por, tal vez, OrderDetails.aspx
            }
        }
    }
}