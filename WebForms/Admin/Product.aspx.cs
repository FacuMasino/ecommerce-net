using System;
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
        private bool _isEditing;

        public bool IsEditing
        {
            get { return _isEditing; }
        }

        public Product CurrentProduct
        {
            get { return _product; }
        }

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
            ProductBrandDDL.DataValueField = "Id";
            ProductBrandDDL.DataTextField = "Name";
            ProductBrandDDL.DataBind();
        }

        private void BindCategories()
        {
            ProductCategoryDDL.DataSource = _categoriesManager.List();
            ProductCategoryDDL.DataValueField = "Id";
            ProductCategoryDDL.DataTextField = "Name";
            ProductCategoryDDL.DataBind();
        }

        private void BindImages()
        {
            ProductImagesRPT.DataSource = _product.Images;
            ProductImagesRPT.DataBind();
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
                _isEditing = true;
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
            if (_product.Name != null)
            {
                ProductName.Value = _product.Name;
                ProductDescription.Value = _product.Description;
                ProductBrandDDL.SelectedValue = _product.Brand.Id.ToString();
                ProductCategoryDDL.SelectedValue = _product.Category.Id.ToString();
                ProductPrice.Value = _product.Price.ToString();
                ProductStock.Value = _product.Stock.ToString();
                BindImages();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckRequest(); // Verificar si se pasó un Id

            if (_isEditing)
            {
                this.Title = "Editar Producto";
            }

            if (!IsPostBack)
            {
                BindBrands();
                BindCategories();
            }
        }
    }
}
