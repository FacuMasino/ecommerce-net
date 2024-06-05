using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;

namespace WebForms.Admin
{
    public partial class Product : System.Web.UI.Page
    {
        private BrandsManager _brandsManager;
        private CategoriesManager _categoriesManager;

        public Product()
        {
            _brandsManager = new BrandsManager();
            _categoriesManager = new CategoriesManager();
        }

        private void BindBrands()
        {
            ProductBrandDDL.DataSource = _brandsManager.List();
            ProductBrandDDL.DataBind();
        }

        private void BindCategories()
        {
            ProductCategoryDDL.DataSource = _categoriesManager.List();
            ProductCategoryDDL.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBrands();
                BindCategories();
            }
        }
    }
}
