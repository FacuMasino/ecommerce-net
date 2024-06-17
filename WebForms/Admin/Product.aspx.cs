using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Web;
using System.Web.UI;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace WebForms.Admin
{
    public partial class ProductAdmin : BasePage
    {
        private BrandsManager _brandsManager;
        private CategoriesManager _categoriesManager;
        private ProductsManager _productsManager;
        private Product _product;
        private bool _isEditing;

        // Referencia a la funcion que se ejecutará luego de confirmar/cancelar un Modal
        // Se le debe pasar la masterpage como referencia por si se necesita manipular controles
        // En caso de manipular un control utilizar Helper.FindControl
        private static Action<MasterPage> _modalOkAction;
        private static Action<MasterPage> _modalCancelAction;

        private List<InputWrapper> _inputValidations;

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
            _inputValidations = new List<InputWrapper>();
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
                ProductNameTxt.Text = _product.Name;
                ProductDescriptionTxt.Text = _product.Description;
                ProductCode.Text = _product.Code;
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
        /// Asocia las validaciones JavaScript (del lado del cliente) a los controles necesarios
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

        private void LoadInputValidations()
        {
            _inputValidations.Add(new InputWrapper(ProductNameTxt, typeof(string), 4, 50));
            _inputValidations.Add(new InputWrapper(ProductDescriptionTxt, typeof(string), 50, 300));
            _inputValidations.Add(new InputWrapper(ProductCost, typeof(decimal), 1));
            _inputValidations.Add(new InputWrapper(ProductStock, typeof(int), 1));
            _inputValidations.Add(new InputWrapper(ProductPrice, typeof(decimal), 1));
        }

        private bool RunValidations()
        {
            int invalids = 0;
            foreach (InputWrapper input in _inputValidations)
            {
                if (!Validator.IsGoodInput(input))
                    invalids++;
            }
            return invalids == 0;
        }

        public override void OnModalConfirmed()
        {
            if (_modalOkAction != null)
            {
                _modalOkAction(this.Master);
                _modalOkAction = null; // Limpiar luego de usar
            }
        }

        public override void OnModalCancelled()
        {
            if (_modalCancelAction != null)
            {
                _modalCancelAction(this.Master);
                _modalCancelAction = null; // Limpiar luego de usar
            }
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSession();
            BindControlsValidation();
            LoadInputValidations();

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
            if (ProductImageUrl.Text.Length < 4)
                return;
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

        protected void DeleteProductBtn_Click(object sender, EventArgs e)
        {
            _modalOkAction = DeleteProduct; // Asigna la acción que se va a ejecutar si confirma

            Admin adminMP = (Admin)this.Master;
            adminMP.ShowMasterModal( // Llama y muestra el modal de la Masterpage
                "Eliminar Producto",
                "Está seguro que desea eliminar el producto?",
                true // requiere confirmación
            );
        }

        private void DeleteProduct(MasterPage masterPage)
        {
            _productsManager.Delete(_product);
            HttpContext.Current.Response.Redirect("Products.aspx?successDelete=true");
        }

        protected void RemoveImgLnkButton_Click(object sender, EventArgs e)
        {
            int imgId = Convert.ToInt32(
                ((System.Web.UI.WebControls.LinkButton)sender).CommandArgument
            );
            _product.Images.Remove(_product.Images.Find(im => im.Id == imgId));
            BindImagesRpt();
        }

        protected void SaveProductBtn_Click(object sender, EventArgs e)
        {
            if (!RunValidations())
                Debug.Print("invalido"); // TODO: Implementación pendiente
        }
    }
}
