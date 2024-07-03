using BusinessLogicLayer;
using DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UtilitiesLayer;

namespace WebForms.Admin
{
    public partial class Orders : System.Web.UI.Page
    {
        // ATTRIBUTES

        private Order _order;
        private List<Order> _orders;
        private OrdersManager _ordersManager;

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

        /// <summary>
        /// Evento que se dispara cuando se hace clic en cualquier control dentro del Repeater que tenga el atributo CommandName definido.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void OrdersListRpt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                
            }
        }
    }
}