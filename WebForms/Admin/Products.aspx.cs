using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace WebForms.Admin
{
    public partial class Products : BasePage
    {
        private ProductsManager _productsManager;
        private List<Product> _products;
        private int _temporalProductId;
        private bool _isSearching;
        private string _textToSearch;

        public int TotalProducts
        {
            get { return _products == null ? 0 : _products.Count; }
        }

        public Products()
        {
            _productsManager = new ProductsManager();
            _products = new List<Product>();
            GetProducts();
        }

        // METHODS

        private void GetSearchState()
        {
            _isSearching = ViewState["IsSearching"] as bool? ?? false;
            _textToSearch = ViewState["TextToSearch"] as string ?? "";
        }

        private void SetSearchState(bool isSearching, string textToSearch)
        {
            ViewState["IsSearching"] = isSearching;
            ViewState["TextToSearch"] = textToSearch;
        }

        private void GetProducts()
        {
            _products = _productsManager.List<Product>();
        }

        private void BindProductList()
        {
            ProductListRepeater.DataSource = _products;
            ProductListRepeater.DataBind();
        }

        /// <summary>
        /// Esta funcion se utiliza solo cuándo es llamada desde otra del tipo Action
        /// </summary>
        private void BindProductList(MasterPage masterPage)
        {
            Repeater auxRpt = ((Repeater)Helper.FindControl(masterPage, "ProductListRepeater"));
            auxRpt.DataSource = _products;
            auxRpt.DataBind();
        }

        private void CheckRequest()
        {
            foreach (string key in Request.QueryString.AllKeys)
            {
                switch (key)
                {
                    case "successDelete":
                        if (Request.QueryString[key] == "true")
                        {
                            Notify("Producto eliminado con éxito!");
                        }
                        break;
                    case "successNewProduct":
                        if (Request.QueryString[key] == "true")
                        {
                            Notify("Producto agregado con éxito!");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void Notify(string message)
        {
            Admin adminMP = (Admin)this.Master;
            adminMP.ShowMasterToast(message);
        }

        private void DeleteProductAction(MasterPage masterPage)
        {
            Product auxProduct = _productsManager.Read(_temporalProductId);
            _productsManager.Delete(auxProduct);
            ((Admin)masterPage).ShowMasterToast("Producto eliminado con éxito!");
            //HttpContext.Current.Response.Redirect("Products.aspx?successDelete=true");
            GetProducts();
            BindProductList(masterPage); // Actualizar lista sin Redirect
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckRequest();
                GetProducts();
                BindProductList();
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            string filter = SearchTextBox.Text;
            GetSearchState(); // Obtiene el estado de busqueda

            // Limpiar búsqueda si ya está buscando y el texto es el mismo
            if (_isSearching && _textToSearch == filter)
            {
                // Resetear estado
                SetSearchState(false, ""); // Limpia el estado de busqueda

                // Resetear controles
                SearchBtn.Text = "<i class=\"bi bi-search\"></i>";
                SearchTextBox.Text = "";
                SearchPanel.CssClass = "input-group mb-3";

                GetProducts();
                BindProductList();
                return;
            }

            if (2 <= filter.Length)
            {
                SearchPanel.CssClass = "input-group mb-3";
                _products = _products.FindAll(
                    x =>
                        x.Name.ToUpper().Contains(filter.ToUpper())
                        || x.Brand.ToString().ToUpper().Contains(filter.ToUpper())
                        || x.Code.ToUpper().Contains(filter.ToUpper())
                        || x.Categories.Any(c => c.Name.ToUpper().Contains(filter.ToUpper()))
                );
                SearchBtn.Text = "<i class=\"bi bi-x-circle\"></i>"; // cambia icono boton de busqueda
            }
            else
            {
                SearchPanel.CssClass = "input-group mb-3 invalid";
            }

            SetSearchState(true, filter); // Guarda el estado para saber que está buscando
            BindProductList();
        }

        protected void DeleteProductLnkBtn_Click(object sender, EventArgs e)
        {
            _temporalProductId = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            ModalOkAction = DeleteProductAction;

            Admin adminMP = (Admin)this.Master;
            adminMP.ShowMasterModal( // Llama y muestra el modal de la Masterpage
                "Eliminar Producto",
                "Está seguro que desea eliminar el producto?",
                true // requiere confirmación
            );
        }
    }
}
