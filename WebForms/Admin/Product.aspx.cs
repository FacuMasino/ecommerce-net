using System;
using System.Collections.Generic;
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

        private List<InputWrapper> _inputValidations;

        private Admin _adminMP; // Referencia a la master page para utilizar sus métodos

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
            {
                // Si no hay ID entonces es nuevo producto, se asigna referencia al objeto en session
                Session["CurrentProduct"] = _product;
                return;
            }

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
                ProductCodeTxt.Text = _product.Code;
                ProductBrandDDL.SelectedValue = _product.Brand.Id.ToString();
                CategoriesDdl.SelectedValue = _product.Categories[0].Id.ToString();
                ProductPriceTxt.Text = _product.Price.ToString("F2");
                ProductCostTxt.Text = _product.Cost.ToString("F2");
                ProductStockTxt.Text = _product.Stock.ToString();
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
                if (_product.Id != 0)
                    _isEditing = true;
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
            ProductPriceTxt.Attributes.Add("onfocus", "bindNumberValidation(this.id)");
            ProductCostTxt.Attributes.Add("onfocus", "bindNumberValidation(this.id)");
        }

        private void LoadInputValidations()
        {
            _inputValidations.Add(new InputWrapper(ProductNameTxt, typeof(string), 4, 50));
            _inputValidations.Add(new InputWrapper(ProductCodeTxt, typeof(string), 3, 50));
            _inputValidations.Add(new InputWrapper(ProductDescriptionTxt, typeof(string), 50, 300));
            _inputValidations.Add(new InputWrapper(ProductCostTxt, typeof(decimal), 1));
            _inputValidations.Add(new InputWrapper(ProductStockTxt, typeof(int), 1));
            _inputValidations.Add(new InputWrapper(ProductPriceTxt, typeof(decimal), 1));
        }

        public bool IsValidInput(string controlId)
        {
            InputWrapper auxIW = _inputValidations.Find(ctl => ctl.Control.ID == controlId);
            if (auxIW != null && auxIW.IsValid)
                return true;
            return false;
        }

        /// <summary>
        /// Asocia los valores de los campos del WebForm al objeto producto.
        /// Esta función debe llamarse después de validar los datos de entrada.
        /// </summary>
        private void BindFieldsToProduct()
        {
            // DESPUES DE VALIDAR
            // Mapear los campos al objeto producto
            // Aclaración: Las categorías ya fueron agregadas desde el DDL de categorías
            // Lo mismo ocurre con las imágenes
            _product.Name = ProductNameTxt.Text;
            _product.Description = ProductDescriptionTxt.Text;
            _product.Code = ProductCodeTxt.Text;
            _product.Brand.Id = int.Parse(ProductBrandDDL.SelectedValue);
            _product.Cost = Decimal.Parse(ProductCostTxt.Text);
            _product.Price = Decimal.Parse(ProductPriceTxt.Text);
            _product.Stock = int.Parse(ProductStockTxt.Text);
            // No hace falta asignar IsActive porque ya nace "activo"
        }

        private void ClearSession()
        {
            if (Session["CurrentProduct"] != null)
            {
                Session.Remove("CurrentProduct");
            }
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            _adminMP = (Admin)this.Master;

            if (IsPostBack)
                CheckSession();

            BindControlsValidation();
            LoadInputValidations();

            if (!IsPostBack)
            {
                ClearSession();
                CheckRequest();
                BindBrandsDdl();
                BindCategoriesDdl();
            }

            if (_isEditing)
            {
                this.Title = "Editar Producto";
                DeleteProductBtn.Visible = true;
            }
        }

        protected void AddImageBtn_Click(object sender, EventArgs e)
        {
            if (ProductImageUrl.Text.Length < 4)
                return;
            Image auxImg = new Image();
            auxImg.Url = ProductImageUrl.Text;
            if (_product.Images.FindIndex(im => im.Url == auxImg.Url) != -1)
            {
                _adminMP.ShowMasterToast("La imagen ya existe.");
                return;
            }
            _product.Images.Add(auxImg);
            BindImagesRpt(); // Actualizar repeater imágenes
        }

        protected void AddCategoryBtn_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Id = Convert.ToInt32(CategoriesDdl.SelectedValue);

            if (_product.Categories.FindIndex(c => c.Id == category.Id) != -1)
            {
                _adminMP.ShowMasterToast("El producto ya tiene esa categoría.");
                return;
            }

            category = _categoriesManager.Read(category.Id);
            _product.Categories.Add(category);
            BindCategoriesRpt();
            _categoriesManager.AddRelation(category, _product.Id);
        }

        protected void RemoveCategoryBtn_Click(object sender, EventArgs e)
        {
            int categoryId = Convert.ToInt32(
                ((System.Web.UI.WebControls.LinkButton)sender).CommandArgument
            );
            Category category;
            category = _product.Categories.Find(c => c.Id == categoryId);
            _product.Categories.Remove(category);
            BindCategoriesRpt();
            _categoriesManager.DeleteRelation(category, _product.Id);
        }

        protected void CalcReturns_TextChanged(object sender, EventArgs e)
        {
            if (
                string.IsNullOrEmpty(ProductPriceTxt.Text)
                || string.IsNullOrEmpty(ProductCostTxt.Text)
            )
                return;
            decimal returns = Helper.CalcReturns(
                decimal.Parse(ProductPriceTxt.Text),
                decimal.Parse(ProductCostTxt.Text)
            );
            ProductReturns.Text = $"{(returns != 0 ? returns.ToString("#.##") : "-")} %";
        }

        protected void DeleteProductBtn_Click(object sender, EventArgs e)
        {
            ModalOkAction = DeleteProduct; // Asigna la acción que se va a ejecutar si confirma

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
            try
            {
                // TODO: Agregar validacion de codigo de producto único
                // TODO: Agregar validacion de url de imagen única ?
                if (Validator.RunValidations(_inputValidations))
                {
                    BindFieldsToProduct();
                    if (IsEditing)
                    {
                        _productsManager.Edit(_product);
                        _adminMP.ShowMasterToast("Producto actualizado con éxito!");
                    }
                    else
                    {
                        _productsManager.Add(_product);
                        Response.Redirect("Products.aspx?successNewProduct=true");
                    }
                }
            }
            catch (Exception ex)
            {
                _adminMP.ShowMasterToast($"Ocurrió un error: {ex.Message}");
            }
        }
    }
}
