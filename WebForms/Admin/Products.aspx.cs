using System;
using BusinessLogicLayer;

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindProductList();
        }

        private void BindProductList()
        {
            ProductListRepeater.DataSource = _productsManager.List();
            ProductListRepeater.DataBind();
        }

        protected void SearchBtn_Click(object sender, EventArgs e) { }
    }
}
