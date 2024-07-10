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
    }
}