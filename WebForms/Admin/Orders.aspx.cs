using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms.Admin
{
    public partial class Orders : System.Web.UI.Page
    {
        // ATTRIBUTES

        private List<Order> _orders;
        private OrdersManager _ordersManager;

        // CONSTRUCT

        public Orders()
        {
            _ordersManager = new OrdersManager();
            FetchOrders();
        }

        // METHODS

        private void FetchOrders()
        {
            _orders = _ordersManager.List();
        }

        private void BindOrdersRpt()
        {
            OrdersListRpt.DataSource = _orders;
            OrdersListRpt.DataBind();
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchOrders();
                BindOrdersRpt();
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            // hack
        }

        protected void OrdersListRpt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("Order.aspx?orderId=" + orderId, false);
            }
        }
    }
}