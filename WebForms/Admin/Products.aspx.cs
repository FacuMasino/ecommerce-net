using System;
using System.Collections.Generic;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms.Admin
{
    public partial class Products : System.Web.UI.Page
    {
        private ProductsManager _productsManager;

        public Products()
        {
            _productsManager = new ProductsManager();
        }

        // METHODS

        protected string PrintCategoriesCount(object categoriesList)
        {
            var categories = categoriesList as List<Category>;

            if (categories == null || categories.Count < 2)
            {
                return "";
            }

            return $" (+{categories.Count - 1})";
        }

        private void BindProductList()
        {
            ProductListRepeater.DataSource = _productsManager.List();
            ProductListRepeater.DataBind();
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductList();
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e) { }
    }
}
