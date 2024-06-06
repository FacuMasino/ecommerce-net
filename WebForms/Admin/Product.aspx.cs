using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms.Admin
{
    public partial class ProductAdmin : System.Web.UI.Page
    {
        private BrandsManager _brandsManager;
        private CategoriesManager _categoriesManager;
        private ProductsManager _productsManager;
        private Product _product;

        public ProductAdmin()
        {
            _brandsManager = new BrandsManager();
            _categoriesManager = new CategoriesManager();
            _productsManager = new ProductsManager();
            _product = new Product();
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

        /// <summary>
        /// Verifica si se una consulta por parametro en la URL
        /// </summary>
        private void CheckRequest()
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
                return;
            try
            {
                int articleId = Convert.ToInt32(Request.QueryString["Id"].ToString());
                LoadProduct(articleId);
            }
            catch (Exception ex)
            {
                if (ex is FormatException || ex is OverflowException)
                {
                    throw (new Exception("Id de producto inválido"));
                }
                else
                {
                    throw ex;
                }
            }
        }

        private void LoadProduct(int id)
        {
            _product = _productsManager.Read(id);
            // TODO: llenar controles
            // TODO: Verificar que pasa cuando se manda un Id que no existe
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckRequest(); // Verificar si se pasó un Id

            if (!IsPostBack)
            {
                BindBrands();
                BindCategories();
            }
        }
    }
}
