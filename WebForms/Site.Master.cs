using System;
using System.Collections.Generic;
using System.Web;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms
{
    public partial class Site : System.Web.UI.MasterPage
    {
        // ATTRIBUTES
        public List<ProductSet> _productSets;
        private CategoriesManager _categoriesManager;

        // PROPERTIES

        public List<Category> Categories;

        // CONSTRUCT

        public Site()
        {
            _categoriesManager = new CategoriesManager();
            _productSets = new List<ProductSet>();
            Categories = new List<Category>();
            Categories = _categoriesManager.List();
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CurrentProductSets"] != null)
            {
                _productSets = (List<ProductSet>)Session["CurrentProductSets"];
            }
        }

        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("GoodBye.aspx", false);
        }
    }
}
