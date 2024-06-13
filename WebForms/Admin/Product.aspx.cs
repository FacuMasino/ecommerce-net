using System;
using System.Globalization;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;

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

        // METHODS

        private void BindBrandsDdl()
        {
            ProductBrandDDL.DataSource = _brandsManager.List();
            ProductBrandDDL.DataValueField = "Id";
            ProductBrandDDL.DataTextField = "Name";
            ProductBrandDDL.DataBind();
        }

        private void BindCategoriesDdl()
        {
            CategoriesDdl.DataSource = _categoriesManager.List();
            CategoriesDdl.DataValueField = "Id";
            CategoriesDdl.DataTextField = "Name";
            CategoriesDdl.DataBind();
        }

        private void BindCategoriesRpt()
        {
            ProductCategoriesRpt.DataSource = _product.Categories;
            ProductCategoriesRpt.DataBind();
        }

        private void BindImagesRpt()
        {
            ProductImagesRPT.DataSource = _product.Images;
            ProductImagesRPT.DataBind();
        }

        /// <summary>
        /// Verifica si se pasó una consulta por parametro en la URL
        /// para editar un producto
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

        /// <summary>
        /// Carga de un producto para edición
        /// </summary>
        /// <param name="id">Id del producto</param>
        private void LoadProduct(int id)
        {
            _product = _productsManager.Read(id);

            if (_product.Name != null)
            {
                Session["CurrentProduct"] = _product;
                ProductName.Value = _product.Name;
                ProductDescription.Value = _product.Description;
                ProductBrandDDL.SelectedValue = _product.Brand.Id.ToString();
                CategoriesDdl.SelectedValue = _product.Categories[0].Id.ToString();
                ProductPrice.Text = _product.Price.ToString("F2");
                ProductCost.Text = _product.Cost.ToString("F2");
                ProductStock.Text = _product.Stock.ToString();
                ProductReturns.Text = Helper
                    .CalcReturns(_product.Price, _product.Cost)
                    .ToString("#.##'%'", CultureInfo.InvariantCulture);
                BindImagesRpt();
                BindCategoriesRpt();
            }
        }

        /// <summary>
        /// Verifica si hay un producto guardado en sesión,
        /// si es así, se lo asigna a _product
        /// </summary>
        private void CheckSession()
        {
            if (Session["CurrentProduct"] != null)
            {
                // Se asigna por referencia el mismo objeto
                // Cuando se actualice _product también se actualiza el de la Session
                _product = (Product)Session["CurrentProduct"];
            }
        }

        /// <summary>
        /// Asocia las validaciones JavaScript a los controles necesarios
        /// </summary>
        /// <remarks>
        /// Agrega un atributo 'onfocus' al control para vincular la función de
        /// validación en el navegador cuando el control recibe el foco.
        /// </remarks>
        private void BindControlsValidation()
        {
            // Valida que solo se ingresen números para los TextBox que no son TextMode="Number"
            ProductPrice.Attributes.Add("onfocus", "bindNumberValidation(this.id)");
            ProductCost.Attributes.Add("onfocus", "bindNumberValidation(this.id)");
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSession();
            BindControlsValidation();

            if (_isEditing)
            {
                this.Title = "Editar Producto";
            }

            if (!IsPostBack)
            {
                CheckRequest();
                BindBrandsDdl();
                BindCategoriesDdl();
            }
        }

        protected void AddImageBtn_Click(object sender, EventArgs e)
        {
            Image auxImg = new Image();
            auxImg.Url = ProductImageUrl.Text;
            _product.Images.Add(auxImg);
            BindImagesRpt(); // Actualizar repeater imágenes
        }

        protected void AddCategoryBtn_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Id = Convert.ToInt32(CategoriesDdl.SelectedValue);
            category = _categoriesManager.Read(category.Id);
            _product.Categories.Add(category);
            BindCategoriesRpt();
        }

        protected void CalcReturns_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProductPrice.Text) || string.IsNullOrEmpty(ProductCost.Text))
                return;
            decimal returns = Helper.CalcReturns(
                decimal.Parse(ProductPrice.Text),
                decimal.Parse(ProductCost.Text)
            );
            ProductReturns.Text = $"{returns:#.##}%";
        }
    }
}
