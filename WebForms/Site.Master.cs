using DomainModelLayer;
using System;
using System.Collections.Generic;

namespace WebForms
{
    public partial class Site : System.Web.UI.MasterPage
    {
        // ATTRIBUTES

        public List<ProductSet> _productSets;

        // CONSTRUCT

        public Site()
        {
            _productSets = new List<ProductSet>();
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CurrentProductSets"] != null)
            {
                _productSets = (List<ProductSet>)Session["CurrentProductSets"];
            }
        }
    }
}