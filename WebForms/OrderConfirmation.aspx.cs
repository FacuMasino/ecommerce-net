using System;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace WebForms
{
    public partial class OrderConfirmation : System.Web.UI.Page
    {
        // ATTRIBUTES

        private Order _order;

        // PROPERTIES

        // CONSTRUCT

        public OrderConfirmation()
        { 
            _order = new Order();
        }

        // METHODS

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}