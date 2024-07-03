using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms.Admin
{
    public partial class Orders : System.Web.UI.Page
    {
        // ATTRIBUTES

        private Order _order;
        private List<Order> _orders;
        private OrdersManager _ordersManager;
        private OrderStatusesManager _orderStatusesManager = new OrderStatusesManager();

        // CONSTRUCT

        public Orders()
        {
            _order = new Order();
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

        protected void OrdersListRpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _order = (Order)e.Item.DataItem;
                int distributionChannelId = _ordersManager.GetDistributionChannelId(_order.Id);
                DropDownList ddl = (DropDownList)e.Item.FindControl("OrderStatusesDDL");
                ddl.DataSource = _orderStatusesManager.List(distributionChannelId);
                ddl.DataBind();
                ddl.SelectedValue = _order.OrderStatus.Name;
            }
        }

        protected void OrderStatusesDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            // hack : logica para editar el estado de una orden
        }
    }
}