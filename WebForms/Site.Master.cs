using System;
using System.Collections.Generic;
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

        // METHODS

        public bool ShouldDisplayLoginPrompt()
        {
            if (Session["user"] != null) // Si inició sesión no mostrar
                return false;

            switch (Request.Path)
            {
                case "/Login.aspx":
                    return false;
                case "/Signup.aspx":
                    return false;
                default:
                    return true;
            }
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
