using System;
using System.Collections.Generic;
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
        private bool _isSearching;
        private string _textToSearch;

        public int TotalOrders
        {
            get { return _orders == null ? 0 : _orders.Count; }
        }

        // CONSTRUCT

        public Orders()
        {
            _ordersManager = new OrdersManager();
            FetchOrders();
        }

        // METHODS

        private void GetSearchState()
        {
            _isSearching = ViewState["IsSearching"] as bool? ?? false;
            _textToSearch = ViewState["TextToSearch"] as string ?? "";
        }

        private void SetSearchState(bool isSearching, string textToSearch)
        {
            ViewState["IsSearching"] = isSearching;
            ViewState["TextToSearch"] = textToSearch;
        }

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
            string filter = SearchTextBox.Text;
            GetSearchState(); // Obtiene el estado de busqueda

            // Limpiar búsqueda si ya está buscando y el texto es el mismo
            if (_isSearching && _textToSearch == filter)
            {
                // Resetear estado
                SetSearchState(false, ""); // Limpia el estado de busqueda

                // Resetear controles
                SearchBtn.Text = "<i class=\"bi bi-search\"></i>";
                SearchTextBox.Text = "";
                SearchPanel.CssClass = "input-group mb-3";

                FetchOrders();
                BindOrdersRpt();
                return;
            }

            if (2 <= filter.Length)
            {
                string filterUpper = filter.ToUpper();

                SearchPanel.CssClass = "input-group mb-3";
                _orders = _orders.FindAll(x =>
                    (x.User?.FirstName?.ToUpper().Contains(filterUpper) ?? false)
                    || (x.User?.LastName?.ToUpper().Contains(filterUpper) ?? false)
                    || (x.User?.Username?.ToUpper().Contains(filterUpper) ?? false)
                );
                SearchBtn.Text = "<i class=\"bi bi-x-circle\"></i>"; // cambia icono boton de busqueda
            }
            else
            {
                SearchPanel.CssClass = "input-group mb-3 invalid";
            }

            SetSearchState(true, filter); // Guarda el estado para saber que está buscando
            BindOrdersRpt();
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
